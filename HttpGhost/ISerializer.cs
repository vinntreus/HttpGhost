using System.Linq;
using System.Collections.Generic;

namespace HttpGhost
{
	public interface ISerializer
	{
		string Serialize(object objectToSerialize);
	}
}