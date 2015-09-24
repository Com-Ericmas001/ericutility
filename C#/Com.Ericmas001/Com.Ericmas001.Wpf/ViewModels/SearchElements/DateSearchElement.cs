using System;
using System.Globalization;
using Com.Ericmas001.Portable.Util.Entities.Fields;

namespace Com.Ericmas001.Wpf.ViewModels.SearchElements
{
    [FieldType(FieldTypeEnum.Date)]
    public class DateSearchElement : BaseSearchElement
    {

        private bool m_Ok = true;
        private DateTime m_Valeur = DateTime.Now;
        
        public string Valeur
        {
            get
            {
                return m_Valeur.ToString("yyyy-MM-dd");
            }
            set
            {
                DateTime myDate = m_Valeur = DateTime.Now;
                if (DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out myDate))
                {
                    m_Ok = true;
                    m_Valeur = myDate;
                }
                else
                    m_Ok = false;
                RaisePropertyChanged("Valeur");
                RaisePropertyChanged("TextValue");
            }
        }

        public override string TextValue
        {
            get { return Valeur; }
        }
        public override bool IsAllInputsValidated()
        {
            return m_Ok;
        }
    }
}
