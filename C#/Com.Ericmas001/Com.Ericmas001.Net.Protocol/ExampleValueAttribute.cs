using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public class ExampleValueAttribute : Attribute
    {
        public object Value { get; private set; }
        public ExampleValueAttribute(object value)
        {
            Value = value;
        }
    }
}
