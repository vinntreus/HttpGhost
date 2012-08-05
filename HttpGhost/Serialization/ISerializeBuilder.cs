namespace HttpGhost.Serialization
{
    public interface ISerializeBuilder
    {
        ISerializer BuildBy(string contentType);
    }
}