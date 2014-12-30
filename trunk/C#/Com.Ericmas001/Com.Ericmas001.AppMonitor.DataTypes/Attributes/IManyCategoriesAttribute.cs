using System.Collections.Generic;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public interface IManyCategoriesAttribute<out TCategory>
        where TCategory : struct
    {        
        IEnumerable<TCategory> Categories { get; }
    }
}
