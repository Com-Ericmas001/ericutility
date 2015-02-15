using Com.Ericmas001.Util.Entities.Fields;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public interface ISearchCriteriaAttribute<out TCategory> : IManyCategoriesAttribute<TCategory> 
        where TCategory : struct
    {
        FieldTypeEnum FieldType { get; }
    }
}
