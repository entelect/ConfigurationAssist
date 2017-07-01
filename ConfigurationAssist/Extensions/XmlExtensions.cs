using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConfigurationAssist.Extensions
{
    public static class XmlExtensions
    {
        public static string XmlNodeToString(this XmlNode value)
        {
            if (value == null)
            {
                return null;
            }

            using (var sw = new StringWriter())
            using (var xw = new XmlTextWriter(sw))
            {
                value.WriteTo(xw);
                return sw.ToString();
            }
        }

        public static T Deserialize<T>(this string xmlString) where T : class
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
