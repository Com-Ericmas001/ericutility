using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.AppMonitor.DataTypes.TreeElements;
using Com.Ericmas001.Wpf.ViewModels;
using Com.Ericmas001.Wpf.ViewModels.Trees;

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
