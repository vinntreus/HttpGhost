using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public class Navigator
    {
        protected readonly IRequest request;
        private readonly Stopwatch watch;        

        public Navigator(IRequest request)
        {
            this.request = request;
            this.watch = new Stopwatch();
        }

        public INavigationResult Get()
        {
            return Navigate(() => Client.Fetch(request));
        }

        public INavigationResult Post()
        {
            return ModifyData("POST");
        }

        public INavigationResult Put()
        {
            return ModifyData("PUT");
        }

        public INavigationResult Delete()
        {
            return ModifyData("DELETE");
        }

        private INavigationResult ModifyData(string method)
        {
            return Navigate(() => Client.Push(request, method));
        }

        private INavigationResult Navigate(Func<IResponse> getResponse)
        {
            watch.Restart();

            var response = getResponse();
            var result = new NavigationResult(request, response);

            watch.Stop();

            result.TimeSpent = watch.ElapsedMilliseconds;

            return result;
        }
    }    
}