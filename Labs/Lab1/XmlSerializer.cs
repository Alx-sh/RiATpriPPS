using System.IO;
using System.Text;

namespace Lab1
{
    class XmlSerializer : ISerializer
    {
        public T Deserialize<T>(string str)
        {
            var formatter = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var reader = new StringReader(str))
            {
                return (T)formatter.Deserialize(reader);
            }
        }

        public string Serialize<T>(T obj)
        {
            var formatter = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);

                string serialObject = Encoding.UTF8.GetString(stream.ToArray());
                serialObject = serialObject.Remove(0, serialObject.IndexOf('>') + 1);
                serialObject = serialObject.Remove(serialObject.IndexOf(' '), serialObject.IndexOf('>', serialObject.IndexOf(' ')) - serialObject.IndexOf(' '));

                return serialObject;
            }
        }
    }
}
