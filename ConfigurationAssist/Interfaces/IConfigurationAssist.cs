using System;
using System.Configuration;

namespace ConfigurationAssist.Interfaces
{
    public interface IConfigurationAssist
    {
        [Obsolete("Rather use ExtractSettings<T> with the appropriate extraction strategy")]
        T ConfigurationSection<T>() where T: ConfigurationSection, new();
        [Obsolete("Rather use ExtractSettings<T> with the appropriate extraction strategy")]
        T ConfigurationSection<T>(string sectionName, string sectionGroup = null) where T : ConfigurationSection, new();
        
        [Obsolete("Rather use ExtractSettings<T> with the appropriate extraction strategy")]
        T AppSettings<T>() where T : class, new();

        T ExtractSettings<T>(IConfigurationExtractionStrategy extractionStrategy) where T : class, new();
        T ExtractSettings<T>(IConfigurationSectionExtractionStrategy extractionStrategy) where T : ConfigurationSection, new();
    }
}
