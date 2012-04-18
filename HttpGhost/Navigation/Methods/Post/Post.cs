using System.Linq;
using System.Collections.Generic;
using HttpGhost.Transport;

namespace HttpGhost.Navigation.Methods
{
    internal class Post  : Navigator
    {
        public Post(IRequest request, PostNavigationOptions options) : base(request)
        {
            this.request.SetMethod("Post");
            this.request.SetContentType(options.ContentType);
            this.request.SetAuthentication(options.AuthenticationInfo);
            this.request.WriteFormDataToRequestStream(options.PostingObject);
        }
    }
}