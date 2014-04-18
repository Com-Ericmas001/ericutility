using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util.Options
{
    public static class FactoryOption<TOption, TEnum> 
        where TOption : IOption<TEnum>
    {
        public static TOption GenerateOption(int enumValue)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in a.GetTypes().Where(t => t != typeof(TOption) && typeof(TOption).IsAssignableFrom(t)))
                {
                    TOption instance = (TOption)Activator.CreateInstance(t);
                    if (Convert.ToInt32(instance.OptionType) == enumValue)
                        return instance;
                }
            }
            return default(TOption);
        }
    }
}
