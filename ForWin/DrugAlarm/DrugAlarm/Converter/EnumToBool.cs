using System;
using System.Globalization;
using System.Windows.Data;

namespace DrugAlarm.Converter
{
    /// <summary>
    /// Converter
    /// Enum ⇔ bool
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(bool))]
    class EnumToBool : IValueConverter
    {

        /// <summary>
        /// Enum ⇒ bool
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null || parameter == null) return false;

            return value.ToString().Equals(parameter.ToString());

        }

        /// <summary>
        /// bool ⇒ Enum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((value == null || parameter == null) || !(bool)value) return Binding.DoNothing;

            return Enum.Parse(targetType, parameter.ToString());

        }
    }
}
