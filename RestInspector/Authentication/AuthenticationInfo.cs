namespace RestInspector.Authentication
{
	public class AuthenticationInfo
	{
		public AuthenticationInfo(AuthenticationType type, Credentials credentials)
		{
			Type = type;
			Credentials = credentials;
		}

		public AuthenticationType Type { get; private set; }
		public Credentials Credentials { get; private set; }
	}
}