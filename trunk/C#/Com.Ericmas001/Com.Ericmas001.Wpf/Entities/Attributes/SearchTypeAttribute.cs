using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.Entities.Attributes
{
    public class SearchTypeAttribute : Attribute
    {
        public SearchTypeEnum SearchType { get; private set; }

        public SearchTypeAttribute(SearchTypeEnum searchType)
        {
            SearchType = searchType;
        }
    }
}
