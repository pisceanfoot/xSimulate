using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
