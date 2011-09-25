using System;

namespace RestInspector.Authentication
{
	public class InvalidUserCredentialsException : Exception
	{
		public InvalidUserCredentialsException(string message) : base(message){}
	}
}