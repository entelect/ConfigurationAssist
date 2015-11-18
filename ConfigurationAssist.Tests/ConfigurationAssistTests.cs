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
    }
}
