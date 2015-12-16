using System;

namespace ConfigurationAssist.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionItem : Attribute
    {
        public ConfigurationSectionItem()
        {
        }

        public ConfigurationSectionItem(string sectionName = null) :this()
        {
            SectionName = sectionName;
        }

        public ConfigurationSectionItem(string sectionName, string sectionGroup) : this(sectionName)
        {
            SectionGroup = sectionGroup;
        }

        public string SectionName { get; set; }
        public string SectionGroup { get; set; }
    }
}
