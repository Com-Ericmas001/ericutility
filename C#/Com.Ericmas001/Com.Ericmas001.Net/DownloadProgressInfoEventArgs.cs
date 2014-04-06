namespace Com.Ericmas001.Net
{
    public class DownloadProgressInfoEventArgs
    {
        private static char[] units = new char[] { ' ', 'K', 'M', 'G', 'T' };

        private long m_SpeedBytes;

        public long SpeedBytes
        {
            get { return m_SpeedBytes; }
        }

        private double m_SpeedValue;

        public double SpeedValue
        {
            get { return m_SpeedValue; }
        }

        private char m_SpeedUnit;

        public char SpeedUnit
        {
            get { return m_SpeedUnit; }
        }private long m_CurrentBytes;

        public long CurrentBytes
        {
            get { return m_CurrentBytes; }
        }

        private double m_CurrentValue;

        public double CurrentValue
        {
            get { return m_CurrentValue; }
        }

        private char m_CurrentUnit;

        public char CurrentUnit
        {
            get { return m_CurrentUnit; }
        }

        private long m_TotalBytes;

        public long TotalBytes
        {
            get { return m_TotalBytes; }
        }

        private double m_TotalValue;

        public double TotalValue
        {
            get { return m_TotalValue; }
        }

        private char m_TotalUnit;

        public char TotalUnit
        {
            get { return m_TotalUnit; }
        }

        private int m_Pourcent;

        public int Pourcent
        {
            get { return m_Pourcent; }
        }

        public DownloadProgressInfoEventArgs(long received, long total, int progress, long speed)
        {
            const int magic = 950;
            m_CurrentBytes = received;
            m_TotalBytes = total;
            m_Pourcent = progress;
            m_SpeedBytes = speed;

            m_CurrentValue = received;
            m_CurrentUnit = units[0];
            for (int i = 1; i < units.Length && m_CurrentValue > magic; ++i)
            {
                m_CurrentUnit = units[i];
                m_CurrentValue /= 1024;
            }
            m_TotalValue = total;
            m_TotalUnit = units[0];
            for (int i = 1; i < units.Length && m_TotalValue > magic; ++i)
            {
                m_TotalUnit = units[i];
                m_TotalValue /= 1024;
            }
            m_SpeedValue = speed;
            m_SpeedUnit = units[0];
            for (int i = 1; i < units.Length && m_SpeedValue > magic; ++i)
            {
                m_SpeedUnit = units[i];
                m_SpeedValue /= 1024;
            }
        }
    }
}