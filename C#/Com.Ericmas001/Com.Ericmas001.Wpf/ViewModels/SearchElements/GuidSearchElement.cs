using System;
using Com.Ericmas001.Portable.Util.Entities.Fields;
using Com.Ericmas001.Wpf.Validations;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.Guid)]
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
