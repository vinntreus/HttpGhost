namespace HttpGhost.Serialization
{
    public class DefaultSerializeBuilder : ISerializeBuilder
    {
        public ISerializer BuildBy(string contentType)
        {
            switch (contentType)
            {
                case "application/json": return new JsonSerializer();
                default: return new FormSerializer();
            }
        }
    }
}