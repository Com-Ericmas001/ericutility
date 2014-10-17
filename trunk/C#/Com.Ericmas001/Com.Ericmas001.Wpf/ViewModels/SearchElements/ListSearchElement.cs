using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.Validations;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [SearchType(SearchTypeEnum.List)]
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
