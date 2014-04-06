using System;

namespace Com.Ericmas001.Net.JSON
{
    internal class Program
    {
        private const string jsonText =
            "{" +
            " \"FirstValue\": 1.1," +
            " \"SecondValue\": \"some text\"," +
            " \"TrueValue\": true" +
            "}";

        private static void Main(string[] args)
        {
            // 1. parse sample

            Console.WriteLine();
            Console.WriteLine("Source data:");
            Console.WriteLine(jsonText);
            Console.WriteLine();

            JsonTextParser parser = new JsonTextParser();
            JsonObject obj = parser.Parse(jsonText);

            Console.WriteLine();
            Console.WriteLine("Parsed data with indentation in JSON data format:");
            Console.WriteLine(obj.ToString());
            Console.WriteLine();

            JsonUtility.GenerateIndentedJsonText = false;

            Console.WriteLine();
            Console.WriteLine("Parsed data without indentation in JSON data format:");
            Console.WriteLine(obj.ToString());
            Console.WriteLine();

            // enumerate values in json object
            Console.WriteLine();
            Console.WriteLine("Parsed object contains these nested fields:");
            foreach (JsonObject field in obj as JsonObjectCollection)
            {
                string name = field.Name;
                string value = string.Empty;
                string type = field.GetValue().GetType().Name;

                // try to get value.
                switch (type)
                {
                    case "String":
                        value = (string)field.GetValue();
                        break;

                    case "Double":
                        value = field.GetValue().ToString();
                        break;

                    case "Boolean":
                        value = field.GetValue().ToString();
                        break;

                    default:

                        // in this sample we'll not parse nested arrays or objects.
                        throw new NotSupportedException();
                }

                Console.WriteLine("{0} {1} {2}",
                    name.PadLeft(15), type.PadLeft(10), value.PadLeft(15));
            }

            Console.WriteLine();

            // 2. generate sample
            Console.WriteLine();

            // root object
            JsonObjectCollection collection = new JsonObjectCollection();

            // nested values
            collection.Add(new JsonStringValue("FirstName", "Pavel"));
            collection.Add(new JsonStringValue("LastName", "Lazureykis"));
            collection.Add(new JsonNumericValue("Age", 23));
            collection.Add(new JsonStringValue("Email", "me@somewhere.com"));
            collection.Add(new JsonBooleanValue("HideEmail", true));

            Console.WriteLine("Generated object:");
            JsonUtility.GenerateIndentedJsonText = true;
            Console.WriteLine(collection);

            Console.WriteLine();

            // 3. generate own library for working with own custom json objects
            ///
            /// Note that generator in this pre-release version of library supports
            /// only JsonObjectCollection in root level ({...}) and only simple
            /// value types can be nested. Not arrays or other objects.
            /// Also names of nested values cannot contain spaces or starts with
            /// numeric symbols. They must comply with C# variable declaration rules.
            JsonGenerator generator = new JsonGenerator();
            generator.GenerateLibrary("Person", collection, @"C:\");
            Console.WriteLine();
        }
    }
}