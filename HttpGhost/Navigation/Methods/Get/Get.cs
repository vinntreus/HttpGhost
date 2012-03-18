using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Get: Navigator
    {
        public Get(IRequest request, GetNavigationOptions options) : base(request)
        {
            this.request.SetMethod("Get");
            this.request.SetContentType(options.ContentType);
            this.request.SetAuthentication(options.AuthenticationInfo);
        }
    }
}