using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCodeHunt.Config;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace VCodeHunt
{
    class SearchText
    {
        EventWaitHandle m_terminate;

        public SearchText(EventWaitHandle terminate)
        {
            m_terminate = terminate;
        }

        public static bool IsMine(string file)
        {
            return true;
        }

        public List<VCodeHunt.SearchFile.KeywordMatch> Search(SearchParams searchParams, string file)
        {
            // test regex is actually valid
            Regex regex = null;
            if (searchParams.UseRegexMatch)
            {
                try
                {
                    regex = new Regex(searchParams.Keywords, searchParams.UseCaseSensitiveMatch ? RegexOptions.None : RegexOptions.IgnoreCase);
                }
                catch (ArgumentException)
                {
                    // no point dropping out
                    return null;
                }
            }

            List<VCodeHunt.SearchFile.KeywordMatch> matches = new List<VCodeHunt.SearchFile.KeywordMatch>();
            VCodeHunt.SearchFile.KeywordMatch match = new VCodeHunt.SearchFile.KeywordMatch();

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
                        if (regexmatch.Success)
                        {
                            keywordsMatched = regexmatch.Value;
                            found = true;
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
                                if (matchIndex != 0 && char.IsLetterOrDigit(line[matchIndex - 1]))
                                {
                                    found = false;
                                }

                                // test end
                                if (matchIndex != line.Length - searchParams.Keywords.Length && char.IsLetterOrDigit(line[matchIndex + searchParams.Keywords.Length]))
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

                        match = new VCodeHunt.SearchFile.KeywordMatch();
                    }
                    else if (searchParams.ShowContextLines && searchParams.ContextLinesCount > 0)
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
                    {
                        break;
                    }
                }
            }

            return matches.Count() == 0 ? null : matches;
        }
    }
}
