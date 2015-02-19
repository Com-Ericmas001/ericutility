using System.Collections.Generic;

namespace Com.Ericmas001.AppMonitor.DataTypes.TreeElements
{
    public interface IBaseTreeElement
    {
        IEnumerable<string> UsedStringCriterias{ get; }
    }
}
