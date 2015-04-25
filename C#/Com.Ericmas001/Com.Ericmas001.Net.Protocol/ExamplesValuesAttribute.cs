using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public class ExampleValuesAttribute : Attribute
    {
        public int NbObjects { get; private set; }
        public object[][] Values { get; private set; }
        public ExampleValuesAttribute(int nbObjects, params object[] values)
        {
            NbObjects = nbObjects;
            if (values != null && values.Length > 0)
            {
                int nbByO = values.Length / nbObjects;
                Values = new object[NbObjects][];
                for (int i = 0; i < NbObjects; ++i)
                    Values[i] = new object[nbByO];
                for(int i = 0; i < values.Length; ++i)
                    Values[i / nbByO][i % nbByO] = values[i];
            }
            else
                Values = new object[0][];
        }
    }
}
