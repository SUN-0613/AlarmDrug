using System;
using System.Globalization;
using Xamarin.Forms;

namespace DrugAlarm.Converter
{

    /// <summary>
    /// Converter
    /// Enum ⇔ bool
    /// </summary>
    public class EnumToBool : IValueConverter
    {

        /// <summary>
        /// Enum ⇒ bool
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            return value.ToString().Equals(parameter.ToString());
        }

        /// <summary>
        /// bool ⇒ Enum
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value == null || parameter == null) || !(bool)value) return null;
            return Enum.Parse(targetType, parameter.ToString());
        }
    
    }
}
