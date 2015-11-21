using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class TypedPropertiesConfigurationFailCase: ConfigurationSection
    {
        [ConfigurationProperty("intValue")]
        public int IntValue { get; set; }
    }
}
