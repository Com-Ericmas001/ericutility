using System;
using System.Collections.Generic;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public interface IBaseCategoryTreeElement<TCategory, TCriteria>
        where TCategory : struct 
        where TCriteria : struct 
    {
        IEnumerable<TCriteria> UsedCriterias{ get; }
        TCriteria SearchCriteria{ get; }
        TCategory Category{ get; }
    }
}
