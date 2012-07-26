using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
