using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DrugAlarm.Base
{
    class VolumeToForeground : IMultiValueConverter
    {

        /// <summary>
        /// 薬残量により文字色の変更
        /// </summary>
        /// <param name="values">
        /// 0:薬残量
        /// 1:薬切れアラーム残量
        /// </param>
        /// <returns>文字色
        /// 黒：残量十分
        /// 赤：残量不足
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (Int32.Parse(values[0].ToString()) <= (Int32.Parse(values[1].ToString())))
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
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
