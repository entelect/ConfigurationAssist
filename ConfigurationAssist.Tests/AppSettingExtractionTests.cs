using System;
using System.Globalization;
using System.Threading;
using ConfigurationAssist.ConfigurationExtractionStrategies;
using ConfigurationAssist.Interfaces;
using ConfigurationAssist.Tests.Configuration;
using NUnit.Framework;

namespace ConfigurationAssist.Tests
{
    [TestFixture, Category(TestCategory.Unit)]
    public class AppSettingExtractionTests
    {
        IConfigurationAssist _configurationAssist;

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            try
            {
                var cultureInfo = new CultureInfo("EN-ZA");
                cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                cultureInfo.NumberFormat.PercentDecimalSeparator = ".";
                cultureInfo.NumberFormat.NumberGroupSeparator = "";
                cultureInfo.NumberFormat.CurrencyGroupSeparator = "";
                cultureInfo.NumberFormat.PercentGroupSeparator = "";

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                _configurationAssist = new ConfigurationAssist();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Test]
        [Description("Extraction method specified and the correct results should be returned")]
        public void ExtractSettings_Should_Return_AppSettingsConfiguration_When_AppSettingsExtractionStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<AppSettingsConfiguration>(new AppSettingExtractionStrategy());

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.TestIntValue, Is.EqualTo(42));
            Assert.That(configuration.TestName, Is.EqualTo("AppSettingTest"));
            Assert.That(configuration.MinimumDiscount, Is.EqualTo(33.33m));
            Assert.That(configuration.NullDouble, Is.Null);
            Assert.That(configuration.MaxFileLength, Is.EqualTo(128000000));
        }

        [Test]
        [Description("Factory should pick up the correct extraction method and return the correct extraction results")]
        public void ExtractSettings_Should_Return_AppSettingsConfiguration_When_NoStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<AppSettingsConfiguration>();

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.TestIntValue, Is.EqualTo(42));
            Assert.That(configuration.TestName, Is.EqualTo("AppSettingTest"));
            Assert.That(configuration.MinimumDiscount, Is.EqualTo(33.33m));
            Assert.That(configuration.NullDouble, Is.Null);
            Assert.That(configuration.MaxFileLength, Is.EqualTo(128000000));
        }
    }
}
