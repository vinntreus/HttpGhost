using System.Linq;
using System.Collections.Generic;
using System;

namespace HttpGhost.Authentication
{
	[Serializable]
	public class InvalidUserCredentialsException : Exception
	{
		public InvalidUserCredentialsException(string message) : base(message){}
	}
}