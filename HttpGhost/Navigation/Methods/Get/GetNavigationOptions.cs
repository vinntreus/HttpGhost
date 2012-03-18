using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Navigation.Methods
{
    public class GetNavigationOptions
    {
        public AuthenticationInfo AuthenticationInfo { get; private set; }
        public string ContentType { get; private set; }
        public object Querystring { get; private set; }

        public GetNavigationOptions(AuthenticationInfo authenticationInfo, string contentType, object querystring)
        {
            AuthenticationInfo = authenticationInfo;
            ContentType = contentType;
            Querystring = querystring;
        }
    }
}