using Com.Ericmas001.Util.IO;
using System;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.Downloader
{
    public partial class DownloadFolder : UserControl
    {
        public DownloadFolder(string defaultFolder)
        {
            InitializeComponent();

            Text = "Directory";

            txtSaveTo.Text = defaultFolder;
        }

        public string LabelText
        {
            get
            {
                return lblText.Text;
            }
            set
            {
                lblText.Text = value;
            }
        }

        public string Folder
        {
            get { return PathHelper.GetWithBackslash(txtSaveTo.Text); }
        }

        private void btnSelAV_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSaveTo.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}