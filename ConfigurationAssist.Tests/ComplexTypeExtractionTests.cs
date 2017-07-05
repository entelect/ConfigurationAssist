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
    public class ComplexTypeExtractionTests
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
        public void ExtractSettings_Should_Return_ComplexType_When_ComplexTypeSectionHandlerSpecified()
        {
            var settings = _configurationAssist.ExtractSettings<ComplexSettings>(new ComplexTypeExtractionStrategy());
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Name, Is.Not.Null);
            Assert.That(settings.Name, Is.EqualTo("Test Complex Settings"));
            Assert.That(settings.Sub, Is.Not.Null);
            Assert.That(settings.Sub.ObjectName, Is.EqualTo("Sub Object Name"));
            Assert.That(settings.KeyValue, Has.Length.GreaterThan(0));
            Assert.That(settings.KeyValue[2].Value, Is.EqualTo(3));
        }

        [Test]
        [Description("Factory should pick up the correct extraction method and return the correct extraction results")]
        public void ExtractSettings_Should_ReturnComplexSetting_When_ComplexSettingsSpecified()
        {
            var settings = _configurationAssist.ExtractSettings<ComplexSettings>();
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Name, Is.Not.Null);
            Assert.That(settings.Name, Is.EqualTo("Test Complex Settings"));
            Assert.That(settings.Sub, Is.Not.Null);
            Assert.That(settings.Sub.ObjectName, Is.EqualTo("Sub Object Name"));
            Assert.That(settings.KeyValue, Has.Length.GreaterThan(0));
            Assert.That(settings.KeyValue[2].Value, Is.EqualTo(3));
        }
    }
}
