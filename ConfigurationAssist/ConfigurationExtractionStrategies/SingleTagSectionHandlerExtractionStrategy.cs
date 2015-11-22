using ConfigurationAssist.Interfaces;

namespace ConfigurationAssist.ConfigurationExtractionStrategies
{
    public class SingleTagSectionHandlerExtractionStrategy : IConfigurationExtractionStrategy
    {
        public SingleTagSectionHandlerExtractionStrategy(string fullSectionName) 
        {
            FullSectionName = fullSectionName;
        }

        public SingleTagSectionHandlerExtractionStrategy()
        {
        }

        public string FullSectionName { get; set; }

        public T ExtractConfiguration<T>() where T : class, new()
        {
            var dictionaryExtractionStrategy = new DictionarySectionHandlerExtractionStrategy(FullSectionName);
            return dictionaryExtractionStrategy.ExtractConfiguration<T>();
        }
    }
}
