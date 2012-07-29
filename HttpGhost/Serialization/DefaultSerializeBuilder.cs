namespace HttpGhost.Serialization
{
    public class DefaultSerializeBuilder : ISerializeBuilder
    {
        public ISerializer Build(string contentType)
        {
            switch (contentType)
            {
                case "application/json": return new JsonSerializer();
                default: return new FormSerializer();
            }
        }
    }
}