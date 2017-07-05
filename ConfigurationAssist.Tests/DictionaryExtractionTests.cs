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
    public class DictionaryExtractionTests
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
        [Description("Extraction method specidied and the correct results should be returned")]
        public void ExtractSettings_Should_Return_DictionarySectionConfiguration_When_DictionarySectionHandlerStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<DictionarySectionConfiguration>(new DictionarySectionHandlerExtractionStrategy());

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("DictionaryTest"));
        }

        [Test]
        [Description("Factory should pick up the correct extraction method and return the correct extraction results")]
        public void ExtractSettings_Should_Return_DictionarySectionConfiguration_When_NoStrategySpecified()
        {
            var dictionarySettings = _configurationAssist.ExtractSettings<DictionarySectionConfiguration>();
            Assert.That(dictionarySettings, Is.Not.Null);
            Assert.That(dictionarySettings.Name, Is.EqualTo("DictionaryTest"));
        }
    }
}
