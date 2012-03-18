using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Navigation.Methods
{
    public class DeleteNavigationOptions
    {
        public object PostingObject { get; private set; }
        public AuthenticationInfo AuthenticationInfo { get; private set; }

        public DeleteNavigationOptions(object postingObject, AuthenticationInfo authenticationInfo)
        {
            PostingObject = postingObject;
            AuthenticationInfo = authenticationInfo;
        }
    }
}