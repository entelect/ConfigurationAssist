using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using ConfigurationAssist.Common;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.ConfigurationExtractionStrategies
{
    public class NameValueHandlerSectionExtractionStrategy: IConfigurationExtractionStrategy
    {
        private readonly Conversion _converter;

        public NameValueHandlerSectionExtractionStrategy(string fullSectionName) : this()
        {
            FullSectionName = fullSectionName;
        }

        public NameValueHandlerSectionExtractionStrategy()
        {
            _converter = new Conversion();
        }

        public string FullSectionName { get; set; }

        public T ExtractConfiguration<T>() where T : class, new()
        {
            if (string.IsNullOrEmpty(FullSectionName))
            {
                FullSectionName = typeof (T).Name;
            }
            
            var configuration = (NameValueCollection)ConfigurationManager.GetSection(FullSectionName);
            if (configuration == null)
            {
                throw new ConfigurationErrorsException(string.Format("Could not convert the named section '{0}' to type '{1}'",
                    FullSectionName,
                    typeof(T)));
            }

            var output = new T();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string keyName;
                object keyValue;
                var attribute = property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute), true);
                if (!attribute.Any())
                {
                    keyName = property.Name;
                    keyValue = null;
                }
                else
                {
                    var propertyAttribute = (ConfigurationPropertyAttribute)attribute.First();
                    keyName = propertyAttribute.Name;
                    keyValue = propertyAttribute.DefaultValue;
                }

                keyValue = configuration.AllKeys.Contains(keyName)
                    ? configuration[keyName]
                    : keyValue;

                var convertedValue = _converter.Convert(property.PropertyType, keyValue);
                property.SetValue(output, convertedValue, null);
            }

            return output;
        }
    }
}
