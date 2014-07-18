using System;

namespace Com.Ericmas001.Wpf.Validations
{
    public class NullOrEmptyValidationAttribute : SimpleValidationAttribute
    {
        public string Default { get; set; }
        public override string Validate(string value)
        {
            return String.IsNullOrEmpty(value) ? String.Format("Ce paramètre est obligatoire!{0}", Default == null ? "" : String.Format(" (Défaut: {0})", Default)) : null;
        }
    }
}
