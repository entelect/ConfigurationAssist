using System.Configuration;

namespace ConfigurationAssist.Interfaces
{
    public interface IConfigurationAssist
    {
        T ConfigurationSection<T>() where T: ConfigurationSection, new();
        T ConfigurationSection<T>(string sectionName) where T : ConfigurationSection, new();
    }
}
