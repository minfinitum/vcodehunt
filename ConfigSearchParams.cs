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
            FiltersInclusions = "*";
            FiltersExclusions = "";

            Keywords = string.Empty;

            FileType = FileContentType.Detect;
            UseRegexMatch = false;
            UseCaseSensitiveMatch = false;
            UseNegateSearch = false;
            UseWholeWordMatch = false;
            UseSubFolders = true;

            ShowContextLines = true;
            ContextLinesCount = 5;

            ShowLineNumbers = true;

            UseMinFileSize = false;
            MinFileSize = 0;

            UseMaxFileSize = false;
            MaxFileSize = 0;
        }

        public string Path { get; set; }
        public string FiltersInclusions { get; set; }
        public string FiltersExclusions { get; set; }
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
            hashcode ^= FiltersInclusions.GetHashCode();
            hashcode ^= FiltersExclusions.GetHashCode();
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
                if (Keywords.Length <= 0 || Path.Length <= 0 || (FiltersInclusions.Length <= 0 && FiltersExclusions.Length <= 0))
                    return string.Empty;
                return string.Format("[{0}] - [{1}] [{2}] [{3}] [{4}]", GetHashCode().ToString("X8"), Keywords, FiltersInclusions, FiltersExclusions, Path);
            }
        }
    }
}
