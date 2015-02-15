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
    [FieldType(FieldTypeEnum.Int)]
    public class IntSearchElement : BaseSearchElement
    {
        private string m_Valeur;

        [NullOrEmptyValidation]
        [DigitValidation]
        public string Valeur
        {
            get { return m_Valeur; }
            set 
            { 
                m_Valeur = value;
                RaisePropertyChanged("Valeur");
            }
        }

        public override string TextValue
        {
            get { return Valeur; }
        }
    }
}
