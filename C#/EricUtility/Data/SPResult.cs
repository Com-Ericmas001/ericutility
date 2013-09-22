using System.Collections.Generic;

namespace EricUtility2011.Data
{
    public class SPResult
    {
        private List<Dictionary<string, object>> m_QueryResults = new List<Dictionary<string, object>>();

        public List<Dictionary<string, object>> QueryResults
        {
            get { return m_QueryResults; }
            set { m_QueryResults = value; }
        }

        private Dictionary<string, object> m_Parameters = new Dictionary<string, object>();

        public Dictionary<string, object> Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }
    }
}