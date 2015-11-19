using System.Configuration;
using ConfigurationAssist.CustomAttributes;

namespace ConfigurationAssist.Tests.Configuration
{
    [ConfigurationSectionItem("TestGroupOtherSection", "TestingGroup")]
    public class TestGroupOtherSection : ConfigurationSection
    {
        [ConfigurationProperty("testvalue")]
        public string GetValue { get; set; }
    }
}
