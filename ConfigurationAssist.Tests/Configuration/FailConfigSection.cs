using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class FailConfigSection
    {
        [ConfigurationProperty("Value", DefaultValue = "34.12")]
        public decimal Value { get; set; }

        [ConfigurationProperty("BaseValue")]
        public int BaseValue { get; set; }
    }
}
