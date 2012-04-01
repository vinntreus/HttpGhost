using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    internal class Delete : Navigator
    {
        public Delete(IRequest request, DeleteNavigationOptions options) : base(request)
        {
            this.request.SetMethod("Delete");
            this.request.SetContentType("application/x-www-form-urlencoded");
            this.request.SetAuthentication(options.AuthenticationInfo);
            this.request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}