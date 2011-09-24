using System;

namespace WebTester.Authentication
{
	public class InvalidUserCredentialsException : Exception
	{
		public InvalidUserCredentialsException(string message) : base(message){}
	}
}