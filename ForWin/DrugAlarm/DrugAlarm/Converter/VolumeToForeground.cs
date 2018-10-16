using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DrugAlarm.Converter
{
    /// <summary>
    /// Converter
    /// 薬残量により文字色変更
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    class VolumeToForeground : IValueConverter
    {

        /// <summary>
        /// 薬残量により文字色の変更
        /// </summary>
        /// <param name="value">薬切れFLG</param>
        /// <returns>文字色
        /// 黒：残量十分
        /// 赤：残量不足
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null) return Brushes.Black;

            return (bool)value ? Brushes.Red : Brushes.Black;

        }

        /// <summary>
        /// 使用しない
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
