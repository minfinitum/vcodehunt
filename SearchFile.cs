namespace VCodeHunt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using VCodeHunt.Config;

    public class SearchFile
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

        public static FileContentType DetectFileType(string file)
        {
            if (SearchBinary.IsMine(file))
            {
                return FileContentType.Binary;
            }
            else if (SearchText.IsMine(file))
            {
                return FileContentType.Text;
            }

            return FileContentType.None;
        }

        public List<KeywordMatch> DetectSearch(SearchParams context, string file)
        {
            List<KeywordMatch> matches = new List<KeywordMatch>();
            var contentType = DetectFileType(file);
            switch (contentType)
            {
                case FileContentType.Binary:
                    {
                        SearchBinary bin = new SearchBinary(m_terminate);
                        matches = bin.Search(context, file);
                    }
                    break;

                case FileContentType.Text:
                default:
                    {
                        SearchText text = new SearchText(m_terminate);
                        matches = text.Search(context, file);
                    }
                    break;
            }

            return (matches == null || matches.Count() == 0) ? null : matches;
        }

        public List<KeywordMatch> Search(SearchParams context, string file)
        {
            m_terminate.Reset();

            List<KeywordMatch> matches = null;
            switch (context.FileType)
            {
                case FileContentType.Text:
                    {
                        SearchText text = new SearchText(m_terminate);
                        matches = text.Search(context, file);
                    }
                    break;
                case FileContentType.Binary:
                    {
                        SearchBinary bin = new SearchBinary(m_terminate);
                        matches = bin.Search(context, file);
                        break;
                    }
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
