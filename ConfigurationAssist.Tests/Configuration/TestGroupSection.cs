using System.Configuration;
using ConfigurationAssist.CustomAttributes;

namespace ConfigurationAssist.Tests.Configuration
{
    [ConfigurationSectionItem("TestGroupSection", "TestingGroup")]
    public class TestGroupSection : ConfigurationSection
    {
        [ConfigurationProperty("name")]
        public string Name { get; set; }

        [ConfigurationProperty("value")]
        public string Value { get; set; }
    }
}
