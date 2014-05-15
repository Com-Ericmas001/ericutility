using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    [Obsolete("HistoryManager is deprecated, please use HistoryFileModel instead.")]
    public class HistoryManager
    {
        private const int MAX_HISTORY = 25;
        private List<string> m_HistoryList = new List<string>();
        private string m_FileName;

        public HistoryManager(string filename)
        {
            m_FileName = filename;
            try
            {
                if (File.Exists(filename))
                    m_HistoryList.AddRange(File.ReadAllLines(filename));
            }
            catch
            {
                m_HistoryList = new List<string>();
            }
        }

        public string[] History
        {
            get
            {
                string[] ret = new string[Count];
                m_HistoryList.CopyTo(0, ret, 0, Count);
                return ret;
            }
        }

        public string MostRecent
        {
            get
            {
                return Count > 0 ? m_HistoryList[0] : "";
            }
        }

        public int Count
        {
            get
            {
                return Math.Min(m_HistoryList.Count, MAX_HISTORY);
            }
        }

        public void AddEntry(string entry)
        {
            if (m_HistoryList.Contains(entry))
                m_HistoryList.Remove(entry);
            m_HistoryList.Insert(0, entry);
            SaveHistory();
        }

        public void SaveHistory()
        {
            try
            {
                StreamWriter sw = new StreamWriter(m_FileName);
                for (int i = 0; i < Count; ++i)
                    sw.WriteLine(m_HistoryList[i]);
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
