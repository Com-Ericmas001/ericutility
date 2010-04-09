using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EricUtility
{
    public class StringTokenizer
    {
        private string m_TotalStr;
        private string[] m_SplittedStr;
        private int m_Index;
        private string m_Delimitter;

        public StringTokenizer(string str, string delimitter)
        {
            m_TotalStr = str;
            m_Delimitter = delimitter;
            m_SplittedStr = str.Split(delimitter);
            m_Index = -1;
        }

        public void Reset()
        {
            m_Index = -1;
        }

        public bool HasMoreTokens()
        {
            return CountTokens() > m_Index;
        }

        public int CountTokens()
        {
            return m_SplittedStr.Length;
        }

        public string NextToken()
        {
            m_Index++;
            if (HasMoreTokens())
                return m_SplittedStr[m_Index];
            else
                return "";
        }
    }
}
