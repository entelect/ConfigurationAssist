using ConfigurationAssist.Tests.Configuration;
using NUnit.Framework;

namespace ConfigurationAssist.Tests
{
    [TestFixture]
    public class ConfigurationAssistTests
    {
        [Test]
        public void ConfigurationSection_Should_ReturnTestConfiguration_When_ConfiguredCorrectly()
        {
            var configAssist = new ConfigurationAssist();
            var config = configAssist.ConfigurationSection<TestConfiguration>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Name, Is.EqualTo("TestName"));
            Assert.That(config.Version, Is.EqualTo("1.0.0.0"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnConfiguration_When_NoConfigurationSectionItemAttributeSet()
        {
            var configAssist = new ConfigurationAssist();
            var config = configAssist.ConfigurationSection<AutomaticConfiguration>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Name, Is.EqualTo("AutomaticName"));
            Assert.That(config.Version, Is.EqualTo("1.2.3.4"));
            Assert.That(config.Value, Is.EqualTo("100.00"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnSectionGroupConfigs_When_GroupSpecifiedOnAttribute()
        {
            var configAssist = new ConfigurationAssist();
            var testGroupSection = configAssist.ConfigurationSection<TestGroupSection>();
            var testGroupOtherSection = configAssist.ConfigurationSection<TestGroupOtherSection>();

            Assert.That(testGroupSection, Is.Not.Null);
            Assert.That(testGroupSection.Name, Is.EqualTo("MyTestGroupSectionName"));
            Assert.That(testGroupSection.Value, Is.EqualTo("MyTestGroupSectionValue"));

            Assert.That(testGroupOtherSection, Is.Not.Null);
            Assert.That(testGroupOtherSection.GetValue, Is.EqualTo("123"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnSectionGroupConfigs_When_GroupSpecifiedAsParameter()
        {
            var configAssist = new ConfigurationAssist();
            var testGroupSection = configAssist.ConfigurationSection<TestGroupSection>("TestGroupSection", "TestingGroup");
            var testGroupOtherSection = configAssist.ConfigurationSection<TestGroupOtherSection>("TestGroupOtherSection","TestingGroup");
            
            Assert.That(testGroupSection, Is.Not.Null);
            Assert.That(testGroupSection.Name, Is.EqualTo("MyTestGroupSectionName"));
            Assert.That(testGroupSection.Value, Is.EqualTo("MyTestGroupSectionValue"));

            Assert.That(testGroupOtherSection, Is.Not.Null);
            Assert.That(testGroupOtherSection.GetValue, Is.EqualTo("123"));
        }
    }
}
