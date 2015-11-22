using System.Configuration;

namespace ConfigurationAssist.Interfaces
{
    public interface IConfigurationSectionExtractionStrategy
    {
        T ExtractConfiguration<T>() where T : ConfigurationSection, new();
    }
}
