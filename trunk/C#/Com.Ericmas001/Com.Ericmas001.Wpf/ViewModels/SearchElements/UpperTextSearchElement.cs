using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.Validations;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    public class UpperTextSearchElement : TextSearchElement
    {
        public override string TextValue
        {
            get { return Valeur.ToUpper(); }
        }
    }
}
