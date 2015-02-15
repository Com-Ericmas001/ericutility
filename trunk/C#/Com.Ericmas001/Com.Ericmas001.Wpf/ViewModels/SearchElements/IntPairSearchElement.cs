using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.Validations;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.IntPair)]
    public class IntPairSearchElement : BaseSearchElement
    {
        private string m_Valeur1;
        private string m_Valeur2;

        [NullOrEmptyValidation]
        [DigitValidation]
        public string Valeur1
        {
            get { return m_Valeur1; }
            set
            {
                m_Valeur1 = value;
                RaisePropertyChanged("Valeur1");
            }
        }
        [NullOrEmptyValidation]
        [DigitValidation]
        public string Valeur2
        {
            get { return m_Valeur2; }
            set
            {
                m_Valeur2 = value;
                RaisePropertyChanged("Valeur2");
            }
        }

        public override string TextValue
        {
            get { return Valeur1 + ";" + Valeur2; }
        }
    }
}
