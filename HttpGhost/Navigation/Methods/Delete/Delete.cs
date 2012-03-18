using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Delete : NavigationBase
    {
        public Delete(IRequest request, DeleteNavigationOptions options) : base(request)
        {
            request.SetMethod("Delete");
            request.SetContentType("application/x-www-form-urlencoded");
            request.SetAuthentication(options.AuthenticationInfo);
            request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}