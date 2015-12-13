using System.Configuration;
using System.Linq;

namespace ConfigurationAssist.Common
{
    public class ConfigHelperService
    {
        private readonly Conversion _converter = new Conversion();
 
        public T CreateSettingsWithDefaults<T>() where T : class, new()
        {
            var setting = new T();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(ConfigurationPropertyAttribute), true);
                if (!attribute.Any())
                {
                    continue;
                }

                var configurationProperty = (ConfigurationPropertyAttribute)attribute.First();
                var convertedDefault = _converter.Convert(property.PropertyType, configurationProperty.DefaultValue);
                property.SetValue(setting, convertedDefault, null);
            }

            return setting;
        }
    }
}
