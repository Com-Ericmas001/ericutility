using System.Drawing;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms
{
    public class OpenFolderTextBox : OpenFileTextBox
    {
        private FolderBrowserDialog m_MyOpenFolderDialog;

        public OpenFolderTextBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            m_MyOpenFolderDialog = new FolderBrowserDialog();
            SuspendLayout();

            //
            // OpenFolderTextBox
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            Name = "OpenFolderTextBox";
            ResumeLayout(false);
            PerformLayout();
        }

        protected override void OnOpenClick()
        {
            m_MyOpenFolderDialog.SelectedPath = Value;
            if (m_MyOpenFolderDialog.ShowDialog() == DialogResult.OK)
                Value = m_MyOpenFolderDialog.SelectedPath;
        }
    }
}