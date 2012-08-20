using System;
using HttpGhost.Navigation;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class HtmlSession : Session<IHtmlResult>
    {
        protected override IHtmlResult Navigate(IRequest request)
        {
            return new HtmlResult(navigator.Navigate(request))
                {
                    OnFollow = url => Get(url),
                    OnSubmitForm = (postingObject, action) =>
                        {
                            var url = UrlByLink.Build(action, new Uri(request.Url));
                            return Post(url, postingObject, ContentType.X_WWW_FORM_URLENCODED);
                        }
                };
        }
    }
}