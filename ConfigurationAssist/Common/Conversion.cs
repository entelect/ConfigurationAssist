using System;
using System.ComponentModel;

namespace ConfigurationAssist.Common
{
    public class Conversion
    {
        public object Convert(Type type, object input)
        {
            var converter = TypeDescriptor.GetConverter(type);

            if (input == null)
            {
                return null;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return null;
            }

            if (type.BaseType != null && type.BaseType != typeof(object) && type.BaseType != typeof(ValueType))
            {
                return Activator.CreateInstance(type);
            }

            if (type.BaseType != null && type.BaseType == typeof(object) && input.ToString() == typeof(object).ToString())
            {
                return converter.ConvertFromString(string.Empty);
            }

            try
            {
                string value = input.ToString();
                return converter.ConvertFromString(value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
