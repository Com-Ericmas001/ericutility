using Com.Ericmas001.Portable.Util.Entities.Fields;
using Com.Ericmas001.Wpf.Validations;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.Text)]
    public class TextSearchElement : BaseSearchElement
    {
        private string m_Valeur;

        [NullOrEmptyValidation]
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
