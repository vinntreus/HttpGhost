using System.Diagnostics;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public abstract class Navigator
    {
        protected readonly IRequest request;
        private readonly Stopwatch watch;

        protected Navigator(IRequest request)
        {
            this.request = request;
            this.watch = new Stopwatch();
        }

        public INavigationResult Navigate()
        {
            watch.Reset();
            watch.Start();
            var result = new NavigationResult(request, request.GetResponse());
            watch.Stop();
            result.TimeSpent = watch.ElapsedMilliseconds;
            return result;
        }
    }
}