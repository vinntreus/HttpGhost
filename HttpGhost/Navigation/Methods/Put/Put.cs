using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Put : NavigationBase
    {
        public Put(IRequest request, PutNavigationOptions options) : base(request)
        {
            request.SetMethod("Put");
            request.SetContentType("application/x-www-form-urlencoded");
            request.SetAuthentication(options.AuthenticationInfo);
            request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}