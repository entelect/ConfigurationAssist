using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class AutomaticConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Name")]
        public string Name { get; set; }
        [ConfigurationProperty("Version")]
        public string Version { get; set; }
        //[ConfigurationProperty("Value")]
        public string Value { get; set; }
    }
}
