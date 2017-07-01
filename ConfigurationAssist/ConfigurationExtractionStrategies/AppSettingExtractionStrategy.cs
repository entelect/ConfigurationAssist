using System.Configuration;
using System.Linq;
using ConfigurationAssist.Common;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.ConfigurationExtractionStrategies
{
    public class AppSettingExtractionStrategy: IConfigurationExtractionStrategy
    {
        readonly Conversion _converter;

        public AppSettingExtractionStrategy()
        {
            _converter = new Conversion();
        }

        public T ExtractConfiguration<T>() where T : class, new()
        {
            var configuration = new T();

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                string keyName;
                var attribute = property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute), true);
                if (!attribute.Any())
                {
                    keyName = property.Name;
                    if (!ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                    {
                        continue;
                    }
                }
                else
                {
                    var configurationProperty = (ConfigurationPropertyAttribute)attribute.First();
                    keyName = ((ConfigurationPropertyAttribute)attribute.First()).Name;
                    if (!ConfigurationManager.AppSettings.AllKeys.Contains(keyName))
                    {
                        var convertedDefault = _converter.Convert(property.PropertyType, configurationProperty.DefaultValue);
                        property.SetValue(configuration, convertedDefault, null);
                        continue;
                    }
                }

                var convertedValue = _converter.Convert(property.PropertyType, ConfigurationManager.AppSettings[keyName]);
                property.SetValue(configuration, convertedValue, null);
            }

            return configuration;
        }
    }
}
