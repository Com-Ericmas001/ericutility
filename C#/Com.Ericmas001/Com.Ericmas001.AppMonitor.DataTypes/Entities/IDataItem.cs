using System.Collections.Generic;

namespace Com.Ericmas001.AppMonitor.DataTypes.Entities
{
    public interface IDataItem
    {
        string ObtainValue(string field);
        string ObtainFilterValue(string field);
        
        string TextDescription { get; }
        string HtmlDescription { get; }
    }

    public interface IDataItem<in TCriteria> : IDataItem
        where TCriteria : struct
    {
        string ObtainValue(TCriteria field);
        string ObtainFilterValue(TCriteria field);
        string ObtainDateTime(IEnumerable<TCriteria> criterias);
    }
}
