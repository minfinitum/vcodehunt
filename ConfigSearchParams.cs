using System.Xml.Serialization;
namespace VCodeHunt.Config
{
    public enum FileContentType
    {
        Detect,
        Binary,
        Text,
        None
    }

    public class SearchParams
    {
        public SearchParams()
        {
            Path = string.Empty;
            Filters = string.Empty;
            Keywords = string.Empty;
        }

        public string Path { get; set; }
        public string Filters { get; set; }
        public string Keywords { get; set; }

        public FileContentType FileType { get; set; }

        public bool UseRegexMatch { get; set; }
        public bool UseCaseSensitiveMatch { get; set; }
        public bool UseNegateSearch { get; set; }
        public bool UseWholeWordMatch { get; set; }
        public bool UseSubFolders { get; set; }

        public bool ShowContextLines { get; set; }
        public int ContextLinesCount { get; set; }

        public bool ShowLineNumbers { get; set; }

        public bool UseMinFileSize { get; set; }
        public int MinFileSize { get; set; }

        public bool UseMaxFileSize { get; set; }
        public int MaxFileSize { get; set; }

        public override int GetHashCode()
        {
            int hashcode = 0;
            hashcode ^= Path.GetHashCode();
            hashcode ^= Filters.GetHashCode();
            hashcode ^= Keywords.GetHashCode();

            hashcode ^= UseRegexMatch.GetHashCode();
            hashcode ^= UseCaseSensitiveMatch.GetHashCode();
            hashcode ^= UseNegateSearch.GetHashCode();
            hashcode ^= UseWholeWordMatch.GetHashCode();
            hashcode ^= UseSubFolders.GetHashCode();
            hashcode ^= ContextLinesCount.GetHashCode();
            hashcode ^= ShowLineNumbers.GetHashCode();

            return hashcode;
        }

        public string DisplayID
        {
            get
            {
                if (Keywords.Length <= 0 || Filters.Length <= 0 || Path.Length <= 0)
                    return string.Empty;

                //int maxlen = 10;
                string keywords = Keywords;
                //if (keywords.Length > maxlen)
                //    keywords = keywords.Substring(0, 8) + "...";

                string filters = Filters;
                //if (filters.Length > maxlen)
                //    filters = filters.Substring(0, 8) + "...";

                string path = Path;
                //if (path.Length > maxlen)
                //    path = "..." + path.Substring(path.Length - maxlen);

                return string.Format("[{0}] - [{1}] [{2}] [{3}]", GetHashCode().ToString("X8"), keywords, filters, path);
            }
        }
    }
}
