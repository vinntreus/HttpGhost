using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public interface INavigate
    {
        INavigationResult Navigate(IRequest request);
    }

    public class WebClientNavigator : INavigate
    {        
        private readonly Stopwatch watch;        

        public WebClientNavigator()
        {            
            this.watch = new Stopwatch();
        }

        public INavigationResult Navigate(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Method))
            {
                throw new NavigationResultException("Request method is empty");
            }
            if (request.Method.ToUpper().Trim() == "GET")
            {
                return Fetch(request);
            }
            return Push(request);
        }

        private INavigationResult Fetch(IRequest request)
        {            
            var response = Navigate(() => Client.Fetch(request));
            return BuildNavigationResult(request, response);
        }        

        private INavigationResult Push(IRequest request)
        {            
            var response = Navigate(() => Client.Push(request));
            return BuildNavigationResult(request, response);
        }

        private IResponse Navigate(Func<IResponse> getResponse)
        {
            watch.Restart();
            var response = getResponse();
            watch.Stop();
            return response;
        }

        private INavigationResult BuildNavigationResult(IRequest request, IResponse response)
        {
            var result = new NavigationResult(request, response);
            result.TimeSpent = watch.ElapsedMilliseconds;
            return result;
        }        
    }    
}