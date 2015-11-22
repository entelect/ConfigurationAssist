using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class AppSettingsConfiguration
    {
        public long MaxFileLength { get; set; }

        [ConfigurationProperty("testName")]
        public string TestName { get; set; }

        [ConfigurationProperty("testIntValue")]
        public int TestIntValue { get; set; }

        [ConfigurationProperty("minimumDiscount", DefaultValue = 33.33)]
        public decimal MinimumDiscount { get; set; }

        [ConfigurationProperty("nullDouble")]
        public double? NullDouble { get; set; }
    }
}
