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
    public class SingleTagExtractionTests
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
        public void ExtractSettings_Should_Return_SingleTagSectionConfiguration_When_SingleTagSectionHandlerStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<SingleTagSectionConfiguration>(new SingleTagSectionHandlerExtractionStrategy());

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("SingleTagTest"));
            Assert.That(configuration.DecimalValue, Is.EqualTo(12.4m));
            Assert.That(configuration.DoubleValue, Is.EqualTo(123.43d));
            Assert.That(configuration.IntValue, Is.EqualTo(1));
            Assert.That(configuration.LongValue, Is.EqualTo(23L));
            Assert.That(configuration.StringValue, Is.EqualTo("Test"));
            Assert.That(configuration.NotConfigured, Is.Empty);
            Assert.That(configuration.NotConfiguredNullableInt, Is.Null);
        }

        [Test]
        [Description("Factory should pick up the correct extraction method and return the correct extraction results")]
        public void ExtractSettings_Should_Return_SingleTagSectionConfiguration_When_NoStrategySpecified()
        {
            var configuration = _configurationAssist.ExtractSettings<SingleTagSectionConfiguration>();
            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("SingleTagTest"));
            Assert.That(configuration.DecimalValue, Is.EqualTo(12.4m));
            Assert.That(configuration.DoubleValue, Is.EqualTo(123.43d));
            Assert.That(configuration.IntValue, Is.EqualTo(1));
            Assert.That(configuration.LongValue, Is.EqualTo(23L));
            Assert.That(configuration.StringValue, Is.EqualTo("Test"));
            Assert.That(configuration.NotConfigured, Is.Empty);
            Assert.That(configuration.NotConfiguredNullableInt, Is.Null);
        }

        [Test]
        [Description("The settings should extract from a group if only the group is specifed in the class, and the class name is the same as the section")]
        public void ExtractSettings_Should_Return_TestSectionOnlyGroupSpecified_When_OnlySectionGroupSpecifiedAndSectionNameSameAsClass()
        {
            var settings = _configurationAssist.ExtractSettings<TestSectionOnlyGroupSpecified>();
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Name, Is.EqualTo("TestName"));
            Assert.That(settings.Value, Is.EqualTo("TestValue"));
        }
    }
}
