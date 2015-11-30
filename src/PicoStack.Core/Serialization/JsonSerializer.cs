using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PicoStack.Core.Serialization
{
    public class JsonSerializer
    {
        private static Type[] _primitiveTypes = new[]
        {
            typeof(DateTime),
            typeof(bool),
            typeof(string),
            typeof(int),
            typeof(float),
            typeof(decimal),
            typeof(double),
            typeof(char),
            // and a bunch more...
        };

        public static string Serialize(object obj)
        {
            if (obj == null)
                return "null";

            var objType = obj.GetType();

            if (objType == typeof(string) || objType == typeof(char))
                return $"\"{obj}\"";

            if (_primitiveTypes.Contains(objType))
                return obj.ToString();

            var collection = obj as IEnumerable;

            if (collection != null)
            {
                return SerializeCollection(collection);
            }

            var properties = objType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var serializedProperties = new List<string>();

            foreach (var property in properties)
            {
                var serializedValue = Serialize(property.GetValue(obj, null));
                var camelCasePropertyName = property.Name[0].ToString().ToLower() + property.Name.Substring(1);
                serializedProperties.Add($"\"{camelCasePropertyName}\": {serializedValue}");
            }

            return "{" + string.Join(", ", serializedProperties) + "}";
        }

        private static string SerializeCollection(IEnumerable collection)
        {
            var result = new StringBuilder();
            var serializedChildren = new List<string>();

            foreach (var child in collection)
            {
                serializedChildren.Add(Serialize(child));
            }

            result.Append("[");
            result.Append(string.Join(", ", serializedChildren));
            result.Append("]");

            return result.ToString();
        }
    }
}