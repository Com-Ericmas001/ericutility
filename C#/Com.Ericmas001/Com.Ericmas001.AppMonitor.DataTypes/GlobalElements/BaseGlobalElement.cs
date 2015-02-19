using Com.Ericmas001.AppMonitor.DataTypes.TreeElements;
using Com.Ericmas001.Wpf.ViewModels;

namespace Com.Ericmas001.AppMonitor.DataTypes.GlobalElements
{
    public abstract class BaseGlobalElement : BaseViewModel
    {
        public abstract string Header { get; }

        public BaseBranchTreeElement Branch { get; private set; }

        public BaseGlobalElement(BaseBranchTreeElement branch)
        {
            Branch = branch;
        }
    
    }
}
