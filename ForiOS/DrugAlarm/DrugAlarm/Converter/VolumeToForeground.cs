using System;
using System.Globalization;
using Xamarin.Forms;

namespace DrugAlarm.Converter
{

    /// <summary>
    /// Converter
    /// 薬残量により文字色の変更
    /// </summary>
    public class VolumeToForeground : IValueConverter
    {

        /// <summary>
        /// 薬残量により文字色の変更
        /// </summary>
        /// <returns>文字色
        /// 黒：残量十分
        /// 赤：残量不足
        /// </returns>
        /// <param name="value">薬切れFLG</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Color.Black;
            return (bool)value ? Color.Red : Color.Black;
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
