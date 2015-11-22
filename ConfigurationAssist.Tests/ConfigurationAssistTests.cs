using System.Configuration;
using ConfigurationAssist.Interfaces;
using ConfigurationAssist.Tests.Configuration;
using NUnit.Framework;

namespace ConfigurationAssist.Tests
{
    [TestFixture, Category(TestCategory.Unit)]
    public class ConfigurationAssistTests
    {
        private IConfigurationAssist _configurationAssist;
        
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            _configurationAssist = new ConfigurationAssist();
        }

        [Test]
        public void ConfigurationSection_Should_ReturnTestConfiguration_When_ConfiguredCorrectly()
        {
            var config = _configurationAssist.ConfigurationSection<TestConfiguration>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Name, Is.EqualTo("TestName"));
            Assert.That(config.Version, Is.EqualTo("1.0.0.0"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnConfiguration_When_NoConfigurationSectionItemAttributeSet()
        {
            var config = _configurationAssist.ConfigurationSection<AutomaticConfiguration>();

            Assert.That(config, Is.Not.Null);
            Assert.That(config.Name, Is.EqualTo("AutomaticName"));
            Assert.That(config.Version, Is.EqualTo("1.2.3.4"));
            Assert.That(config.Value, Is.EqualTo("100.00"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnSectionGroupConfigs_When_GroupSpecifiedOnAttribute()
        {
            var testGroupSection = _configurationAssist.ConfigurationSection<TestGroupSection>();
            var testGroupOtherSection = _configurationAssist.ConfigurationSection<TestGroupOtherSection>();

            Assert.That(testGroupSection, Is.Not.Null);
            Assert.That(testGroupSection.Name, Is.EqualTo("MyTestGroupSectionName"));
            Assert.That(testGroupSection.Value, Is.EqualTo("MyTestGroupSectionValue"));

            Assert.That(testGroupOtherSection, Is.Not.Null);
            Assert.That(testGroupOtherSection.GetValue, Is.EqualTo("123"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnSectionGroupConfigs_When_GroupSpecifiedAsParameter()
        {
            var testGroupSection = _configurationAssist.ConfigurationSection<TestGroupSection>("TestGroupSection", "TestingGroup");
            var testGroupOtherSection = _configurationAssist.ConfigurationSection<TestGroupOtherSection>("TestGroupOtherSection", "TestingGroup");
            
            Assert.That(testGroupSection, Is.Not.Null);
            Assert.That(testGroupSection.Name, Is.EqualTo("MyTestGroupSectionName"));
            Assert.That(testGroupSection.Value, Is.EqualTo("MyTestGroupSectionValue"));

            Assert.That(testGroupOtherSection, Is.Not.Null);
            Assert.That(testGroupOtherSection.GetValue, Is.EqualTo("123"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnSectionGroupConfigs_When_GroupSpecifiedPartOfSectionName()
        {
            var testGroupSection = _configurationAssist.ConfigurationSection<TestGroupSection>("TestingGroup/TestGroupSection");
            var testGroupOtherSection = _configurationAssist.ConfigurationSection<TestGroupOtherSection>("TestingGroup/TestGroupOtherSection");

            Assert.That(testGroupSection, Is.Not.Null);
            Assert.That(testGroupSection.Name, Is.EqualTo("MyTestGroupSectionName"));
            Assert.That(testGroupSection.Value, Is.EqualTo("MyTestGroupSectionValue"));

            Assert.That(testGroupOtherSection, Is.Not.Null);
            Assert.That(testGroupOtherSection.GetValue, Is.EqualTo("123"));
        }

        [Test]
        public void ConfigurationSection_Should_ReturnNotThrowException_When_ConfigurationWithPrimitivesPropertiesOtherThanStringCalled()
        {
            var typedPropertiesConfiguration = _configurationAssist.ConfigurationSection<TypedPropertiesConfiguration>();

            Assert.That(typedPropertiesConfiguration, Is.Not.Null);
            Assert.That(typedPropertiesConfiguration.DecimalValue, Is.EqualTo(12.4m));
            Assert.That(typedPropertiesConfiguration.DoubleValue, Is.EqualTo(123.43d));
            Assert.That(typedPropertiesConfiguration.IntValue, Is.EqualTo(1));
            Assert.That(typedPropertiesConfiguration.LongValue, Is.EqualTo(23L));
            Assert.That(typedPropertiesConfiguration.StringValue, Is.EqualTo("Test"));
            Assert.That(typedPropertiesConfiguration.NotConfigured, Is.EqualTo(string.Empty));
            Assert.That(typedPropertiesConfiguration.NotConfiguredNullableInt, Is.Null);
        }

        [Test]
        public void ConfigurationSection_Should_ThrowConfigurationErrorsException_When_ConfigurationPrimitiveDiffersFromConfigrationValue()
        {
            Assert.Throws<ConfigurationErrorsException>(() => _configurationAssist.ConfigurationSection<TypedPropertiesConfigurationFailCase>());
        }

        [Test]
        public void AppSettings_Should_ReturnConfigurationWithAllPropertiesMapped_When_ValidConfigurationTypeRequested()
        {
            var configuration = _configurationAssist.AppSettings<AppSettingsConfiguration>();

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.TestIntValue, Is.EqualTo(42));
            Assert.That(configuration.TestName, Is.EqualTo("AppSettingTest"));
            Assert.That(configuration.MinimumDiscount, Is.EqualTo(33.33m));
            Assert.That(configuration.NullDouble, Is.Null);
            Assert.That(configuration.MaxFileLength, Is.EqualTo(128000000));
        }
    }
}
