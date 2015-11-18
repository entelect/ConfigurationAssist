using ConfigurationAssist.CustomAttributes;
using ConfigurationAssist.Interfaces;
using System;
using System.Configuration;
using System.Linq;

namespace ConfigurationAssist
{
    public class ConfigurationAssist : IConfigurationAssist
    {
        public T ConfigurationSection<T>() where T : ConfigurationSection, new()
        {
            var type = typeof (T);
            var attr = type.GetCustomAttributes(typeof(ConfigurationSectionItem), true).AsQueryable();

            var sectionName = attr.Any()
                ? ((ConfigurationSectionItem) attr.First()).SectionName
                : type.Name;

            return ConfigurationSection<T>(sectionName);
        }

        public T ConfigurationSection<T>(string sectionName) where T : ConfigurationSection, new()
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                throw new ArgumentNullException("sectionName");
            }

            var configuration = (T)ConfigurationManager.GetSection(sectionName);
            if (configuration == null)
            {
                throw new ConfigurationErrorsException(string.Format("Could not convert the named section '{0}' to type '{1}'",
                    sectionName,
                    typeof(T)));
            }

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute), true);
                if (!attribute.Any())
                {
                    continue;
                }

                var name = ((ConfigurationPropertyAttribute)attribute.First()).Name;
                var propertyInformation = configuration.ElementInformation.Properties[name];
                if (propertyInformation == null)
                {
                    continue;
                }

                var value = propertyInformation.Value;
                property.SetValue(configuration, value, null);
            }

            return configuration;
        }
    }
}
