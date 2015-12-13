using ConfigurationAssist.Common;
using ConfigurationAssist.ConfigurationExtractionStrategies;
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
            var configurationAttributeItem = GetConfigurationSectionItem(typeof(T));

            return ConfigurationSection<T>(
                configurationAttributeItem.SectionName,
                configurationAttributeItem.SectionGroup);
        }
      
        public T ConfigurationSection<T>(string sectionName, string sectionGroup = null)
            where T : ConfigurationSection, new()
        {
            if (string.IsNullOrEmpty(sectionName))
            {
                throw new ArgumentNullException("sectionName");
            }

            var fullSectionName = sectionGroup == null
                ? sectionName
                : string.Format("{0}/{1}", sectionGroup, sectionName);

            var extractionStrategy = new CustomTypeSectionExtractionStrategy
            {
                FullSectionName = fullSectionName
            };

            return extractionStrategy.ExtractConfiguration<T>();
        }

        public T AppSettings<T>() where T: class, new()
        {
            var extractionStrategy = new AppSettingExtractionStrategy();
            return extractionStrategy.ExtractConfiguration<T>();
        }

        public T ExtractSettings<T>(IConfigurationExtractionStrategy extractionStrategy) where T : class, new()
        {
            return extractionStrategy.ExtractConfiguration<T>();
        }

        public T ExtractSettings<T>(IConfigurationSectionExtractionStrategy extractionStrategy) where T : ConfigurationSection, new()
        {
            return extractionStrategy.ExtractConfiguration<T>();
        }

        public T TryExtractSettings<T>(IConfigurationExtractionStrategy extractionStrategy) where T : class, new()
        {
            try
            {
                return ExtractSettings<T>(extractionStrategy);
            }
            catch (Exception)
            {
                var configHelper = new ConfigHelperService();
                return configHelper.CreateSettingsWithDefaults<T>();
            }
        }

        public T TryExtractSettings<T>(IConfigurationSectionExtractionStrategy extractionStrategy)
            where T : ConfigurationSection, new()
        {
            try
            {
                return ExtractSettings<T>(extractionStrategy);
            }
            catch (Exception)
            {
                var configHelper = new ConfigHelperService();
                return configHelper.CreateSettingsWithDefaults<T>();
            }
        }

        private ConfigurationSectionItem GetConfigurationSectionItem(Type type)
        {
            var attr = type.GetCustomAttributes(typeof(ConfigurationSectionItem), true).AsQueryable();
            if (attr.Any())
            {
                return (ConfigurationSectionItem) attr.First();
            }

            return new ConfigurationSectionItem(type.Name);
        }
    }
}
