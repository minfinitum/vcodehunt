namespace CodeHunt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using CodeHunt.Config;

    public class FileSearch
    {
        private EventWaitHandle m_terminate = new EventWaitHandle(false, EventResetMode.ManualReset);

        public class KeywordMatch
        {            
            public KeywordMatch()
            {
                this.ContextPre = new Queue<string>();
                this.ContextPost = new Queue<string>();

                this.Index = 0;
                this.Value = string.Empty;
                this.Match = string.Empty;
                this.MatchType = FileContentType.None;
            }

            public KeywordMatch(long index, string value, string match, FileContentType matchType)
            {
                this.ContextPre = new Queue<string>();
                this.ContextPost = new Queue<string>();

                this.Index = index;
                this.Value = value;
                this.Match = match;

                this.MatchType = matchType;
            }

            public void SetMatch(long index, byte[] value, byte [] match, FileContentType matchType)
            {
                this.Index = index;
                string vstr = string.Empty;
                for (int idx = 0; idx < value.Count(); idx++)
                {
                    vstr += value[idx].ToString("X2");

                    if (idx < value.Count() - 1)
                    {
                        vstr += " ";
                    }
                }

                string mstr = string.Empty;
                for (int idx = 0; idx < match.Count(); idx++)
                {
                    mstr += match[idx].ToString("X2");
                    if (idx < match.Count() - 1)
                    {
                        mstr += " ";
                    }
                }

                this.Value = vstr;
                this.Match = mstr;
                this.MatchType = matchType;
            }

            public void SetMatch(long index, string value, string match, FileContentType matchType)
            {
                this.Index = index;
                this.Value = value;
                this.Match = match;
                this.MatchType = matchType;
            }

            public Int64 Index { get; set; }
            public string Value { get; set; }
            public string Match { get; set; }

            public Queue<string> ContextPre { get; set; }
            public Queue<string> ContextPost { get; set; }

            public FileContentType MatchType { get; set; }

            private static string IndexToString(Int64 index, bool whitespace = false)
            {
                string format = "D";
                if (index <= (Int64)99999999)
                {
                    return string.Format("[{0,8}]", (whitespace) ? " " : index.ToString(format));
                }

                return string.Format("[{0}]", (whitespace) ? " " : index.ToString(format));
            }

            public string BinaryMatchToString(SearchParams searchParams)
            {
                string formatted = string.Empty;
                if (searchParams.ShowLineNumbers)
                {
                    formatted += IndexToString(Index);
                }

                if (searchParams.ContextLinesCount > 0)
                {
                    for (int idx = 0; idx < ContextPre.Count; idx++)
                    {
                        formatted += " " + ContextPre.ElementAt(idx);
                    }
                }

                formatted += " " + Value;

                if (searchParams.ContextLinesCount > 0)
                {
                    for (int idx = 0; idx < ContextPost.Count; idx++)
                    {
                        formatted += " " + ContextPost.ElementAt(idx);
                    }
                }

                formatted += Environment.NewLine;

                return formatted;
            }

            public string TextMatchToString(SearchParams searchParams)
            {
                string formatted = string.Empty;
                if (searchParams.ContextLinesCount > 0)
                {
                    for (int idx = 0; idx < ContextPre.Count; idx++)
                    {
                        if (searchParams.ShowLineNumbers)
                        {
                            formatted += string.Format("{0} {1}", IndexToString(Index - (ContextPre.Count - idx), true), ContextPre.ElementAt(idx)) + Environment.NewLine;
                        }
                        else
                        {
                            formatted += ContextPre.ElementAt(idx) + Environment.NewLine;
                        }
                    }
                }
                
                if (searchParams.ShowLineNumbers)
                {
                    formatted += string.Format("{0} {1}", IndexToString(Index), Value) + Environment.NewLine;
                }
                else
                {
                    formatted += Value + Environment.NewLine;
                }

                if (searchParams.ContextLinesCount > 0)
                {
                    for (int idx = 0; idx < ContextPost.Count; idx++)
                    {
                        if (searchParams.ShowLineNumbers)
                        {
                            formatted += string.Format("{0} {1}", IndexToString(Index + 1 + idx, true), ContextPost.ElementAt(idx)) + Environment.NewLine;
                        }
                        else
                        {
                            formatted += ContextPre.ElementAt(idx) + Environment.NewLine;
                        }
                    }
                }

                return formatted;
            }

            public string ToString(SearchParams searchParams)
            {
                if (MatchType == FileContentType.Binary)
                {
                    return BinaryMatchToString(searchParams);
                }

                return TextMatchToString(searchParams);
            }

        };

        public List<KeywordMatch> TextSearch(SearchParams searchParams, string file)
        {
            m_terminate.Reset();

            // test regex now
            Regex regex = null;
            if(searchParams.UseRegexMatch) 
            {
                try
                {
                    regex = new Regex(searchParams.Keywords, searchParams.UseCaseSensitiveMatch ? RegexOptions.None : RegexOptions.IgnoreCase);
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }

            List<KeywordMatch> matches = new List<KeywordMatch>();
            KeywordMatch match = new KeywordMatch();

            using (StreamReader sr = new StreamReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                string line = string.Empty;
                Int64 lineCount = 1;

                while (!sr.EndOfStream || m_terminate.WaitOne(0))
                {
                    string keywordsMatched = string.Empty;
                    line = sr.ReadLine();
                    if (null == line)
                    {
                        break;
                    }

                    bool found = false;
                    if (searchParams.UseRegexMatch)
                    {
                        Match regexmatch = regex.Match(line);
                        if(regexmatch != null)
                        {
                            keywordsMatched = regexmatch.Value;
                        }
                    }
                    else
                    {
                        int matchIndex = line.IndexOf(searchParams.Keywords, searchParams.UseCaseSensitiveMatch ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);
                        if (-1 != matchIndex)
                        {
                            found = true;
                            keywordsMatched = line.Substring(matchIndex, searchParams.Keywords.Length);

                            if (searchParams.UseWholeWordMatch)
                            {
                                // test start
                                if (matchIndex != 0 && !char.IsWhiteSpace(line[matchIndex - 1]))
                                {
                                    found = false;
                                }

                                // test end
                                if (matchIndex != line.Length - searchParams.Keywords.Length && !char.IsWhiteSpace(line[matchIndex + searchParams.Keywords.Length]))
                                {
                                    found = false;
                                }
                            }
                        }
                    }

                    // store match
                    if (!searchParams.UseNegateSearch == found)
                    {
                        match.SetMatch(lineCount, line, keywordsMatched, FileContentType.Text);
                        matches.Add(match);

                        match = new KeywordMatch();
                    }
                    else if (searchParams.ContextLinesCount > 0)
                    {
                        if (matches.Count > 0 && matches.Last().ContextPost.Count < searchParams.ContextLinesCount)
                        {
                            matches.Last().ContextPost.Enqueue(line);
                        }
                        else
                        {
                            if (match.ContextPre.Count != 0 && match.ContextPre.Count >= searchParams.ContextLinesCount)
                            {
                                match.ContextPre.Dequeue();
                            }
                            match.ContextPre.Enqueue(line);
                        }
                    }

                    lineCount++;

                    if (m_terminate.WaitOne(0))
                        break;
                }
            }

            return matches.Count() == 0 ? null : matches;
        }

        public List<KeywordMatch> BinarySearch(SearchParams searchParams, string file)
        {
            m_terminate.Reset();

            List<KeywordMatch> matches = new List<KeywordMatch>();
            KeywordMatch match = new KeywordMatch();

            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] keywords = ascii.GetBytes(searchParams.Keywords);
            byte[] keywordsLowerCase = ascii.GetBytes(searchParams.Keywords.ToLower());
            byte[] keywordsUpperCase = ascii.GetBytes(searchParams.Keywords.ToUpper());

            int overlap = keywords.Count() - 1;
            const int bufflen = 64 * 1024;
            byte[] buff = new byte[bufflen + overlap];

            using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                long maxlen = br.BaseStream.Length;
                long idx = 0;
                long bytesRead = 0;

                while (idx < maxlen || m_terminate.WaitOne(0))
                {
                    // search data must overlap
                    bytesRead = br.Read(buff, 0, bufflen + overlap);
                    if(0 == bytesRead || bytesRead <= overlap)
                    {
                        break;
                    }
                    
                    // search
                    byte[] matchkeywords = keywords;
                    long bytesMax = bytesRead - (searchParams.Keywords.Length - 1);
                    bool found = false;
                    for (int bidx = 0; bidx < bytesMax; bidx++)
                    {
                        found = false;
                        matchkeywords = keywords;

                        if (searchParams.UseCaseSensitiveMatch)
                        {
                            if (buff[bidx] == keywords[0])
                            {
                                found = true;

                                for (int kidx = 1, sidx = bidx; kidx < keywords.Length && sidx < bytesMax; kidx++, sidx++)
                                {
                                    if (buff[sidx] != keywords[kidx])
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (buff[bidx] == keywordsLowerCase[0])
                            {
                                matchkeywords[0] = keywordsLowerCase[0];
                                found = true;
                            }
                            else if (buff[bidx] == keywordsUpperCase[0])
                            {
                                matchkeywords[0] = keywordsUpperCase[0];
                                found = true;
                            }

                            if (found)
                            {
                                for (int kidx = 1, sidx = bidx + 1; kidx < keywords.Length && sidx < bytesMax; kidx++, sidx++)
                                {
                                    if (buff[sidx] == keywordsLowerCase[kidx])
                                    {
                                        matchkeywords[kidx] = keywordsLowerCase[kidx];
                                    }
                                    else if (buff[sidx] == keywordsUpperCase[kidx])
                                    {
                                        matchkeywords[kidx] = keywordsUpperCase[kidx];
                                    }
                                    else
                                    {
                                        found = false;
                                        break;
                                    }
                                }
                            }
                        }

                        // check for match
                        if (found)
                        {
                            match.SetMatch(idx + bidx, matchkeywords, matchkeywords, FileContentType.Binary);
                            matches.Add(match);

                            match = new KeywordMatch();
                        }
                        else if (searchParams.ContextLinesCount > 0)
                        {
                            if (matches.Count > 0 && matches.Last().ContextPost.Count < searchParams.ContextLinesCount)
                            {
                                matches.Last().ContextPost.Enqueue(String.Format("{0:X2}", buff[bidx]));
                            }
                            else
                            {
                                if (match.ContextPre.Count != 0 && match.ContextPre.Count >= searchParams.ContextLinesCount)
                                {
                                    match.ContextPre.Dequeue();
                                }
                                match.ContextPre.Enqueue(String.Format("{0:X2}", buff[bidx]));
                            }
                        }
                    }

                    idx += (bytesRead - overlap);
                    if (m_terminate.WaitOne(0))
                        break;
                }
            }

            return matches.Count() == 0 ? null : matches;
        }

        public List<KeywordMatch> DetectSearch(SearchParams context, string file)
        {
            List<KeywordMatch> matches = new List<KeywordMatch>();

            // detect file type
            const int bufflen = 8 * 1024;
            byte[] buff = new byte[bufflen];

            int chars = 0;
            bool pe = false;
            long bytesRead = 0;
            using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                bytesRead = br.Read(buff, 0, bufflen);
                if(bytesRead > 2 && ((buff[0] == 'M' || buff[0] == 'm') && (buff[1] == 'Z' || buff[1] == 'z')))
                {
                    pe = true;
                }
                else
                {
                    for (int idx = 0; idx < bytesRead; idx++)
                    {
                        if (buff[idx] >= 20 && buff[idx] <= 126)
                        {
                            chars++;
                        }
                    }
                }
            }

            if (!pe && (chars >= ((bytesRead / 2) - (bytesRead * 0.05))))
            {
                matches = TextSearch(context, file);
            }
            else
            {
                matches = BinarySearch(context, file);
            }

            return (matches == null || matches.Count() == 0) ? null : matches;
        }

        public List<KeywordMatch> Search(SearchParams context, string file)
        {
            List<KeywordMatch> matches = null;
            switch (context.FileType)
            {
                case FileContentType.Text:
                    matches = TextSearch(context, file);
                    break;
                case FileContentType.Binary:
                    matches = BinarySearch(context, file);
                    break;
                case FileContentType.Detect:
                    matches = DetectSearch(context, file);
                    break;
            }
            return matches;
        }

        public void Teminate()
        {
            m_terminate.Set();
        }
    }
}
