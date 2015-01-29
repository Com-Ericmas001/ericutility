using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Commands
{
    [FilterCommand(FilterCommandEnum.Must)]
    public class MustBasicFilterCommand : BasicFilterCommand
    {
        public override bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value)
        {
            return comparator.IsDataFiltered(comparatorValue,value);
        }
    }
}
