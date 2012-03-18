using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Navigation.Methods
{
    public class PutNavigationOptions
    {
        public object PostingObject { get; private set; }
        public AuthenticationInfo AuthenticationInfo { get; private set; }

        public PutNavigationOptions(object postingObject, AuthenticationInfo authenticationInfo)
        {
            PostingObject = postingObject;
            AuthenticationInfo = authenticationInfo;
        }
    }
}