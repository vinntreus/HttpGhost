using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Serialization
{
	/// <summary>
	/// Serializer
	/// </summary>
	public interface ISerializer
	{
	    /// <summary>
	    /// Serialize from anonymous object
	    /// </summary>
	    /// <param name="objectToSerialize"></param>
	    /// <param name="contentType"> </param>
	    /// <returns></returns>
	    string Serialize(object objectToSerialize, string contentType);
	}
}