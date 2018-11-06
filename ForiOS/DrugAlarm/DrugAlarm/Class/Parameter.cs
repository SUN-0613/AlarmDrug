using System;
using System.Collections.Generic;

namespace DrugAlarm.Class
{

    /// <summary>
    /// 設定パラメータ
    /// </summary>
    public class Parameter : IDisposable
    {

        /// <summary>
        /// 次回アラームプロパティ
        /// </summary>
        /// <value>The next alarm.</value>
        public UserControl.AlarmInfo NextAlarm { get; private set; }

        /// <summary>
        /// 再通知
        /// </summary>
        private UserControl.AlarmInfo Realarm;

        /// <summary>
        /// パラメータ名称一覧
        /// </summary>
        private class NAME
        {

            /// <summary>
            /// 設定
            /// </summary>
            public class SETTING
            {

                /// <summary>
                /// パラメータ名：設定データ開始
                /// </summary>
                public const string START = "SettingStart";

                /// <summary>
                /// パラメータ名：設定データ終了
                /// </summary>
                public const string END = "SettingEnd";

                /// <summary>
                /// 朝食
                /// </summary>
                public const string BREAKFAST = "Breakfast";

                /// <summary>
                /// 昼食
                /// </summary>
                public const string LUNCH = "Lunch";

                /// <summary>
                /// 夕食
                /// </summary>
                public const string DINNER = "Dinner";

                /// <summary>
                /// 就寝
                /// </summary>
                public const string SLEEP = "Sleep";

                /// <summary>
                /// 食前アラーム(分)
                /// </summary>
                public const string BEFOREMEALS = "BeforeMeals";

                /// <summary>
                /// 食間アラーム(分)
                /// </summary>
                public const string BETWEENMEALS = "BetweenMeals";

                /// <summary>
                /// 食後アラーム(分)
                /// </summary>
                public const string AFTERMEALS = "AfterMeals";

                /// <summary>
                /// 就寝前アラーム(分)
                /// </summary>
                public const string BEFORESLEEP = "BeforeSleep";

                /// <summary>
                /// 再通知
                /// </summary>
                public const string REALARM = "ReAlarm";

            }

            /// <summary>
            /// 薬
            /// </summary>
            public class DRUG
            {

                /// <summary>
                /// パラメータ名：設定データ開始
                /// </summary>
                public const string START = "DrugStart";

                /// <summary>
                /// パラメータ名：設定データ終了
                /// </summary>
                public const string END = "DrugEnd";

                /// <summary>
                /// 薬名称
                /// </summary>
                public const string NAME = "Name";

                /// <summary>
                /// タイミング
                /// 食前、食間、食後
                /// </summary>
                public const string TIMING = "Timing";

                /// <summary>
                /// 食前
                /// </summary>
                public const string BEFOREMEALS = "BeforeMeals";

                /// <summary>
                /// 食間
                /// </summary>
                public const string BETWEENMEALS = "BetweenMeals";

                /// <summary>
                /// 食後
                /// </summary>
                public const string AFTERMEALS = "AfterMeals";

                /// <summary>
                /// 朝食
                /// </summary>
                public const string BREAKFAST = "BreakFast";

                /// <summary>
                /// 昼食
                /// </summary>
                public const string LUNCH = "Lunch";

                /// <summary>
                /// 夕食
                /// </summary>
                public const string DINNER = "Dinner";

                /// <summary>
                /// 頓服
                /// </summary>
                public const string TOBETAKEN = "ToBeTaken";

                /// <summary>
                /// 就寝
                /// </summary>
                public const string SLEEP = "Sleep";

                /// <summary>
                /// 指定時間
                /// </summary>
                public const string APPOINTTIME = "AppointTime";

                /// <summary>
                /// 時間毎(時)
                /// </summary>
                public const string HOUREACH = "HourEach";

                /// <summary>
                /// 処方量
                /// </summary>
                public const string TOTALVOLUME = "TotalVolume";

                /// <summary>
                /// 残量アラーム(錠)
                /// </summary>
                public const string PRESCRIPTIOIN = "PrescriptionAlarm";

                /// <summary>
                /// 備考
                /// </summary>
                public const string REMARKS = "Remarks";

            }

        }

        /// <summary>
        /// 設定クラス
        /// </summary>
        public class SettingParameter
        {

            /// <summary>
            /// 設定データ読み込み・書き込み中
            /// </summary>
            public bool IsAccess;

            /// <summary>
            /// 朝食時間範囲
            /// </summary>
            public UserControl.BetweenTime Breakfast;

            /// <summary>
            /// 昼食時間範囲
            /// </summary>
            public UserControl.BetweenTime Lunch;

            /// <summary>
            /// 夕食時間範囲
            /// </summary>
            public UserControl.BetweenTime Dinner;

            /// <summary>
            /// 就寝時間
            /// </summary>
            public DateTime Sleep;

            /// <summary>
            /// 食前時間（分）
            /// </summary>
            public Int32 MinuteBeforeMeals;

            /// <summary>
            /// 食後時間（分）
            /// </summary>
            public Int32 MinuteAfterMeals;

            /// <summary>
            /// 就寝前時間（分）
            /// </summary>
            public Int32 MinuteBeforeSleep;

            /// <summary>
            /// 再通知時間（分）
            /// </summary>
            public Int32 MinuteRealarm;

