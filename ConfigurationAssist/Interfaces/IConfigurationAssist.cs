namespace ConfigurationAssist.Interfaces
{
    public interface IConfigurationAssist
    {
        T ExtractSettings<T>(IConfigurationExtractionStrategy extractionStrategy) where T : class, new();
        T ExtractSettings<T>() where T : class, new();
        T TryExtractSettings<T>(IConfigurationExtractionStrategy extractionStrategy) where T : class, new();
        T TryExtractSettings<T>() where T : class, new();
    }
}
