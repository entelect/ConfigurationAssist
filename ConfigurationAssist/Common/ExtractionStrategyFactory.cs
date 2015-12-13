using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using ConfigurationAssist.ConfigurationExtractionStrategies;
using ConfigurationAssist.CustomAttributes;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.Common
{
    public class ExtractionStrategyFactory
    {
        public IConfigurationExtractionStrategy GetExtractionStrategy<T>() where T: class, new()
        {
            var configurationFullName = GetConfigurationName(typeof (T));

            try
            {
                var section = ConfigurationManager.GetSection(configurationFullName);
                var sectionStrategy =  GetSectionExtractionStrategy(section);
                return sectionStrategy ?? new AppSettingExtractionStrategy();
            }
            catch (ConfigurationErrorsException)
            {
                return new AppSettingExtractionStrategy();
            }
        }

        private string GetConfigurationName(Type type)
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

        private IConfigurationExtractionStrategy GetSectionExtractionStrategy(object section)
        {
            if (section is Hashtable)
            {
                return new SingleTagSectionHandlerExtractionStrategy();
            }

            if (section is NameValueCollection)
            {
                return new NameValueHandlerSectionExtractionStrategy();
            }

            if (section is ConfigurationSection)
            {
                return new CustomTypeSectionExtractionStrategy();
            }

            return null;
        }
    }
}
