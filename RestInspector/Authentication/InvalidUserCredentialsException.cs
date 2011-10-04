using System;

namespace RestInspector.Authentication
{
	[Serializable]
	public class InvalidUserCredentialsException : Exception
	{
		public InvalidUserCredentialsException(string message) : base(message){}
	}
}