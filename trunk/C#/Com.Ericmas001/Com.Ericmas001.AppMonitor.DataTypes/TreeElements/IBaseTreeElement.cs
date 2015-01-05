using System;
using System.Collections.Generic;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.ViewModels.Trees;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public interface IBaseTreeElement
    {
        IEnumerable<string> UsedStringCriterias{ get; }
    }
}
