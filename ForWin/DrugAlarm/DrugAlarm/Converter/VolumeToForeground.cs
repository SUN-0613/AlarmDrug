using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DrugAlarm.Converter
{
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

            if (bool.Parse(value.ToString()))
            {
                return Colors.Red;
            }
            else
            {
                return Colors.Black;
            }

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
