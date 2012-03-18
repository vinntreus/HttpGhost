using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Put : Navigator
    {
        public Put(IRequest request, PutNavigationOptions options) : base(request)
        {
            this.request.SetMethod("Put");
            this.request.SetContentType("application/x-www-form-urlencoded");
            this.request.SetAuthentication(options.AuthenticationInfo);
            this.request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}