using System.Collections.Generic;
using System.Linq;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
{
    public class BunchOfDataItems<T> : IBunchOfDataItems
        where T : IDataItem
    {
        private IEnumerable<T> m_Data;

        private Dictionary<string, string[]> m_DistinctValues = new Dictionary<string, string[]>();
        public IEnumerable<T> Data
        {
            get { return m_Data; }
            set
            {
                m_Data = value;
                m_DistinctValues = new Dictionary<string, string[]>();
            }
        }

        public string[] ObtainAllValues(string field)
        {
            if (!m_DistinctValues.ContainsKey(field))
            {
                var keys = new SortedSet<string>();
                foreach (T it in Data.ToArray())
                {
                    keys.Add(it.ObtainValue(field));
                }
                m_DistinctValues.Add(field, keys.ToArray());
            }
            return m_DistinctValues[field];
        }

    }
}
