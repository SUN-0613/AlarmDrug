using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugAlarm.Class
{

    /// <summary>
    /// 設定パラメータ
    /// </summary>
    public class Parameter
    {

        /// <summary>
        /// 範囲時間
        /// </summary>
        public struct BetweenTime
        {

            /// <summary>
            /// 開始時間
            /// </summary>
            public DateTime Start;

            /// <summary>
            /// 終了時間
            /// </summary>
            public DateTime Finish;

        }

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <param name="Hour">時</param>
        /// <param name="Minute">分</param>
        /// <returns></returns>
        private static DateTime GetTodayTime(Int32 Hour, Int32 Minute)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);
        }

        /// <summary>
        /// 次回時間の取得
        /// </summary>
        /// <param name="Time">設定時間</param>
        private static DateTime GetNextDateTime(DateTime Time)
        {

            DateTime Now = DateTime.Now;
            DateTime Return = new DateTime(Now.Year, Now.Month, Now.Day, Time.Hour, Time.Minute, 0);

            if (Time.Hour < Now.Hour || (Time.Hour.Equals(Now.Hour) && Time.Minute < Now.Minute))
            {
                Return.AddDays(1.0);
            }

            return Return;

        }

        /// <summary>
        /// 設定クラス
        /// </summary>
        public class SettingParameter
        {

            /// <summary>
            /// パラメータ名：設定データ開始
            /// </summary>
            public const string SETTING_START = "SettingStart";

            /// <summary>
            /// パラメータ名：設定データ終了
            /// </summary>
            public const string SETTING_END = "SettingEnd";

            /// <summary>
            /// 設定データ読込・書込中
            /// </summary>
            public bool IsAccess;

            /// <summary>
            /// 朝食時間範囲
            /// </summary>
            public BetweenTime Breakfast;

            /// <summary>
            /// 昼食時間範囲
            /// </summary>
            public BetweenTime Lunch;

            /// <summary>
            /// 夕食時間範囲
            /// </summary>
            public BetweenTime Dinner;

            /// <summary>
            /// 就寝時間
            /// </summary>
            public DateTime Sleep;

            /// <summary>
            /// お知らせを表示する薬残量
            /// </summary>
            public Int32 PrescriptionAlarmVolume;

            /// <summary>
            /// 食前時間(分)
            /// </summary>
            public Int32 MinuteBeforeMeals;

            /// <summary>
            /// 食間時間(分)
            /// </summary>
            public Int32 MinuteBetweenMeals;

            /// <summary>
            /// 食後時間(分)
            /// </summary>
            public Int32 MinuteAfterMeals;

            /// <summary>
            /// 就寝前時間(分)
            /// </summary>
            public Int32 MinuteBeforeSleep;
            
            /// <summary>
            /// 再通知時間(分)
            /// </summary>
            public Int32 MinuteReAlarm;

            /// <summary>
            /// 初期値設定
            /// </summary>
            public SettingParameter()
            {

                IsAccess = false;
                Breakfast.Start = GetTodayTime(6, 0);
                Breakfast.Finish = GetTodayTime(6, 30);
                Lunch.Start = GetTodayTime(12, 0);
                Lunch.Finish = GetTodayTime(12, 30);
                Dinner.Start = GetTodayTime(18, 0);
                Dinner.Finish = GetTodayTime(18, 30);
                Sleep = GetTodayTime(23, 0);
                PrescriptionAlarmVolume = 0;
                MinuteBeforeMeals = 30;
                MinuteBetweenMeals = 30;
                MinuteAfterMeals = 30;
                MinuteBeforeSleep = 30;
                MinuteReAlarm = 30;

            }

        }

        /// <summary>
        /// 薬クラス
        /// </summary>
        public class DrugParameter
        {

            /// <summary>
            /// パラメータ名：設定データ開始
            /// </summary>
            public const string DRUG_START = "DrugStart";

            /// <summary>
            /// パラメータ名：設定データ終了
            /// </summary>
            public const string DRUG_END = "DrugEnd";

            /// <summary>
            /// 改行(\r\n)の置換文字
            /// </summary>
            public const string CRLF = "_CRLF_";

            /// <summary>
            /// 服用時間
            /// </summary>
            public enum KindTiming
            {
                /// <summary>
                /// 指定なし
                /// </summary>
                None,
                /// <summary>
                /// 食前
                /// </summary>
                Before,
                /// <summary>
                /// 食間
                /// </summary>
                Between,
                /// <summary>
                /// 食後
                /// </summary>
                After,
                /// <summary>
                /// 指定日時
                /// </summary>
                Appoint,
                /// <summary>
                /// 時間毎
                /// </summary>
                TimeEach
            }

            /// <summary>
            /// 服用情報
            /// </summary>
            public struct Timing
            {

                /// <summary>
                /// 服用するか
                /// </summary>
                bool IsDrug;

                /// <summary>
                /// 服用時間
                /// </summary>
                KindTiming Kind;

                /// <summary>
                /// 何錠
                /// </summary>
                Int32 Volume;

                /// <summary>
                /// 初期値設定
                /// </summary>
                /// <param name="IsDrug">服用するか</param>
                /// <param name="Kind">服用時間</param>
                /// <param name="Volume">何錠</param>
                public void Initialize(bool IsDrug, KindTiming Kind, Int32 Volume)
                {
                    this.IsDrug = IsDrug;
                    this.Kind = Kind;
                    this.Volume = Volume;
                }


            }

            /// <summary>
            /// 設定データ読込・書込中
            /// </summary>
            public bool IsAccess;

            /// <summary>
            /// 名称
            /// </summary>
            public string Name;

            /// <summary>
            /// 朝食
            /// </summary>
            public Timing Breakfast;

            /// <summary>
            /// 昼食
            /// </summary>
            public Timing Lunch;

            /// <summary>
            /// 夕食
            /// </summary>
            public Timing Dinner;

            /// <summary>
            /// 就寝
            /// </summary>
            public Timing Sleep;

            /// <summary>
            /// 頓服
            /// </summary>
            public Timing ToBeTaken;

            /// <summary>
            /// 指定時間
            /// </summary>
            public DateTime AppointTime;

            /// <summary>
            /// 時間毎(時)
            /// </summary>
            public Int32 HourEach;

            /// <summary>
            /// 処方量
            /// </summary>
            public Int32 TotalVolume;

            /// <summary>
            /// 備考
            /// </summary>
            public string Remarks;

            /// <summary>
            /// 初期値設定
            /// </summary>
            public DrugParameter()
            {

                IsAccess = false;
                Name = "";
                Breakfast.Initialize(false, KindTiming.None, 0);
                Lunch.Initialize(false, KindTiming.None, 0);
                Dinner.Initialize(false, KindTiming.None, 0);
                Sleep.Initialize(false, KindTiming.Before, 0);
                ToBeTaken.Initialize(false, KindTiming.None, 0);
                AppointTime = new DateTime(1900, 1, 1, 0, 0, 0);
                HourEach = 0;
                TotalVolume = 0;
                Remarks = "";

            }

        }

        /// <summary>
        /// 設定データ
        /// </summary>
        public SettingParameter Setting;

        /// <summary>
        /// 薬リスト
        /// </summary>
        public List<DrugParameter> DrugList;

        /// <summary>
        /// new
        /// </summary>
        public Parameter()
        {

            Setting = new SettingParameter();
            DrugList = new List<DrugParameter>();

        }





    }

}
