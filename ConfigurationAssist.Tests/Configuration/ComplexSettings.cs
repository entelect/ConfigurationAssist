using System;

namespace ConfigurationAssist.Tests.Configuration
{
    [Serializable]
    public class SubSettings
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    [Serializable]
    public class SubObject
    {
        public string ObjectName { get; set; }
        public string Alternative { get; set; }
    }

    [Serializable]
    public class ComplexSettings
    {
        public ComplexSettings()
        {
            Sub = new SubObject();
        }

        public string Name { get; set; }
        public string TopValue { get; set; }
        public SubObject Sub { get; set; }
        public SubSettings[] KeyValue { get; set; } 
    }
}
