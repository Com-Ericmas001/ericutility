using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Com.Ericmas001.Net
{
    public delegate void DownloadProgressInfoEventHandler(object sender, DownloadProgressInfoEventArgs e);

    public class DownloadItem
    {
        private string m_Url;
        private string m_DestinationPath;
        private string m_OverridedFileName;
        private WebClient m_WebClient;

        private DateTime m_LastCheck;
        private long m_LastCheckAmount;
        private long m_LastDelta;

        public event DownloadProgressInfoEventHandler DownloadProgressChanged = delegate { };

        public event AsyncCompletedEventHandler DownloadFileCompleted = delegate { };

        public DownloadItem(string url, string destinationPath) :
            this(url, destinationPath, null)
        {
        }

        public DownloadItem(string url, string destinationPath, string overridedFileName)
        {
            m_Url = url;
            m_DestinationPath = destinationPath;
            m_OverridedFileName = overridedFileName;

            m_WebClient = new System.Net.WebClient();
            m_WebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(m_WebClient_DownloadFileCompleted);
            m_WebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(m_WebClient_DownloadProgressChanged);
        }

        public void StartDownload()
        {
            Uri src = new Uri(m_Url);
            string filename = m_OverridedFileName == null ? src.Segments[src.Segments.Length - 1] : m_OverridedFileName;
            m_WebClient.DownloadFileAsync(src, Path.Combine(m_DestinationPath, filename));
        }

        private void m_WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DateTime now = DateTime.Now;
            if (m_LastCheck == null)
            {
                m_LastCheck = now;
                m_LastCheckAmount = e.BytesReceived;
                m_LastDelta = e.BytesReceived;
            }
            else
            {
                TimeSpan ts = now - m_LastCheck;
                if (ts.TotalSeconds > 1)
                {
                    m_LastCheck = now;
                    m_LastDelta = (long)((e.BytesReceived - m_LastCheckAmount) / ts.TotalSeconds);
                    m_LastCheckAmount = e.BytesReceived;
                }
            }
            DownloadProgressChanged(sender, new DownloadProgressInfoEventArgs(e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage, m_LastDelta));
        }

        private void m_WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DownloadFileCompleted(sender, e);
        }
    }
}