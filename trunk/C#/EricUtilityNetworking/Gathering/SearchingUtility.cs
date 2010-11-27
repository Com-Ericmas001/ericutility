using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtility.Networking.Gathering
{
    public static class SearchingUtility
    {
        public static List<SearchResultEntry> GoogleSearch(string searchString, int maxresults)
        {
            string pattern = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&rsz=large&safe=active&q={0}&start={1}";
            List<SearchResultEntry> resultsList = new List<SearchResultEntry>();
            if ((maxresults % 8) > 0)
                maxresults += (8 - (maxresults % 8));
            for (int p = 0; p < maxresults; p += 8)
            {
                string res = GatheringUtility.GetPageSource(string.Format(pattern, searchString, p));
                int i = -1;
                while ((i = res.IndexOf("GsearchResultClass", i+1))>=0)
                {
                    string url = StringUtility.Extract(res, "\"url\":\"", "\"", i);
                    string title = StringUtility.DecodeString(StringUtility.Extract(res, "\"titleNoFormatting\":\"", "\"", i).Replace("\\u0026", "&"));
                    string content = StringUtility.Extract(res, "\"content\":\"", "\"", i);
                    resultsList.Add(new SearchResultEntry(url,title,content,SearchEngineType.Google));
                }
            }
            return resultsList;
        }
        public static List<SearchResultEntry> GoogleSearch(string searchString)
        {
            return GoogleSearch(searchString, 8);
        }
    }
}
