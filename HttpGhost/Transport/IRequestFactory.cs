namespace HttpGhost.Transport
{
    public interface IRequestFactory
    {
        IRequest Create(string url);
    }
}