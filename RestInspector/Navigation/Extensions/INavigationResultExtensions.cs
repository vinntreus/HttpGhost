using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace RestInspector.Navigation.Extensions
{
	public static class INavigationResultExtensions
	{
		public static IDictionary<string, object> ToJson(this INavigationResult navigationResult)
		{
			return new JavaScriptSerializer().Deserialize<dynamic>(navigationResult.ResponseString);
		}
	}
}