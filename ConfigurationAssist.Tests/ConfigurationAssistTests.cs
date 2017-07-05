using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using ConfigurationAssist.ConfigurationExtractionStrategies;
using ConfigurationAssist.Interfaces;
using ConfigurationAssist.Tests.Configuration;
using NUnit.Framework;

namespace ConfigurationAssist.Tests
{
    [TestFixture, Category(TestCategory.Unit)]
    public class ConfigurationAssistTests
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
        public void TryExtractSettings_Should_ReturnDefaultObject_When_ExceptionThrownInExtraction()
        {
            //First we test our fail scenario so we know our success scenario works correctly
            Assert.Throws<ConfigurationErrorsException>(() => _configurationAssist.ExtractSettings<FailConfigSection>(new SingleTagSectionHandlerExtractionStrategy()));

            var settings = _configurationAssist.TryExtractSettings<FailConfigSection>(new SingleTagSectionHandlerExtractionStrategy());
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Value, Is.EqualTo(34.12));
            Assert.That(settings.BaseValue, Is.EqualTo(0));
        }

        [Test]
        public void TryExtractSettings_Should_ReturnDefaultObject_When_ExceptionThrownInExtractionAndNoStrategySpecified()
        {
            //First we test our fail scenario so we know our success scenario works correctly
            Assert.Throws<ConfigurationErrorsException>(() => _configurationAssist.ExtractSettings<FailConfigSection>(new SingleTagSectionHandlerExtractionStrategy()));

            var settings = _configurationAssist.TryExtractSettings<FailConfigSection>(new SingleTagSectionHandlerExtractionStrategy());
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Value, Is.EqualTo(34.12));
            Assert.That(settings.BaseValue, Is.EqualTo(0));
        }
    }
}
