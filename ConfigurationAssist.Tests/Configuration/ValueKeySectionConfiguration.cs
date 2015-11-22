using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class ValueKeySectionConfiguration
    {
        public string Name { get; set; }
        public decimal Value { get; set; }

        [ConfigurationProperty("linkedValue")]
        public string LinkedValue { get; set; }

        public long LongValueDefault { get; set; }

        [ConfigurationProperty("longValueSpecifiedDefault", DefaultValue = 1234567890)]
        public long LongValueSpecifiedDefault { get; set; }

        public long? LongValueDefaultNull { get; set; }
    }
}
