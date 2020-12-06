using System.IO;
using System.Xml.Serialization;

namespace FractalPainting.Infrastructure.Common
{
    public class XmlObjectSerializer : IObjectSerializer
    {
        public T Deserialize<T>(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                return (T) new XmlSerializer(typeof(T)).Deserialize(ms);
            }
        }

        public byte[] Serialize<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                new XmlSerializer(typeof(T)).Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}