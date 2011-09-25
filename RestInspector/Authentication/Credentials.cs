namespace RestInspector.Authentication
{
	public class Credentials
	{
		private readonly string username;
		private readonly string password;

		public Credentials(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new InvalidUserCredentialsException("not valid username");

			if (string.IsNullOrWhiteSpace(password))
				throw new InvalidUserCredentialsException("not valid password");

			this.username = username;
			this.password = password;
		}

		public string Password
		{
			get { return password; }
		}

		public string Username
		{
			get { return username; }
		}

		public string UsernamePassword
		{
			get { return username + ":" + password; }
		}
	}
}