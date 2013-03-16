using System;
using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
    public partial class OpenFileTextBox : UserControl
    {
        public event EventHandler OnValueChanged;

        public string Value
        {
            get
            {
                return txtPath.Text;
            }
            set
            {
                txtPath.Text = value;
            }
        }

        public OpenFileTextBox()
        {
            InitializeComponent();
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (OnValueChanged != null)
                OnValueChanged(sender, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OnOpenClick();
        }

        protected virtual void OnOpenClick()
        {
            myOpenFileDialog.FileName = txtPath.Text;
            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
                txtPath.Text = myOpenFileDialog.FileName;
        }
    }
}