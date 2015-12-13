using System;
using System.Configuration;
using System.Linq;
using ConfigurationAssist.Common;
using ConfigurationAssist.CustomAttributes;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.ConfigurationExtractionStrategies
{
    public class CustomTypeSectionExtractionStrategy : IConfigurationExtractionStrategy
    {
        private readonly Conversion _converter;

        public CustomTypeSectionExtractionStrategy()
        {
            _converter = new Conversion();
        }

        public string FullSectionName { get; set; }
        
        public T ExtractConfiguration<T>() where T : class, new()
        {
            return ExtractConfigurationSection<T>();
        }

        private T ExtractConfigurationSection<T>() where T : class, new()
        {

            if (string.IsNullOrEmpty(FullSectionName))
            {
                FullSectionName = GetConfigurationSectionName(typeof(T));
            }

            var configuration = (T)ConfigurationManager.GetSection(FullSectionName);
            if (configuration == null)
            {
                throw new ConfigurationErrorsException(string.Format("Could not convert the named section '{0}' to type '{1}'",
                    FullSectionName,
                    typeof(T)));
            }

            var baseType = configuration as ConfigurationSection;
            if (baseType == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("The strategy exectuted for {0} is not inherited from type ConfigurationSection",
                        typeof(T).Name));
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
                var propertyValue = baseType.ElementInformation.Properties[name];
                if (propertyValue == null)
                {
                    continue;
                }

                var convertedValue = _converter.Convert(property.PropertyType, propertyValue.Value);
                property.SetValue(configuration, convertedValue, null);
            }

            return configuration;
        }

        private string GetConfigurationSectionName(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(ConfigurationSectionItem), true).AsQueryable();
            if (!attr.Any())
            {
                return type.Name;
            }

            var section = (ConfigurationSectionItem)attr.First();
            var sectionName = string.Empty;
            if (!string.IsNullOrEmpty(section.SectionName))
            {
                sectionName = section.SectionName;
            }

            if (!string.IsNullOrEmpty(section.SectionGroup))
            {
                sectionName = string.Format("{0}/{1}", section.SectionGroup, sectionName);
            }

            return sectionName;
        }
    }
}
