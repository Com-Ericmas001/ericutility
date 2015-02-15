using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util.Entities.Fields;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.List)]
    public class ListSearchElement : BaseSearchElement
    {
        private DisplayList<string> m_ItemList = new DisplayList<string>();

        public DisplayList<string> ItemList{ get{ return m_ItemList;}}

        public override string TextValue
        {
            get { return ItemList.Selected; }
        }

        public ListSearchElement(IEnumerable<string> items)
            : base()
        {
            items.ToList().ForEach(x => ItemList.Items.Add(x));
        }

        public override bool IsAllInputsValidated()
        {
            return ItemList.Selected != null;
        }
    }
}
