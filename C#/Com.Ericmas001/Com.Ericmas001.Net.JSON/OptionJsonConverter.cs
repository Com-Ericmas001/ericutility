using Com.Ericmas001.Util.Options;
using Newtonsoft.Json.Linq;

namespace Com.Ericmas001.Net.JSON
{
    public class OptionJsonConverter<TOption, TEnum> : AbstractCustomJsonConverter<TOption>
        where TOption : IOption<TEnum>
    {
        public override TOption ObtainCustomObject(JObject jObject)
        {
            return FactoryOption<TOption, TEnum>.GenerateOption(jObject.Value<string>("OptionType"));
        }
    }
}
