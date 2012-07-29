namespace HttpGhost.Serialization
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            if (objectToSerialize == null)
                return "";
            return Newtonsoft.Json.JsonConvert.SerializeObject(objectToSerialize);
        }

        public T Deserialize<T>(string body)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body);
        }
    }
}
