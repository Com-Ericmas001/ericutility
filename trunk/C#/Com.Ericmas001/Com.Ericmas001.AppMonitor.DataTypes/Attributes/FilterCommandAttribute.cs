using System;
using System.Collections.Generic;
using Com.Ericmas001.AppMonitor.DataTypes.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public class FilterCommandAttribute : Attribute
    {
        public IEnumerable<FilterCommandEnum> Commands{get; private set; }
        public FilterCommandAttribute(params FilterCommandEnum[] commands)
        {
            Commands = commands;
        }

    }
}
