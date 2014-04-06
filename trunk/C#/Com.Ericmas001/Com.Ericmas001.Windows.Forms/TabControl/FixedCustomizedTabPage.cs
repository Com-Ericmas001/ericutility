namespace Com.Ericmas001.Windows.Forms
{
    public partial class FixedCustomizedTabPage : CustomizedTabPage, INonCloseableTabPage
    {
        public FixedCustomizedTabPage(TabPageContent content)
            : base(content)
        {
        }
    }
}