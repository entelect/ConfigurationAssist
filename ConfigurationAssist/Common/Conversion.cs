using System;
using System.ComponentModel;
using System.Configuration;

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

            if (type.BaseType != null && type.BaseType != typeof (object) && type.BaseType != typeof(ValueType))
            {
                return Activator.CreateInstance(type);
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
