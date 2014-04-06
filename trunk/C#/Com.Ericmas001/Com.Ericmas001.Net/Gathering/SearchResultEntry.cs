namespace Com.Ericmas001.Net.Gathering
{
    public class SearchResultEntry
    {
        private string m_Url;
        private string m_Title;
        private string m_Content;
        private SearchEngineType m_Engine;

        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public string Title
        {
            get { return m_Title; }
            set { m_Title = value; }
        }

        public string Content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        public SearchEngineType Engine
        {
            get { return m_Engine; }
            set { m_Engine = value; }
        }

        public SearchResultEntry(string url, string title, string content, SearchEngineType engine)
        {
            m_Url = url;
            m_Title = title;
            m_Content = content;
            m_Engine = engine;
        }
    }
}