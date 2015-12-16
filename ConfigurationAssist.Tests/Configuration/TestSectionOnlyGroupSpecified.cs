using ConfigurationAssist.CustomAttributes;

namespace ConfigurationAssist.Tests.Configuration
{
    [ConfigurationSectionItem(SectionGroup = "TestingGroup")]
    public class TestSectionOnlyGroupSpecified
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
