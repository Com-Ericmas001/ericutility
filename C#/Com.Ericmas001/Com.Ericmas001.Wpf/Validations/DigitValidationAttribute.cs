using System;
using System.Linq;

namespace Com.Ericmas001.Wpf.Validations
{
    public class DigitValidationAttribute : SimpleValidationAttribute
    {
        public override string Validate(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;

            return !value.All(Char.IsDigit) ? String.Format("Seul des chiffres sont acceptés!") : null;
        }
    }
}
