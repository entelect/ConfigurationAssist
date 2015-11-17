using System;

namespace ConfigurationAssist.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionItem : Attribute
    {
        public ConfigurationSectionItem(string sectionName)
        {
            SectionName = sectionName;
        }

        public string SectionName { get; set; }
    }
}
