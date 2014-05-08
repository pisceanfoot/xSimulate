using System;
using System.ComponentModel;
using System.Globalization;

namespace xSimulate.Util
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class StringConvertTo
    {
        #region ConvertTo Methods

        #region With default culture

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails an exception will be raised. Supply a default value to suppress the exception.
        /// </summary>
        public static T ConvertTo<T>(string value)
        {
            return ConvertTo<T>(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails the <param name="defaultValue">default value</param> will be returned.
        /// </summary>
        public static T ConvertTo<T>(string value, T defaultValue)
        {
            return ConvertTo<T>(value, CultureInfo.InvariantCulture, defaultValue);
        }

        #endregion With default culture

        #region With Culture as string value

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails an exception will be raised. Supply a default value to suppress the exception.
        /// </summary>
        public static T ConvertTo<T>(string value, string culture)
        {
            return ConvertTo<T>(value, CultureInfo.GetCultureInfo(culture));
        }

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails the <param name="defaultValue">default value</param> will be returned.
        /// </summary>
        public static T ConvertTo<T>(string value, string culture, T defaultValue)
        {
            return ConvertTo<T>(value, CultureInfo.GetCultureInfo(culture), defaultValue);
        }

        #endregion With Culture as string value

        #region With StrongTyped Culture

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails an exception will be raised. Supply a default value to suppress the exception.
        /// </summary>
        /// <typeparam name="T">The type to return</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(string value, CultureInfo culture)
        {
            return ConvertTo<T>(value, culture, default(T), true);
        }

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart.
        /// If the conversion fails the <param name="defaultValue">default value</param> will be returned.
        /// </summary>
        public static T ConvertTo<T>(string value, CultureInfo culture, T defaultValue)
        {
            return ConvertTo<T>(value, culture, defaultValue, false);
        }

        /// <summary>
        /// Converts the specified string value to its strong-typed counterpart. If the conversion fails
        /// either an exception is raised or the default value will be returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="raiseException">if set to <c>true</c> [raise exception].</param>
        /// <returns></returns>
        private static T ConvertTo<T>(string value, CultureInfo culture, T defaultValue, bool raiseException)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                //nasty but needed because the BCL is not turning on the right NumberStyle flags
                if (converter is BaseNumberConverter)
                {
                    return (T)HandleThousandsSeparatorIssue<T>(converter, value, culture);
                }
                if (converter is BooleanConverter)
                    return (T)HandleBooleanValues(converter, value, culture);
                else
                {
                    return (T)converter.ConvertFromString(null, culture, value);
                }
            }
            catch
            {
                if (raiseException)
                    throw;

                return defaultValue;
            }
        }

        /// <summary>
        /// The BooleanTypeConverter is only able to handle True and False strings. With this function we are able to
        /// handle other frequently used values as well eg. Yes, No, On, Off, 0 and 1.
        /// </summary>
        /// <param name="converter">If the provided string cannot be translated, we'll hand it back to the type converter.</param>
        /// <param name="value">The string value which represents a boolean value.</param>
        private static object HandleBooleanValues(TypeConverter converter, string value, CultureInfo culture)
        {
            string[] trueValues = { "true", "yes", "y", "on", "1" };
            string[] falseValues = { "false", "no", "n", "off", "0" };

            if (!string.IsNullOrEmpty(value))
            {
                string tmpValue = value.ToLower();
                foreach (string t in trueValues)
                {
                    if (t == tmpValue)
                        return true;
                }
                foreach (string f in falseValues)
                {
                    if (f == tmpValue)
                        return false;
                }
            }

            return converter.ConvertFromString(null, culture, value);
        }

        /// <remarks>
        /// The Parse methods on the individual number classes are much smarter then their type converter
        /// counterparts. See: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/c980b925-6df5-428d-bf87-7ff83db4504c/
        /// </remarks>
        private static object HandleThousandsSeparatorIssue<T>(TypeConverter converter, string value, CultureInfo culture)
        {
            NumberStyles styles = NumberStyles.Number;
            IFormatProvider format = culture.NumberFormat;

            #region Double

            if (typeof(T).Equals(typeof(double)))
            {
                return double.Parse(value, styles, format);
            }

            #endregion Double

            #region Decimal

            if (typeof(T).Equals(typeof(decimal)))
            {
                return decimal.Parse(value, styles, format);
            }

            #endregion Decimal

            #region Single

            if (typeof(T).Equals(typeof(Single)))
            {
                return decimal.Parse(value, styles, format);
            }

            #endregion Single

            #region Int16 & UInt16

            if (typeof(T).Equals(typeof(Int16)))
            {
                return Int16.Parse(value, styles, format);
            }

            if (typeof(T).Equals(typeof(UInt16)))
            {
                return UInt16.Parse(value, styles, format);
            }

            #endregion Int16 & UInt16

            #region Int32 & UInt32

            if (typeof(T).Equals(typeof(Int32)))
            {
                return Int32.Parse(value, styles ^ NumberStyles.AllowDecimalPoint, format);
            }
            if (typeof(T).Equals(typeof(UInt32)))
            {
                return UInt32.Parse(value, styles ^ NumberStyles.AllowDecimalPoint, format);
            }

            #endregion Int32 & UInt32

            #region Int64 & UInt64

            if (typeof(T).Equals(typeof(Int64)))
            {
                return Int64.Parse(value, styles ^ NumberStyles.AllowDecimalPoint, format);
            }
            if (typeof(T).Equals(typeof(UInt64)))
            {
                return UInt64.Parse(value, styles ^ NumberStyles.AllowDecimalPoint, format);
            }

            #endregion Int64 & UInt64

            //Last chance. Fallback on the TypeConverters if we haven't choosen to provide a different
            //method to parse the string.
            return (T)converter.ConvertFromString(null, culture, value);
        }

        #endregion With StrongTyped Culture

        #endregion ConvertTo Methods
    }
}