using System.Configuration;
using ConfigurationAssist.CustomAttributes;

namespace ConfigurationAssist.Tests.Configuration
{
    [ConfigurationSectionItem("TestConfiguration")]
    public class TestConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name { get; set; }

        [ConfigurationProperty("version")]
        public string Version { get; set; }
    }
}
