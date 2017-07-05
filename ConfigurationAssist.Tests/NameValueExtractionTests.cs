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
    public class NameValueExtractionTests
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
        public void ExtractSettings_Should_ReturnValidConfiguration_When_UsingKeyValueCollectionSectionType()
        {
            var configuration = _configurationAssist.ExtractSettings<ValueKeySectionConfiguration>(new NameValueHandlerSectionExtractionStrategy());

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("MyName"));
            Assert.That(configuration.Value, Is.EqualTo(23.4m));
            Assert.That(configuration.LongValueDefault, Is.EqualTo(0));
            Assert.That(configuration.LongValueSpecifiedDefault, Is.EqualTo(1234567890));
            Assert.That(configuration.LongValueDefaultNull, Is.Null);
        }

        [Test]
        [Description("Factory should pick up the correct extraction method and return the correct extraction results")]
        public void ExtractSettings_Should_Return_SingleTagSectionConfiguration_When_NoStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<ValueKeySectionConfiguration>();

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("MyName"));
            Assert.That(configuration.Value, Is.EqualTo(23.4m));
            Assert.That(configuration.LongValueDefault, Is.EqualTo(0));
            Assert.That(configuration.LongValueSpecifiedDefault, Is.EqualTo(1234567890));
            Assert.That(configuration.LongValueDefaultNull, Is.Null);
        }
    }
}
