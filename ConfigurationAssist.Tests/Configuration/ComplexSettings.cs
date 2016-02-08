using System.Collections.Generic;
using System.Configuration;

namespace ConfigurationAssist.Tests.Configuration
{
    public class SubSettings
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class SubObject : ConfigurationElement
    {
        [ConfigurationProperty("ObjectName")]
        public string ObjectName { get; set; }
        [ConfigurationProperty("Alternative")]
        public string Alternative { get; set; }
    }

    public class ComplexSettings : ConfigurationSection
    {
        public ComplexSettings()
        {
            KeyValue = new List<SubSettings>();
            Sub = new SubObject();
        }

        [ConfigurationProperty("Name")]
        public string Name { get; set; }

        [ConfigurationProperty("TopValue")]
        public string TopValue { get; set; }

        [ConfigurationProperty("Sub")]
        public SubObject Sub { get; set; }

        [ConfigurationProperty("KeyValue")]
        public IList<SubSettings> KeyValue { get; set; } 
    }
}
