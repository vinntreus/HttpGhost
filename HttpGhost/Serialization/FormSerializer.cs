using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HttpGhost.Serialization
{
    internal class FormSerializer : ISerializer
    {
        private StringBuilder httpString;

        public FormSerializer()
        {
            httpString = new StringBuilder();
        }

        public string Serialize(object objectToSerialize)
        {
            if (objectToSerialize == null)
                return "";
            if (objectToSerialize is string)
                return objectToSerialize.ToString();
            if(objectToSerialize is IDictionary<string, string>)
            {
                var dictionary = (IDictionary<string, string>) objectToSerialize;
                return string.Join("&", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray());
            }
                

            httpString = new StringBuilder();

            var properties = objectToSerialize.GetType().GetProperties();
            foreach (var propertyInfo in properties.Where(propertyInfo => propertyInfo.CanRead))
            {
                AppendPropertyName(propertyInfo);
                AppendEqualitySign();
                AppendPropertyValue(propertyInfo, objectToSerialize);
                AppendAmpersandSign();
            }

            return httpString.ToString().TrimEnd('&');
        }

        private void AppendAmpersandSign()
        {
            httpString.Append("&");
        }

        private void AppendPropertyValue(PropertyInfo propertyInfo, object objectToSerialize)
        {
            httpString.Append(propertyInfo.GetValue(objectToSerialize, null));
        }

        private void AppendEqualitySign()
        {
            httpString.Append("=");
        }

        private void AppendPropertyName(PropertyInfo propertyInfo)
        {
            httpString.Append(propertyInfo.Name);
        }
    }
}