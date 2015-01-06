using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.Entities.Attributes
{
    public class FiltersAttribute : Attribute
    {
        public IEnumerable<FilterEnum> Filters { get; private set; }

        public FiltersAttribute(params FilterEnum[] filters)
        {
            if (filters == null || !filters.Any() || filters.Contains(FilterEnum.None))
                Filters = new[] { FilterEnum.None };

            Filters = filters;
        }

    }
}
