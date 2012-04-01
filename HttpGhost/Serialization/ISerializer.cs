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
        /// Serialize from anonymous object to string
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
		string Serialize(object objectToSerialize);
	}
}