using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Com.Ericmas001.Net.JSON
{
    public abstract class AbstractCustomJsonConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(T));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var instance = ObtainCustomObject(jObject);
            if (instance != null)
                serializer.Populate(jObject.CreateReader(), instance);
            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public abstract T ObtainCustomObject(JObject jObject);
    }
}
