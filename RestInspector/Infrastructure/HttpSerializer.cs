using System.Reflection;
using System.Text;

namespace RestInspector.Infrastructure
{
	//Spiked -- cap and add tests
	public class HttpSerializer
	{
		private readonly object objektToSerialize;
		private StringBuilder httpString;

		public HttpSerializer(object objektToSerialize)
		{
			this.objektToSerialize = objektToSerialize;
			httpString = new StringBuilder();
		}

		public string Serialize()
		{
			httpString = new StringBuilder();
			var properties = objektToSerialize.GetType().GetProperties();
			foreach (var propertyInfo in properties)
			{
				if (propertyInfo.CanRead)
				{
					AppendPropertyName(propertyInfo);
					AppendEqualitySign();
					AppendPropertyValue(propertyInfo);
				}
			}
			return httpString.ToString();
		}

		private void AppendPropertyValue(PropertyInfo propertyInfo)
		{
			httpString.Append(propertyInfo.GetValue(objektToSerialize, null));
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