using EricUtilityNetworking.Downloader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms.Downloader
{
    public partial class ImportFromFileForm : Form
    {
        public ImportFromFileForm()
        {
            InitializeComponent();
        }

        public string DownloadPath
        {
            get
            {
                return downloadFolder1.Folder;
            }
        }

        public ResourceLocation[] URLs
        {
            get
            {
                List<ResourceLocation> urls = new List<ResourceLocation>();

                if (rdoTextFile.Checked)
                {
                    FillListFromTextFile(urls);
                }

                return urls.ToArray();
            }
        }

        private void FillListFromTextFile(List<ResourceLocation> urls)
        {
            string[] lines = File.ReadAllLines(txtFileName.Text);

            for (int i = 0; i < lines.Length; i++)
            {
                string url = lines[i].Trim();

                if (!String.IsNullOrEmpty(url) && ResourceLocation.IsURL(url))
                {
                    urls.Add(ResourceLocation.FromURL(url));
                }
            }
        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }

        private void rdoDownloadMode_Changed(object sender, EventArgs e)
        {
            location1.Enabled = rdoHTML.Checked;
        }

        private bool IsValid()
        {
            if (!File.Exists(txtFileName.Text))
            {
                MessageBox.Show("Invalid file name.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}