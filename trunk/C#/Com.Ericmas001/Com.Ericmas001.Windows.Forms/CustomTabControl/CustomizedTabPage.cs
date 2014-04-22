using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl
{
    public class CustomizedTabPage : TabPage
    {
        public CustomizedTabPage(TabPageContent content)
        {
            UseVisualStyleBackColor = true;
            content.Dock = DockStyle.Fill;
            Controls.Add(content);
        }
    }
}