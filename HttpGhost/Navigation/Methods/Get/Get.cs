using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Get: NavigationBase
    {
        public Get(IRequest request, GetNavigationOptions options) : base(request)
        {
            request.SetMethod("Get");
            request.SetContentType(options.ContentType);
            request.SetAuthentication(options.AuthenticationInfo);
        }
    }
}