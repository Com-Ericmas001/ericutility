using Com.Ericmas001.Util;
using System;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.Downloader
{
    public partial class Connection : UserControl
    {
        public Connection()
        {
            Text = "Connection";
            InitializeComponent();

            numRetryDelay.Value = 5;
            numMaxRetries.Value = 10;
            numMinSegSize.Value = 20000;
            numMaxSegments.Value = 5;

            UpdateControls();
        }

        public int RetryDelay
        {
            get
            {
                return (int)numRetryDelay.Value * 1000;
            }
        }

        public int MaxRetries
        {
            get
            {
                return (int)numMaxRetries.Value;
            }
        }

        public int MinSegmentSize
        {
            get
            {
                return (int)numMinSegSize.Value;
            }
        }

        public int MaxSegments
        {
            get
            {
                return (int)numMaxSegments.Value;
            }
        }

        private void numMinSegSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            lblMinSize.Text = ByteFormatter.ToString((int)numMinSegSize.Value);
        }
    }
}