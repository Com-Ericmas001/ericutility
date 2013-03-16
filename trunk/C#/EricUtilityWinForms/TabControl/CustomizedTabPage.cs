using System.Windows.Forms;

namespace EricUtility.Windows.Forms
{
    public partial class CustomizedTabPage : TabPage
    {
        public CustomizedTabPage(TabPageContent content) :
            base()
        {
            UseVisualStyleBackColor = true;
            content.Dock = DockStyle.Fill;
            Controls.Add(content);
        }
    }
}