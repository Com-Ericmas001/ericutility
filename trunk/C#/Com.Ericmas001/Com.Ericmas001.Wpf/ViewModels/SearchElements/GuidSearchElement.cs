using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Com.Ericmas001.Wpf.ViewModels.Tabs;
using Com.Ericmas001.Wpf.Validations;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.Entities.Attributes;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [SearchType(SearchTypeEnum.Guid)]
    public class GuidSearchElement : BaseSearchElement
    {
        private string m_Valeur;

        [NullOrEmptyValidation]
        [CustomValidation("ValidateGuid")]
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

        private string ValidateGuid(string text)
        {
            Guid res;
            if (!Guid.TryParse(text, out res))
                return "Ceci doit être un GUID (Exemple 'BC38C957-C65A-4CB8-A3F8-BE88914DA5C8')";
            return null;
        }
    }
}
