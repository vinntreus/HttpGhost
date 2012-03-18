using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Serialization
{
	public interface ISerializer
	{
		string Serialize(object objectToSerialize);
	}
}