namespace Com.Ericmas001.Util.Entities
{
    public interface IDataItem
    {
        string ObtainValue(string field);
        string ObtainFilterValue(string field);
        
        string TextDescription { get; }
        string HtmlDescription { get; }
    }
}
