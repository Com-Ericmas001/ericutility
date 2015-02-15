using Com.Ericmas001.Util.Entities.Fields;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.UpperText)]
    public class UpperTextSearchElement : TextSearchElement
    {
        public override string TextValue
        {
            get { return Valeur.ToUpper(); }
        }
    }
}
