using Nancy.Authentication.Basic;
using Nancy.Security;

namespace IntegrationTests.Nancy
{
    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            return new BasicUserIdentity { UserName = username };
        }
    }
}