using System;
using System.ComponentModel;

namespace xSimulate.Util
{
    public static class Converter
    {
        public static T ToType<T>(object value)
        {
            return (T)ToType(typeof(T), value);
        }

        public static object ToType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }
    }
}