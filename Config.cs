namespace VCodeHunt.Config
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable()]
    public class AppConfig
    {
        public AppConfig()
        {
            SearchParamsHistoryMaximum = 128;
            SearchParamsHistory = new List<Config.SearchParams>();
            Viewers = new List<Viewer>();
        }

        public void AddSearchParams(SearchParams searchParams)
        {
            for(int idx = 0; idx < SearchParamsHistory.Count; idx++)
            {
                if(SearchParamsHistory[idx].GetHashCode() == searchParams.GetHashCode())
                {
                    SearchParamsHistory.RemoveAt(idx);
                    break;
                }
            }

            // maximum search history items
            if (SearchParamsHistory.Count > SearchParamsHistoryMaximum)
            {
                SearchParamsHistory.RemoveAt(SearchParamsHistory.Count - 1);
            }
            SearchParamsHistory.Insert(0, searchParams);
        }

        [XmlIgnoreAttribute]
        public SearchParams SearchParams { get { return (SearchParamsHistory.Count == 0) ? null : SearchParamsHistory.First(); } set { AddSearchParams(value); } }

        public Int32 SearchParamsHistoryMaximum { get; set; }
        public List<SearchParams> SearchParamsHistory { get; set; }
        public List<Viewer> Viewers { get; set; }
    }
}
