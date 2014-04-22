using System.Collections.Generic;

namespace Com.Ericmas001.Net.JSON.Custom
{
    public class JsonArrayCollection : JsonCollection
    {
        public JsonArrayCollection()
        {
        }

        public JsonArrayCollection(string name)
            : base(name)
        {
        }

        public JsonArrayCollection(IEnumerable<JsonObject> collection)
            : base(collection)
        {
        }

        public JsonArrayCollection(string name, IEnumerable<JsonObject> collection)
            : base(name, collection)
        {
        }

        protected override char BeginCollection
        {
            get { return '['; }
        }

        protected override char EndCollection
        {
            get { return ']'; }
        }
    }
}