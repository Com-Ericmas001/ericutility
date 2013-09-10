using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using EricUtilityNetworking.Downloader;

namespace EricUtility.Windows.Forms.Downloader
{
    public partial class NewDownloadForm : Form
    {

        public NewDownloadForm()
        {
            InitializeComponent();

            locationMain.UrlChanged += new EventHandler(locationMain_UrlChanged);
        }

        void locationMain_UrlChanged(object sender, EventArgs e)
        {
            try
            {
                Uri u = new Uri(locationMain.ResourceLocation.URL);
                txtFilename.Text = u.Segments[u.Segments.Length - 1];
            }
            catch
            {
                txtFilename.Text = string.Empty;
            }
        }

        public ResourceLocation DownloadLocation
        {
            get
            {
                return locationMain.ResourceLocation;
            }
            set
            {
                locationMain.ResourceLocation = value;
            }
        }

        public ResourceLocation[] Mirrors
        {
            get
            {
                ResourceLocation[] mirrors = new ResourceLocation[lvwLocations.Items.Count];

                for (int i = 0; i < lvwLocations.Items.Count; i++)
                {
                    ListViewItem item = lvwLocations.Items[i];
                    mirrors[i] = ResourceLocation.FromURL(
                            item.SubItems[0].Text, 
                            BoolFormatter.FromString(item.SubItems[1].Text), 
                            item.SubItems[2].Text, 
                            item.SubItems[3].Text);
                }

                return mirrors;
            }
        }

        public string LocalFile
        {
            get
            {
                return PathHelper.GetWithBackslash(folderBrowser1.Folder) + txtFilename.Text;
            }
        }

        public int Segments
        {
            get
            {
                return (int)numSegments.Value;
            }
        }

        public bool StartNow
        {
            get
            {
                return chkStartNow.Checked;
            }
        }

        private void lvwLocations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            bool hasSelected = lvwLocations.SelectedItems.Count > 0;
            btnRemove.Enabled = hasSelected;
            if (hasSelected)
            {
                ListViewItem item = lvwLocations.SelectedItems[0];
                locationAlternate.ResourceLocation = ResourceLocation.FromURL(
                    item.SubItems[0].Text, BoolFormatter.FromString(item.SubItems[1].Text), item.SubItems[2].Text, item.SubItems[3].Text);
            }
            else
            {
                locationAlternate.ResourceLocation = null;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = lvwLocations.Items.Count - 1; i >= 0; i--)
            {
                if (lvwLocations.Items[i].Selected)
                {
                    lvwLocations.Items.RemoveAt(i);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResourceLocation rl = locationAlternate.ResourceLocation;

            if (lvwLocations.SelectedItems.Count > 0)
            {
                ListViewItem item = lvwLocations.SelectedItems[0];
                item.SubItems[0].Text = rl.URL;
                item.SubItems[1].Text = BoolFormatter.ToString(rl.Authenticate);
                item.SubItems[2].Text = rl.Login;
                item.SubItems[3].Text = rl.Password;
            }
            else
            {
                ListViewItem item = new ListViewItem();
                item.Text = rl.URL;
                item.SubItems.Add(BoolFormatter.ToString(rl.Authenticate));
                item.SubItems.Add(rl.Login);
                item.SubItems.Add(rl.Password);
                lvwLocations.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ResourceLocation rl = this.DownloadLocation;

                rl.BindProtocolProviderType();

                if (rl.ProtocolProviderType == null)
                {
                    MessageBox.Show("Invalid URL format, please check the location field.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    DialogResult = DialogResult.None;
                    return;
                }

                ResourceLocation[] mirrors = this.Mirrors;

                if (mirrors != null && mirrors.Length > 0)
                {
                    foreach (ResourceLocation mirrorRl in mirrors)
                    {
                        mirrorRl.BindProtocolProviderType();

                        if (mirrorRl.ProtocolProviderType == null)
                        {
                            MessageBox.Show("Invalid mirror URL format, please check the mirror URLs.",
                                "Error", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);

                            DialogResult = DialogResult.None;
                            return;
                        }
                    }
                }

                    EricUtilityNetworking.Downloader.Downloader download = DownloadManager.Instance.Add(
                        rl,
                        mirrors,
                        this.LocalFile,
                        this.Segments,
                        this.StartNow);

                Close();
            }
            catch (Exception)
            {
                DialogResult = DialogResult.None;

                MessageBox.Show("Unknow error, please check your input data.",
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private TreeNode GetNodeFromPath(String path, out string displayName)
        {
            string[] subPaths = path.Split('/');

            if (subPaths.Length == 0)
            {
                displayName = null;
                return null;
            }

            TreeNode result = null;

            TreeNodeCollection nodes = checkableTreeView1.Nodes;

            displayName = subPaths[subPaths.Length - 1];

            for (int j = 0; j < subPaths.Length - 1; j++)
            {
                TreeNode parentNode = null;

                for (int i = 0; i < nodes.Count; i++)
                {
                    if (String.Equals(nodes[i].Text, subPaths[j], StringComparison.OrdinalIgnoreCase))
                    {
                        parentNode = nodes[i];
                        break;
                    }
                }

                if (parentNode == null)
                {
                    // add the path
                    result = new TreeNode(subPaths[j]);
                    result.ImageIndex = FileTypeImageList.GetImageIndexFromFolder(false);
                    result.SelectedImageIndex = FileTypeImageList.GetImageIndexFromFolder(true);
                    nodes.Add(result);                    
                }
                else
                {
                    result = parentNode;
                }

                nodes = result.Nodes;
            }

            return result;
        }
    }
}