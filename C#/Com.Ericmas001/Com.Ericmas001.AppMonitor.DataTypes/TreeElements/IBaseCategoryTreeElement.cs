using System.Collections.Generic;

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
