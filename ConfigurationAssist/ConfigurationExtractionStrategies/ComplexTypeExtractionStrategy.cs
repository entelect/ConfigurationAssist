using System.Configuration;
using System.Xml;
using ConfigurationAssist.Extensions;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.ConfigurationExtractionStrategies
{
    public class ComplexTypeExtractionStrategy : IConfigurationExtractionStrategy
    {
        public string FullSectionName { get; set; }

        public ComplexTypeExtractionStrategy(string fullSectionName) : this()
        {
            FullSectionName = fullSectionName;
        }

        public ComplexTypeExtractionStrategy()
        {
        }

        public T ExtractConfiguration<T>() where T : class, new()
        {
            if (string.IsNullOrEmpty(FullSectionName))
            {
                FullSectionName = typeof(T).Name;
            }

            var configuration = (XmlNode)ConfigurationManager.GetSection(FullSectionName);
            if (configuration == null)
            {
                throw new ConfigurationErrorsException(string.Format("Could not convert the named section '{0}' to type '{1}'",
                    FullSectionName,
                    typeof(T)));
            }

            var output = configuration.XmlNodeToString().Deserialize<T>();

            return output;
        }
    }
}
