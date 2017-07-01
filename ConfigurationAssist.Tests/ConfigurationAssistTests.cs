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
                //System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = ".";

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
        public void AppSettings_Should_ReturnConfigurationWithAllPropertiesMapped_When_ValidConfigurationTypeRequested()
        {
            var configuration = _configurationAssist.ExtractSettings<AppSettingsConfiguration>();

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.TestIntValue, Is.EqualTo(42));
            Assert.That(configuration.TestName, Is.EqualTo("AppSettingTest"));
            Assert.That(configuration.MinimumDiscount, Is.EqualTo(33.33m));
            Assert.That(configuration.NullDouble, Is.Null);
            Assert.That(configuration.MaxFileLength, Is.EqualTo(128000000));
        }

        [Test]
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
        public void ExtractSettings_Should_Return_DictionarySectionConfiguration_When_DictionarySectionHandlerStrategyUsed()
        {
            var configuration = _configurationAssist.ExtractSettings<DictionarySectionConfiguration>(new DictionarySectionHandlerExtractionStrategy());

            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.Name, Is.EqualTo("DictionaryTest"));
        }

        [Test]
        public void ExtractSettings_Should_Return_SingleTagSectionConfiguration_When_SingleTagSectionHandlerStrategyUsed()
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
        public void ExtractSettings_Should_Return_AppSettingsConfiguration_When_AppSettingsExtractionStrategyUsed()
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
        public void ExtractSettings_Should_ReturnSettings_When_NoParametersPassedIn()
        {
            var appSettings = _configurationAssist.ExtractSettings<AppSettingsConfiguration>();
            Assert.That(appSettings, Is.Not.Null);
            Assert.That(appSettings.TestIntValue, Is.EqualTo(42));
            Assert.That(appSettings.TestName, Is.EqualTo("AppSettingTest"));
            Assert.That(appSettings.MinimumDiscount, Is.EqualTo(33.33m));
            Assert.That(appSettings.NullDouble, Is.Null);
            Assert.That(appSettings.MaxFileLength, Is.EqualTo(128000000));

            var dictionarySettings = _configurationAssist.ExtractSettings<DictionarySectionConfiguration>();
            Assert.That(dictionarySettings, Is.Not.Null);
            Assert.That(dictionarySettings.Name, Is.EqualTo("DictionaryTest"));

            var singleTagSettings = _configurationAssist.ExtractSettings<SingleTagSectionConfiguration>();
            Assert.That(singleTagSettings, Is.Not.Null);
            Assert.That(singleTagSettings.Name, Is.EqualTo("SingleTagTest"));
        }

        [Test] 
        public void ExtractSettings_Should_ThrowException_When_TraditionalConfigurationSectionObjectSpecified()
        {
            Assert.Throws<NotSupportedException>(() => _configurationAssist.ExtractSettings<TestConfiguration>());
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

        [Test]
        public void ExtractSettings_Should_ReturnConfiguration_When_OnlySectionGroupSpecifiedAndSectionNameSameAsClass()
        {
            var settings = _configurationAssist.ExtractSettings<TestSectionOnlyGroupSpecified>();
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Name, Is.EqualTo("TestName"));
            Assert.That(settings.Value, Is.EqualTo("TestValue"));
        }

        [Test]
        public void ExtractSettings_Should_ReturnComplexSetting_When_ComplexSettingsSpecified()
        {
            var settings = _configurationAssist.ExtractSettings<ComplexSettings>();
            Assert.That(settings, Is.Not.Null);
            Assert.That(settings.Name, Is.Not.Empty);
            Assert.That(settings.Sub, Is.Not.Null);
        }

        [Test]
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
    }
}
