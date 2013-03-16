namespace EricUtility.Windows.Forms
{
    public partial class FixedCustomizedTabPage : CustomizedTabPage, INonCloseableTabPage
    {
        public FixedCustomizedTabPage(TabPageContent content)
            : base(content)
        {
        }
    }
}