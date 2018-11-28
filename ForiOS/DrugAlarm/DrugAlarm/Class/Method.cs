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
        public DateTime GetTodayTime(Int32 Hour, Int32 Minute)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);
        }

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <returns>今日日付のDateTime型</returns>
        /// <param name="Time">HH:mm</param>
        public DateTime GetTodayTime(string Time)
        {

            string[] Values = Time.Split(':');

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
        public Int32 ConvertToInt32(string Value, Int32 DefaultValue)
        {
            return Int32.TryParse(Value, out Int32 Return) ? Return : DefaultValue;
        }

        /// <summary>
        /// 文字列を日付型に変換
        /// 変換できない場合はデフォルト値を戻す
        /// </summary>
        /// <returns>The date time.</returns>
        /// <param name="Value">変換文字列</param>
        /// <param name="DefaultValue">デフォルト値</param>
        public DateTime ConvertToDateTime(string Value, DateTime DefaultValue)
        {
            return DateTime.TryParse(Value, out DateTime Return) ? Return : DefaultValue;
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
        public DateTime ConvertToDateTime(Int32 Year, Int32 Month, Int32 Day, Int32 Hour, Int32 Minute, Int32 Second, DateTime DefaultValue)
        {

            StringBuilder Str = new StringBuilder();

            Str.Clear().Append(Year.ToString("0000")).Append("/").Append(Month.ToString()).Append("/").Append(Day.ToString());
            Str.Append(" ").Append(Hour.ToString("00")).Append(":").Append(Minute.ToString("00")).Append(":").Append(Second.ToString("00"));

            DateTime Return = ConvertToDateTime(Str.ToString(), DefaultValue);

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
        public DateTime ConvertToDateTime(Int32 Year, Int32 Month, Int32 Day, Int32 Hour, Int32 Minute, DateTime DefaultValue)
        {
            return ConvertToDateTime(Year, Month, Day, Hour, Minute, 0, DefaultValue);
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
        public DateTime ConvertToDateTime(Int32 Year, Int32 Month, Int32 Day, Int32 Hour, DateTime DefaultValue)
        {
            return ConvertToDateTime(Year, Month, Day, Hour, 0, 0, DefaultValue);
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
        public DateTime ConvertToDateTime(Int32 Year, Int32 Month, Int32 Day, DateTime DefaultValue)
        {
            return ConvertToDateTime(Year, Month, Day, 0, 0, 0, DefaultValue);
        }

        /// <summary>
        /// 改行変換
        /// CRLF → "_CRLF_"
        /// または "_CRLF_" → CRLF
        /// </summary>
        /// <returns>変換後文字列</returns>
        /// <param name="Value">対象文字列</param>
        public string ConvertToCRLF(string Value)
        {

            const string CRLF = "\r\n";
            const string _CRLF_ = "_CRLF_";

            return Value.Contains(CRLF) ? Value.Replace(CRLF, _CRLF_) : Value.Replace(_CRLF_, CRLF);

        }

        /// <summary>
        /// 2つの時間の平均値を取得
        /// </summary>
        /// <returns>The average time.</returns>
        /// <param name="Time1">Time1.</param>
        /// <param name="Time2">Time2.</param>
        public DateTime GetAverageTime(DateTime Time1, DateTime Time2)
        {

            TimeSpan TSpan = Time2 - Time1;

            TSpan = TimeSpan.FromMilliseconds(TSpan.TotalMilliseconds / 2.0);

            return Time1 + TSpan;

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
