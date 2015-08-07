namespace Com.Ericmas001.Portable.Util
{
    public class StringTokenizer
    {
        private readonly string[] m_SplittedStr;
        private int m_Index;

        public StringTokenizer(string str, char delimitter)
        {
            if (str == null)
                m_SplittedStr = new string[0];
            else
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
            {
                var s = m_SplittedStr[m_Index];
                if (s == "")
                    return NextToken();
                return s;
            }
            return null;
        }
    }
}