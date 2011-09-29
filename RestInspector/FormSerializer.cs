using System.Reflection;
using System.Text;

namespace RestInspector
{
	public class FormSerializer : ISerializer
	{
		private StringBuilder httpString;

		public FormSerializer()
		{
			httpString = new StringBuilder();
		}

		public string Serialize(object objectToSerialize)
		{
			httpString = new StringBuilder();

			var properties = objectToSerialize.GetType().GetProperties();
			foreach (var propertyInfo in properties)
			{
				if (propertyInfo.CanRead)
				{
					AppendPropertyName(propertyInfo);
					AppendEqualitySign();
					AppendPropertyValue(propertyInfo, objectToSerialize);
				}
			}
			return httpString.ToString();
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