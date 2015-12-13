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

            try
            {
                return converter.ConvertFromString(input.ToString());
            }
            catch
            {
                return null;
            }
        }
    }
}
