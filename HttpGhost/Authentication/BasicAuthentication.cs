using System;
using System.Text;
using HttpGhost.Transport;

namespace HttpGhost.Authentication
{
    public class BasicAuthentication : IAuthenticate
    {
        private string usernamePassword;

        public BasicAuthentication(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new InvalidUserCredentialsException("username is empty");

            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidUserCredentialsException("password is empty");

            this.usernamePassword = username + ":" + password;
        }

        public void Process(Request request)
        {
            var base64String = GetCredentialsAsBase64();
            request.Headers.Add("Authorization", "Basic " + base64String);
        }

        private string GetCredentialsAsBase64()
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(this.usernamePassword));
        }
    }
}
