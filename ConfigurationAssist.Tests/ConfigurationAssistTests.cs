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
    }
}
