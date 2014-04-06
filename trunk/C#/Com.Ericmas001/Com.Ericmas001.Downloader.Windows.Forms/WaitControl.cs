using System.ComponentModel;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.Downloader
{
    public partial class WaitControl : UserControl
    {
        public WaitControl()
        {
            InitializeComponent();
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return label3.Text;
            }
            set
            {
                label3.Text = value;
            }
        }
    }
}