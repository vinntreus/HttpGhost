using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Navigation.Methods
{
    internal class PostNavigationOptions
    {
        public object PostingObject { get; private set; }
        public AuthenticationInfo AuthenticationInfo { get; private set; }
        public string ContentType { get; private set; }

        public PostNavigationOptions(object postingObject, AuthenticationInfo authenticationInfo, string contentType)
        {
            PostingObject = postingObject;
            AuthenticationInfo = authenticationInfo;
            ContentType = contentType ?? "application/x-www-form-urlencoded";
        }
    }
}