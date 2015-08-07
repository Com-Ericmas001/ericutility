namespace Com.Ericmas001.Portable.Util.Entities
{
    public interface IDataItem
    {
        string ObtainValue(string field);
        string ObtainFilterValue(string field);
        
        string TextDescription { get; }
        string HtmlDescription { get; }
    }
}