            /// <summary>
            /// new
            /// </summary>
            public SettingParameter()
            {

                Method method = new Method();

                IsAccess = false;
                Breakfast = new UserControl.BetweenTime
                {
                    Start = method.GetTodayTime(6, 0),
                    Finish = method.GetTodayTime(6, 30)
                };
                Lunch = new UserControl.BetweenTime
                {
                    Start = method.GetTodayTime(12, 0),
                    Finish = method.GetTodayTime(12, 30)
                };
                Dinner = new UserControl.BetweenTime
                {
                    Start = method.GetTodayTime(18, 0),
                    Finish = method.GetTodayTime(18, 30)
                };
                Sleep = method.GetTodayTime(23, 0);
                MinuteBeforeMeals = 30;
                MinuteAfterMeals = 30;
                MinuteBeforeSleep = 30;
                MinuteRealarm = 30;

            }

        }

        /// <summary>
        /// 薬クラス
        /// </summary>
        public class DrugParameter
        {

            /// <summary>
            /// 薬データ読み込み・書き込み中
            /// </summary>
            public bool IsAccess;

            /// <summary>
            /// 名称
            /// </summary>
            public string Name;

            /// <summary>
            /// 朝食
            /// </summary>
            public UserControl.Timing Breakfast;

            /// <summary>
            /// 昼食
            /// </summary>
            public UserControl.Timing Lunch;

            /// <summary>
            /// 夕食
            /// </summary>
            public UserControl.Timing Dinner;

            /// <summary>
            /// 就寝
            /// </summary>
            public UserControl.Timing Sleep;

            /// <summary>
            /// 頓服
            /// </summary>
            public UserControl.Timing ToBeTaken;

            /// <summary>
            /// 指定日時
            /// </summary>
            public UserControl.Timing Appoint;

            /// <summary>
            /// 指定日時時間
            /// </summary>
            public DateTime AppointTime;

            /// <summary>
            /// 時間毎
            /// </summary>
            public UserControl.Timing HourEach;

            /// <summary>
            /// 時間毎（時）
            /// </summary>
            public Int32 HourEachTime;

            /// <summary>
            /// 時間毎の次回アラーム日時
            /// </summary>
            public DateTime HourEachNextTime;

            /// <summary>
            /// 薬切れお知らせアラーム表示有無プロパティ
            /// </summary>
            public bool IsPrescriptionAlarm { get; private set; }

            /// <summary>
            /// 薬切れお知らせアラームFLG更新
            /// </summary>
            private void UpdatePrescription()
            {
                IsPrescriptionAlarm = (_TotalVolume <= _PrescriptionAlarmVolume);
            }

            /// <summary>
            /// 処方量
            /// </summary>
            private Int32 _TotalVolume;

            /// <summary>
            /// 処方量プロパティ
            /// </summary>
            /// <value>The total volume.</value>
            public Int32 TotalVolume
            {
                get { return _TotalVolume; }
                set
                {
                    _TotalVolume = value < 0 ? 0 : value;
                    UpdatePrescription();
                }
            }

            /// <summary>
            /// お知らせを表示する薬残量
            /// </summary>
            private Int32 _PrescriptionAlarmVolume;

            /// <summary>
            /// お知らせを表示する薬残量プロパティ
            /// </summary>
            /// <value>The prescription alarm volume.</value>
            public Int32 PrescriptionAlarmVolume
            {
                get { return _PrescriptionAlarmVolume; }
                set
                {
                    _PrescriptionAlarmVolume = value;
                    UpdatePrescription();
                }
            }

            /// <summary>
            /// 備考
            /// </summary>
            public string Remarks;

            /// <summary>
            /// 服用タイミング説明文
            /// </summary>
            public string DrugTiming;

            /// <summary>
            /// new
            /// </summary>
            public DrugParameter()
            {

                IsAccess = false;
                Name = "";
                Breakfast = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.None,
                    Volume = 0
                };
                Lunch = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.None,
                    Volume = 0
                };
                Dinner = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.None,
                    Volume = 0
                };
                Sleep = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.Before,
                    Volume = 0
                };
                ToBeTaken = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.None,
                    Volume = 0
                };
                Appoint = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.Appoint,
                    Volume = 0
                };
                AppointTime = DateTime.Now;
                HourEach = new UserControl.Timing
                {
                    IsDrug = false,
                    Kind = UserControl.KindTiming.TimeEach,
                    Volume = 0
                };
                HourEachTime = 2;
                HourEachNextTime = DateTime.MaxValue;
                TotalVolume = 0;
                PrescriptionAlarmVolume = 0;
                Remarks = "";
                DrugTiming = "";

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
        /// 次回通知が再通知の設定によるものか
        /// </summary>
        private bool IsSetRealarm;

        /// <summary>
        /// new
        /// </summary>
        public Parameter()
        {

            IsSetRealarm = false;

            Setting = new SettingParameter();
            DrugList = new List<DrugParameter>();

            NextAlarm = new UserControl.AlarmInfo();
            Realarm = new UserControl.AlarmInfo();

            Load();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Class.Parameter"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Class.Parameter"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Class.Parameter"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Class.Parameter"/> was occupying.</remarks>
        public void Dispose()
        {

            Setting = null;

            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {
                DrugList[iLoop] = null;
            }
            DrugList.Clear();
            DrugList = null;

            NextAlarm.Dispose();
            Realarm.Dispose();

        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        public void Load()
        {

        }

    }
}
