using System.Xml.Serialization;

namespace Workouts.XML
{
    public class XMLConverters
    {
        public string ObjectToXML<T>(T obj) where T : class, new()
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static T LoadFromXMLString<T>(string xmlText) where T : class, new()
        {
            using (var stringReader = new StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stringReader) as T;
            }
        }
    }
}
