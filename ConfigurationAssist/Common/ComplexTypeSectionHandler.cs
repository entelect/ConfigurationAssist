using System.Configuration;
using System.Xml;

namespace ConfigurationAssist.Common
{
    public class ComplexTypeSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return section;
        }
    }
}
