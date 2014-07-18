using System;

namespace Com.Ericmas001.Wpf.Validations
{
    public class MinDigitValidationAttribute : DigitValidationAttribute
    {
        private int m_Min;

        public MinDigitValidationAttribute(int min)
        {
            m_Min = min;
        }
        public override string Validate(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            if (!String.IsNullOrEmpty(base.Validate(value)))
                return base.Validate(value);

            return int.Parse(value) < m_Min ? String.Format("Le minimum possible est {0}!", m_Min) : null;
        }
    }
}
