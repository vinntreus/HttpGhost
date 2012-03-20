using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    public class Post  : Navigator
    {
        public Post(IRequest request, PostNavigationOptions options) : base(request)
        {
            this.request.SetMethod("Post");
            this.request.SetContentType("application/x-www-form-urlencoded");
            this.request.SetAuthentication(options.AuthenticationInfo);
            this.request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}