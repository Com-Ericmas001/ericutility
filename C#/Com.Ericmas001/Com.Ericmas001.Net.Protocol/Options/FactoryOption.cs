using System;
using System.Linq;

namespace Com.Ericmas001.Net.Protocol.Options
{
    public static class FactoryOption<TOption, TEnum> 
        where TOption : IOption<TEnum>
    {
        public static TOption GenerateOption(string enumValue)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var t in a.GetTypes().Where(t => t != typeof(TOption) && typeof(TOption).IsAssignableFrom(t)))
                {
                    var instance = (TOption)Activator.CreateInstance(t);
                    if (instance.OptionType.ToString() == enumValue)
                        return instance;
                }
            }
            return default(TOption);
        }
    }
}
