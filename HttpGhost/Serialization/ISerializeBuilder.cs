namespace HttpGhost.Serialization
{
    public interface ISerializeBuilder
    {
        ISerializer Build(string contentType);
    }
}