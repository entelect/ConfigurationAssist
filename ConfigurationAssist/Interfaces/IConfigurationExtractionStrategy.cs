namespace ConfigurationAssist.Interfaces
{
    public interface IConfigurationExtractionStrategy
    {
        T ExtractConfiguration<T>() where T : class, new();
    }
}
