using System.Collections.Generic;
using Com.Ericmas001.Portable.Util.Entities;

namespace Com.Ericmas001.AppMonitor.DataTypes
{
    public interface IDataItem<in TCriteria> : IDataItem
        where TCriteria : struct
    {
        string ObtainValue(TCriteria field);
        string ObtainFilterValue(TCriteria field);
        string ObtainDateTime(IEnumerable<TCriteria> criterias);
    }
}
