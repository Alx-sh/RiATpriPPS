using Newtonsoft.Json;
using System.Text;

namespace Lab3
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(byte[] str)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(str));
        }

        public byte[] Serialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }
    }
}
