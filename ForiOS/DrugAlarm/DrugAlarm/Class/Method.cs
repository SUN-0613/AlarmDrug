using System;
using System.Collections.Generic;
using System.Text;

namespace DrugAlarm.Class
{

    /// <summary>
    /// メソッドクラス
    /// </summary>
    public class Method
    {

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <returns>今日日付のDateTime型</returns>
        /// <param name="Hour">時</param>
        /// <param name="Minute">分</param>
        public DateTime GetTodayTime(Int32 hour, Int32 minute)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
        }

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <returns>今日日付のDateTime型</returns>
        /// <param name="Time">HH:mm</param>
        public DateTime GetTodayTime(string time)
        {

            string[] Values = time.Split(':');

            if (!Int32.TryParse(Values[0], out Int32 Hour))
            {
                Hour = 0;
            }

            if (!Int32.TryParse(Values[1], out Int32 Minute))
            {
                Minute = 0;
            }

            return GetTodayTime(Hour, Minute);

        }

        /// <summary>
        /// 文字列を整数値に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The to int32.</returns>
        /// <param name="Value">変換文字列</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public Int32 ConvertToInt32(string value, Int32 defaultValue)
        {
            return Int32.TryParse(value, out Int32 Return) ? Return : defaultValue;
        }

        /// <summary>
        /// 文字列を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Value">変換文字列</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(string value, DateTime defaultValue)
        {
            return DateTime.TryParse(value, out DateTime Return) ? Return : defaultValue;
        }

        /// <summary>
        /// 数値を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <param name="Day">日</param>
        /// <param name="Hour">時</param>
        /// <param name="Minute">分</param>
        /// <param name="Second">秒</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second, DateTime defaultValue)
        {

            StringBuilder Str = new StringBuilder();

            Str.Clear().Append(year.ToString("0000")).Append("/").Append(month.ToString()).Append("/").Append(day.ToString());
            Str.Append(" ").Append(hour.ToString("00")).Append(":").Append(minute.ToString("00")).Append(":").Append(second.ToString("00"));

            DateTime Return = ConvertToDateTime(Str.ToString(), defaultValue);

            Str.Clear();
            Str = null;

            return Return;

        }

        /// <summary>
        /// 数値を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <param name="Day">日</param>
        /// <param name="Hour">時</param>
        /// <param name="Minute">分</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, DateTime defaultValue)
        {
            return ConvertToDateTime(year, month, day, hour, minute, 0, defaultValue);
        }

        /// <summary>
        /// 数値を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <param name="Day">日</param>
        /// <param name="Hour">時</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(Int32 year, Int32 month, Int32 day, Int32 hour, DateTime defaultValue)
        {
            return ConvertToDateTime(year, month, day, hour, 0, 0, defaultValue);
        }

        /// <summary>
        /// 数値を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Year">年</param>
        /// <param name="Month">月</param>
        /// <param name="Day">日</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(Int32 year, Int32 month, Int32 day, DateTime defaultValue)
        {
            return ConvertToDateTime(year, month, day, 0, 0, 0, defaultValue);
        }

        /// <summary>
        /// 文字列を判定型に変換、変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns><c>true</c>, if to boolean was converted, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        /// <param name="defaultValue">If set to <c>true</c> default value.</param>
        public bool ConvertToBoolean(string value, bool defaultValue)
        {
            return bool.TryParse(value, out bool Return) ? Return : defaultValue;
        }

        /// <summary>
        /// 数値を判定型に変換、変換できない場合はデフォルト値を戻す
        /// 0:false
        /// 1, -1:true
        /// 他:デフォルト値
        /// </summary>
        /// <returns><c>true</c>, if to boolean was converted, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        /// <param name="defaultValue">If set to <c>true</c> default value.</param>
        public bool ConvertToBoolean(Int32 value, bool defaultValue)
        {
            if (value.Equals(0))
            {
                return false;
            }
            else if (value.Equals(1) || value.Equals(-1))
            {
                return true;
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 改行変換
        /// CRLF → "_CRLF_"
        /// または "_CRLF_" → CRLF
        /// </summary>
        /// <returns>変換後文字列</returns>
        /// <param name="Value">対象文字列</param>
        public string ConvertToCRLF(string value)
        {

            const string CRLF = "\r\n";
            const string _CRLF_ = "_CRLF_";

            return value.Contains(CRLF) ? value.Replace(CRLF, _CRLF_) : value.Replace(_CRLF_, CRLF);

        }

        /// <summary>
        /// 文章に日付を埋め、改行変換
        /// </summary>
        /// <returns>The to message.</returns>
        /// <param name="message">Message.</param>
        /// <param name="dateTime">Date time.</param>
        public string ConvertToMessage(string message, DateTime dateTime)
        {

            const string TIME = "_TIME_";

            return ConvertToCRLF(message.Replace(TIME, dateTime.ToString(UserControl.DateTimeFormat)));

        }

        /// <summary>
        /// 2つの時間の平均値を取得
        /// </summary>
        /// <returns>The average time.</returns>
        /// <param name="Time1">Time1.</param>
        /// <param name="Time2">Time2.</param>
        public DateTime GetAverageTime(DateTime time1, DateTime time2)
        {

            TimeSpan TSpan = time2 - time1;

            TSpan = TimeSpan.FromMilliseconds(TSpan.TotalMilliseconds / 2.0);

            return time1 + TSpan;

        }

        /// <summary>
        /// Listよりvalue値となるIndexを取得
        /// 該当値がListより見つからない場合、Defaultを戻す
        /// </summary>
        /// <returns>該当Index</returns>
        /// <param name="list">検索対象List</param>
        /// <param name="value">検索対象値</param>
        /// <param name="defaultValue">デフォルト値</param>
        public Int32 GetDefaultIndex(List<Int32> list, Int32 value, Int32 defaultValue = 0)
        {

            Int32 Return = list.IndexOf(value);

            if (Return.Equals(-1))
            {
                Return = defaultValue;
            }

            return Return;

        }

        /// <summary>
        /// Listが作成されていて、Indexがちゃんと設定されているか
        /// </summary>
        /// <returns><c>true</c>OK<c>false</c>NG</returns>
        /// <param name="list">対象List</param>
        /// <param name="Index">設定されているIndex</param>
        public bool IsOkListStatus(List<Int32> list, Int32 Index)
        {
            return (list != null) && (-1 < Index && Index < list.Count);
        }

    }
}
