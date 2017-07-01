using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Xml;
using ConfigurationAssist.ConfigurationExtractionStrategies;
using ConfigurationAssist.CustomAttributes;
using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.Common
{
    public class ExtractionStrategyFactory
    {
        public IConfigurationExtractionStrategy GetExtractionStrategy<T>() where T : class, new()
        {
            var configurationFullName = GetConfigurationName(typeof(T));

            try
            {
                var section = ConfigurationManager.GetSection(configurationFullName);
                var sectionStrategy = GetSectionExtractionStrategy(section, configurationFullName);
                return sectionStrategy ?? new AppSettingExtractionStrategy();
            }
            catch (ConfigurationErrorsException)
            {
                return new AppSettingExtractionStrategy();
            }
        }

        string GetConfigurationName(Type type)
        {
            var sectionName = type.Name;
            var attr = type.GetCustomAttributes(typeof(ConfigurationSectionItem), true).AsQueryable();
            if (!attr.Any())
            {
                return sectionName;
            }

            var section = (ConfigurationSectionItem)attr.First();
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

        IConfigurationExtractionStrategy GetSectionExtractionStrategy(object section, string fullSectionName)
        {
            if (section is Hashtable)
            {
                return new SingleTagSectionHandlerExtractionStrategy(fullSectionName);
            }

            if (section is NameValueCollection)
            {
                return new NameValueHandlerSectionExtractionStrategy(fullSectionName);
            }

            if (section is XmlNode)
            {
                return new ComplexTypeExtractionStrategy(fullSectionName);
            }

            if (section is ConfigurationSection)
            {
                throw new NotSupportedException("Traditional ConfigurationSection objects are not currently supported");
            }

            return null;
        }
    }
}
