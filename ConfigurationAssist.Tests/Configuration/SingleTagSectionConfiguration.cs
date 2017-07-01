using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class SingleTagSectionConfiguration
    {
        public string Name { get; set; }

        [ConfigurationProperty("intValue")]
        public int IntValue { get; set; }

        [ConfigurationProperty("decimalValue")]
        public decimal DecimalValue { get; set; }

        [ConfigurationProperty("doubleValue")]
        public double DoubleValue { get; set; }

        [ConfigurationProperty("longValue")]
        public long LongValue { get; set; }

        [ConfigurationProperty("stringValue")]
        public string StringValue { get; set; }

        [ConfigurationProperty("notConfigured")]
        public string NotConfigured { get; set; }

        [ConfigurationProperty("notConfiguredNullableInt")]
        public int? NotConfiguredNullableInt { get; set; }
    }
}
