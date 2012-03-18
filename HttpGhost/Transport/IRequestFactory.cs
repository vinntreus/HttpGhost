namespace HttpGhost.Transport
{
    public interface IRequestFactory
    {
        IRequest Get(string url);
    }
}