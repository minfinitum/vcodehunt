using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCodeHunt.Config;
using System.IO;
using System.Threading;

namespace VCodeHunt
{
    class SearchBinary
    {
        EventWaitHandle m_terminate;
        public SearchBinary(EventWaitHandle terminate)
        {
            m_terminate = terminate;
        }

        public static bool IsMine(string file)
        {
            const int bufflen = 8 * 1024;
            byte[] buff = new byte[bufflen];

            int chars = 0;
            bool pe = false;
            long bytesRead = 0;
            using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                bytesRead = br.Read(buff, 0, bufflen);
                if (bytesRead > 2 && ((buff[0] == 'M' || buff[0] == 'm') && (buff[1] == 'Z' || buff[1] == 'z')))
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

            return (pe || (chars < ((bytesRead / 2) - (bytesRead * 0.05))));
        }

        public List<VCodeHunt.SearchFile.KeywordMatch> Search(SearchParams searchParams, string file)
        {
            List<VCodeHunt.SearchFile.KeywordMatch> matches = new List<VCodeHunt.SearchFile.KeywordMatch>();
            VCodeHunt.SearchFile.KeywordMatch match = new VCodeHunt.SearchFile.KeywordMatch();

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
                    if (0 == bytesRead || bytesRead <= overlap)
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
                            int kidx = 0;
                            int sidx = bidx;
                            for (; kidx < keywords.Length && sidx < bytesMax; kidx++, sidx++)
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

                            found = kidx == keywords.Length;
                        }

                        // check for match
                        if (found)
                        {
                            match.SetMatch(idx + bidx, matchkeywords, matchkeywords, FileContentType.Binary);
                            matches.Add(match);

                            match = new VCodeHunt.SearchFile.KeywordMatch();
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
                    {
                        break;
                    }
                }
            }

            return matches.Count() == 0 ? null : matches;
        }

    }
}
