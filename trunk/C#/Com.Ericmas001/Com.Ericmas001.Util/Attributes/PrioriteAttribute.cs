using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Util.Attributes
{
    public class PrioriteAttribute
    {
        public int Priorite { get; private set; }

        public PrioriteAttribute(int priorite)
        {
            Priorite = priorite;
        }
    }
}
