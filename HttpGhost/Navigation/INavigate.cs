using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public interface INavigate
    {
        INavigationResult Navigate(IRequest request);
    }
}