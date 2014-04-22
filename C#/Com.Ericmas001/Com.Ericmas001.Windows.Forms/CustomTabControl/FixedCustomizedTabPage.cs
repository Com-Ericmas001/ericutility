using Com.Ericmas001.Windows.Forms.Annotations;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl
{
    [UsedImplicitly]
    public class FixedCustomizedTabPage : CustomizedTabPage, INonCloseableTabPage
    {
        public FixedCustomizedTabPage(TabPageContent content)
            : base(content)
        {
        }
    }
}