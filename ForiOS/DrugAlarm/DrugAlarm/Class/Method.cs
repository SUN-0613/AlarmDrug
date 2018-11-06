using System;

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
        public DateTime ConvertDateTime(string Value, DateTime DefaultValue)
        {
            return DateTime.TryParse(Value.Split(':').Length != 2 ? Value + ":00" : Value, out DateTime Return) ? Return : DefaultValue;
        }

        /// <summary>
        /// 改行変換
        /// CRLF → "_CRLF_"
        /// または "_CRLF_" → CRLF
        /// </summary>
        /// <returns>変換後文字列</returns>
        /// <param name="Value">対象文字列</param>
        public string ConvertCRLF(string Value)
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

    }
}
