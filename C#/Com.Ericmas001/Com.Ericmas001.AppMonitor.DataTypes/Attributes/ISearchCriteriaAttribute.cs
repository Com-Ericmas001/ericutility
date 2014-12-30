using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public interface ISearchCriteriaAttribute<out TCategory> : IManyCategoriesAttribute<TCategory> 
        where TCategory : struct
    {
        SearchTypeEnum SearchType { get; }
    }
}
