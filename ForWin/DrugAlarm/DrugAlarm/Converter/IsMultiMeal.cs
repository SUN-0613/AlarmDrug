using System;
using System.Globalization;
using System.Windows.Data;

namespace DrugAlarm.Converter
{

    /// <summary>
    /// Converter
    /// bools ⇔ bool
    /// </summary>
    class IsMultiMeal : IMultiValueConverter
    {

        /// <summary>
        /// bools ⇒ bool
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>何れか1つでもTrueがあればTrueを返す</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            Nullable<bool> Value;
            
            for (Int32 iLoop = 0; iLoop < values.Length; iLoop++)
            {

                Value = (bool)values[iLoop];

                if (!Value.HasValue) return false;

                if (Value.Value) return true;

            }

            return false;

        }

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
