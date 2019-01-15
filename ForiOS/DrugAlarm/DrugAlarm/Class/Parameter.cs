using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace DrugAlarm.Class
{

    /// <summary>
    /// 設定パラメータ
    /// </summary>
    public class Parameter : IDisposable
    {

        /// <summary>
        /// ファイルロック
        /// </summary>
        private object _FileLocker = new object();

        /// <summary>
        /// 次回アラーム情報ファイルロック
        /// </summary>
        private object _NextAlarmLocker = new object();

        /// <summary>
        /// 次回アラームは再通知
        /// </summary>
        private bool _IsNextRealarm = false;

        /// <summary>
        /// 次回アラームプロパティ
        /// </summary>
        /// <value>The next alarm.</value>
        public UserControl.AlarmInfo NextAlarm { get; private set; }

        /// <summary>
        /// 服用済時間一覧
        /// </summary>
        private List<DateTime> _SaveTimes;

        /// <summary>
        /// 再通知
        /// </summary>
        private List<UserControl.AlarmInfo> _Realarm;

        /// <summary>
        /// 履歴情報
        /// </summary>
        public List<UserControl.AlarmInfo> AlarmHistory;

        /// <summary>
        /// 履歴情報最大件数
        /// </summary>
        private const Int32 _AlarmHistoryMaxCount = 10;

        /// <summary>
        /// 次回アラーム読込済
        /// </summary>
        private bool _IsLoadNextAlarm = false;

        /// <summary>
        /// 残数切れ薬Index一覧
        /// </summary>
        public List<Int32> PrescriptionList;

        /// <summary>
        /// パラメータ名称一覧
        /// </summary>
        private class NAME
        {

            /// <summary>
            /// 設定
            /// </summary>
            public static class SETTING
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

#pragma warning disable RECS0146 // メンバーは外部クラスから静的メンバーを隠します
                /// <summary>
                /// 再通知
                /// </summary>
                public const string REALARM = "ReAlarm";
#pragma warning restore RECS0146 // メンバーは外部クラスから静的メンバーを隠します

                /// <summary>
                /// 前回アラーム日時
                /// </summary>
                public const string BEFOREALARM = "BeforeAlarm";

            }

            /// <summary>
            /// 薬
            /// </summary>
            public static class DRUG
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

            /// <summary>
            /// 次回アラーム
            /// </summary>
            public static class NEXTALARM
            {

                /// <summary>
                /// パラメータ名：設定データ開始
                /// </summary>
                public const string START = "NextAlarmStart";

                /// <summary>
                /// パラメータ名：設定データ終了
                /// </summary>
                public const string END = "NextAlarmEnd";

                /// <summary>
                /// アラーム時間
                /// </summary>
                public const string TIME = "Time";

            }

            /// <summary>
            /// 再通知
            /// </summary>
            public static class REALARM
            {

                /// <summary>
                /// パラメータ名：設定データ開始
                /// </summary>
                public const string START = "RealarmStart";

                /// <summary>
                /// パラメータ名：設定データ終了
                /// </summary>
                public const string END = "RealarmEnd";

                /// <summary>
                /// アラーム時間
                /// </summary>
                public const string TIME = "Time";

            }

            /// <summary>
            /// アラームでの薬情報
            /// </summary>
            /// <remarks>アラーム、再通知にて使用</remarks>
            public static class ALARMDRUG
            {

                /// <summary>
                /// 薬情報開始
                /// </summary>
                public const string START = "AlarmDrugStart";

                /// <summary>
                /// 薬情報終了
                /// </summary>
                public const string END = "AlarmDrugEnd";

                /// <summary>
                /// Index
                /// </summary>
                public const string INDEX = "Index";

                /// <summary>
                /// 錠
                /// </summary>
                public const string VOLUME = "Volume";

                /// <summary>
                /// 指定日時か
                /// </summary>
                public const string APPOINT = "Appoint";

                /// <summary>
                /// 時間毎か
                /// </summary>
                public const string HOUREACH = "HourEach";

            }

            /// <summary>
            /// アラーム済日時情報
            /// </summary>
            public static class SAVEALARMTIME
            {

                /// <summary>
                /// アラーム済日時情報開始
                /// </summary>
                public const string START = "SaveAlarmStart";

                /// <summary>
                /// アラーム済日時情報終了
                /// </summary>
                public const string END = "SaveAlarmEnd";

                /// <summary>
                /// 保存日時
                /// </summary>
                public const string TIME = "SaveTime";

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
            /// 前回アラーム日時
            /// </summary>
            public DateTime BeforeAlarmTime;

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

                BeforeAlarmTime = DateTime.Now;

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
            /// 指定日時を日毎に繰り返す
            /// </summary>
            /// <remarks>0なら1回きり</remarks>
            public Int32 AppointDayEach;

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
                AppointDayEach = 0;
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
        /// new
        /// </summary>
        public Parameter()
        {

            _SaveTimes = new List<DateTime>();

            Setting = new SettingParameter();
            DrugList = new List<DrugParameter>();

            NextAlarm = new UserControl.AlarmInfo();
            _Realarm = new List<UserControl.AlarmInfo>();

            AlarmHistory = new List<UserControl.AlarmInfo>();

            PrescriptionList = new List<Int32>();

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

            _SaveTimes.Clear();
            _SaveTimes = null;

            Setting = null;

            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {
                DrugList[iLoop] = null;
            }
            DrugList.Clear();
            DrugList = null;

            NextAlarm.Dispose();
            NextAlarm = null;

            _Realarm.ForEach(Alarm => { Alarm.Dispose(); });
            _Realarm.Clear();
            _Realarm = null;

            AlarmHistory.ForEach(Alarm => { Alarm.Dispose(); });
            AlarmHistory.Clear();
            AlarmHistory = null;

            PrescriptionList.Clear();
            PrescriptionList = null;

        }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        public void Load()
        {

            string FilePath;

            //前回パスの取得
            if (Application.Current.Properties.ContainsKey(UserControl.ParameterFullPath))
            {
                FilePath = (string)Application.Current.Properties[UserControl.ParameterFullPath];
            }
            else
            {
                FilePath = "";
            }

            //前回パスが未記入、またはファイルがなければ新規作成
            if (FilePath.Length.Equals(0) || !File.Exists(FilePath))
            {
                FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                FilePath = Path.Combine(FilePath, UserControl.ParameterFileName);

                Application.Current.Properties[UserControl.ParameterFullPath] = FilePath;

                if (!File.Exists(FilePath))
                {
                    Save(false);
                }

            }

            lock (_FileLocker)
            {

                //ファイル読み込み開始
                using (StreamReader FileData = new StreamReader(FilePath, Encoding.Unicode))
                {

                    Method method = new Method();
                    StringBuilder NewLine = new StringBuilder();
                    Int32 Index = 0;

                    DrugList.Clear();

                    while (!FileData.EndOfStream)
                    {

                        try
                        {

                            NewLine.Append(FileData.ReadLine());

                            if (!NewLine.ToString().Contains("="))
                            {

                                //パラメータ種類の選別
                                switch (NewLine.ToString())
                                {

                                    case NAME.SETTING.START:

                                        Setting.IsAccess = true;

                                        if (DrugList.Count > 0)
                                        {
                                            DrugList[DrugList.Count - 1].IsAccess = false;
                                        }

                                        break;

                                    case NAME.SETTING.END:
                                        Setting.IsAccess = false;
                                        break;

                                    case NAME.DRUG.START:

                                        Setting.IsAccess = false;

                                        DrugList.Add(new DrugParameter());
                                        Index = DrugList.Count - 1;
                                        DrugList[Index].IsAccess = true;

                                        break;

                                    case NAME.DRUG.END:

                                        DrugList[Index].DrugTiming = MakeTimingMessage(Index);
                                        DrugList[Index].IsAccess = false;

                                        break;

                                    default:
                                        break;

                                }
                            }
                            else
                            {

                                string[] Strings = NewLine.ToString().Split('=');
                                string[] Values = Strings[1].Split(',');

                                if (Setting.IsAccess)
                                {

                                    //設定パラメータ
                                    switch (Strings[0])
                                    {

                                        case NAME.SETTING.BREAKFAST:
                                            Setting.Breakfast.Start = method.GetTodayTime(Values[0]);
                                            Setting.Breakfast.Finish = method.GetTodayTime(Values[1]);
                                            break;

                                        case NAME.SETTING.LUNCH:
                                            Setting.Lunch.Start = method.GetTodayTime(Values[0]);
                                            Setting.Lunch.Finish = method.GetTodayTime(Values[1]);
                                            break;

                                        case NAME.SETTING.DINNER:
                                            Setting.Dinner.Start = method.GetTodayTime(Values[0]);
                                            Setting.Dinner.Finish = method.GetTodayTime(Values[1]);
                                            break;

                                        case NAME.SETTING.SLEEP:
                                            Setting.Sleep = method.GetTodayTime(Strings[1]);
                                            break;

                                        case NAME.SETTING.BEFOREMEALS:
                                            Setting.MinuteBeforeMeals = method.ConvertToInt32(Strings[1], Setting.MinuteBeforeMeals);
                                            break;

                                        case NAME.SETTING.AFTERMEALS:
                                            Setting.MinuteAfterMeals = method.ConvertToInt32(Strings[1], Setting.MinuteAfterMeals);
                                            break;

                                        case NAME.SETTING.BEFORESLEEP:
                                            Setting.MinuteBeforeSleep = method.ConvertToInt32(Strings[1], Setting.MinuteBeforeSleep);
                                            break;

                                        case NAME.SETTING.REALARM:
                                            Setting.MinuteRealarm = method.ConvertToInt32(Strings[1], Setting.MinuteRealarm);
                                            break;

                                        case NAME.SETTING.BEFOREALARM:
                                            Setting.BeforeAlarmTime = method.ConvertToDateTime(Strings[1], Setting.BeforeAlarmTime);
                                            break;

                                        default:
                                            break;
                                    }

                                }
                                else if (DrugList[Index].IsAccess)
                                {

                                    //薬パラメータ
                                    switch (Strings[0])
                                    {

                                        case NAME.DRUG.NAME:
                                            DrugList[Index].Name = Strings[1];
                                            break;

                                        case NAME.DRUG.BREAKFAST:
                                            DrugList[Index].Breakfast.IsDrug = Values.Length > 2 ? method.ConvertToBoolean(Values[2], true) : true;
                                            DrugList[Index].Breakfast.Volume = method.ConvertToInt32(Values[0], DrugList[Index].Breakfast.Volume);
                                            DrugList[Index].Breakfast.Kind = GetTiming(Values[1]);
                                            break;

                                        case NAME.DRUG.LUNCH:
                                            DrugList[Index].Lunch.IsDrug = Values.Length > 2 ? method.ConvertToBoolean(Values[2], true) : true;
                                            DrugList[Index].Lunch.Volume = method.ConvertToInt32(Values[0], DrugList[Index].Lunch.Volume);
                                            DrugList[Index].Lunch.Kind = GetTiming(Values[1]);
                                            break;

                                        case NAME.DRUG.DINNER:
                                            DrugList[Index].Dinner.IsDrug = Values.Length > 2 ? method.ConvertToBoolean(Values[2], true) : true;
                                            DrugList[Index].Dinner.Volume = method.ConvertToInt32(Values[0], DrugList[Index].Dinner.Volume);
                                            DrugList[Index].Dinner.Kind = GetTiming(Values[1]);
                                            break;

                                        case NAME.DRUG.SLEEP:
                                            DrugList[Index].Sleep.IsDrug = Values.Length > 1 ? method.ConvertToBoolean(Values[1], true) : true;
                                            DrugList[Index].Sleep.Volume = method.ConvertToInt32(Values[0], DrugList[Index].Sleep.Volume);
                                            break;

                                        case NAME.DRUG.TOBETAKEN:
                                            DrugList[Index].ToBeTaken.IsDrug = Values.Length > 1 ? method.ConvertToBoolean(Values[1], true) : true;
                                            DrugList[Index].ToBeTaken.Volume = method.ConvertToInt32(Values[0], DrugList[Index].ToBeTaken.Volume);
                                            break;

                                        case NAME.DRUG.APPOINTTIME:
                                            DrugList[Index].Appoint.IsDrug = Values.Length > 3 ? method.ConvertToBoolean(Values[3], true) : true;
                                            DrugList[Index].AppointTime = method.ConvertToDateTime(Values[0], DrugList[Index].AppointTime);
                                            DrugList[Index].Appoint.Volume = method.ConvertToInt32(Values[1], DrugList[Index].Appoint.Volume);

                                            // 追加パラメータ:指定日時の繰り返し
                                            if (Values.Length > 2)
                                            {
                                                // 指定日時を[n]日毎に繰り返す
                                                // n=0ならば1回きり
                                                DrugList[Index].AppointDayEach = method.ConvertToInt32(Values[2], 0);
                                            }

                                            // 3日以上経過している場合は現在日時とする
                                            if (DrugList[Index].AppointTime < DateTime.Now.AddDays(-3))
                                            {
                                                DrugList[Index].AppointTime = DateTime.Now;
                                            }

                                            break;

                                        case NAME.DRUG.HOUREACH:
                                            DrugList[Index].HourEach.IsDrug = Values.Length > 3 ? method.ConvertToBoolean(Values[3], true) : true;
                                            DrugList[Index].HourEachTime = method.ConvertToInt32(Values[0], DrugList[Index].HourEachTime);
                                            DrugList[Index].HourEach.Volume = method.ConvertToInt32(Values[1], DrugList[Index].HourEach.Volume);
                                            DrugList[Index].HourEachNextTime = method.ConvertToDateTime(Values[2], DateTime.Now.AddHours(DrugList[Index].HourEachTime));

                                            // 3日以上経過している場合は現在日時とする
                                            if (DrugList[Index].HourEachNextTime < DateTime.Now.AddDays(-3))
                                            {
                                                DrugList[Index].HourEachNextTime = DateTime.Now;
                                            }

                                            break;

                                        case NAME.DRUG.TOTALVOLUME:
                                            DrugList[Index].TotalVolume = method.ConvertToInt32(Strings[1], DrugList[Index].TotalVolume);
                                            break;

                                        case NAME.DRUG.PRESCRIPTIOIN:
                                            DrugList[Index].PrescriptionAlarmVolume = method.ConvertToInt32(Strings[1], DrugList[Index].PrescriptionAlarmVolume);
                                            break;

                                        case NAME.DRUG.REMARKS:
                                            DrugList[Index].Remarks = method.ConvertToCRLF(Strings[1]);
                                            break;

                                        default:
                                            break;

                                    }

                                }

                            }

                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                        }
                        finally
                        {
                            NewLine.Clear();
                        }

                    }

                    //メモリ解放
                    NewLine.Clear();
                    NewLine = null;

                }

                // 次回アラームをパラメータより読み込み
                LoadNextAlarmParameter();

                //次回アラーム取得
                SetNextAlarm();

            }

        }

        /// <summary>
        /// 次回アラームの読み込み
        /// </summary>
        private void LoadNextAlarmParameter()
        {

            // 前回パスの取得
            string FilePath = Application.Current.Properties.ContainsKey(UserControl.AlarmFullPath) 
                                ? (string)Application.Current.Properties[UserControl.AlarmFullPath] 
                                : "";

            if (!FilePath.Length.Equals(0) && File.Exists(FilePath))
            {

                lock (_NextAlarmLocker)
                {

                    // 初期化
                    _Realarm.ForEach(Alarm => { Alarm.Dispose(); });
                    _Realarm.Clear();

                    //ファイル読み込み開始
                    using (StreamReader FileData = new StreamReader(FilePath, Encoding.Unicode))
                    {

                        Method method = new Method();
                        StringBuilder NewLine = new StringBuilder();
                        UserControl.AlarmInfo AddAlarm = null;
                        Int32 Index = -1;

                        bool IsNextAlarm = false;
                        bool IsRealarm = false;
                        bool IsDrugAccess = false;
                        bool IsSaveTime = false;

                        while (!FileData.EndOfStream)
                        {

                            try
                            {

                                NewLine.Append(FileData.ReadLine());

                                if (!NewLine.ToString().Contains("="))
                                {

                                    //パラメータ種類の選別
                                    switch (NewLine.ToString())
                                    {

                                        case NAME.NEXTALARM.START:

                                            // 初期化
                                            NextAlarm.Timer = DateTime.MaxValue;
                                            NextAlarm.DrugList.Clear();

                                            IsNextAlarm = true;
                                            IsRealarm = false;
                                            IsDrugAccess = false;
                                            IsSaveTime = false;

                                            break;

                                        case NAME.NEXTALARM.END:

                                            _IsLoadNextAlarm = true;
                                            IsNextAlarm = false;
                                            break;

                                        case NAME.REALARM.START:

                                            // 初期化
                                            AddAlarm = new UserControl.AlarmInfo();

                                            IsRealarm = true;
                                            IsSaveTime = false;
                                            break;

                                        case NAME.REALARM.END:

                                            // 追加
                                            _Realarm.Add(AddAlarm);

                                            IsRealarm = false;
                                            break;

                                        case NAME.ALARMDRUG.START:

                                            // 初期化
                                            if (IsRealarm)
                                            {

                                                // 薬追加
                                                AddAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug());
                                                Index = AddAlarm.DrugList.Count - 1;

                                            }
                                            else if (IsNextAlarm)
                                            {

                                                // 薬追加
                                                NextAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug());
                                                Index = NextAlarm.DrugList.Count - 1;

                                            }

                                            IsDrugAccess = true;
                                            break;

                                        case NAME.ALARMDRUG.END:

                                            // 初期化
                                            Index = -1;

                                            IsDrugAccess = false;
                                            break;

                                        case NAME.SAVEALARMTIME.START:

                                            // 初期化
                                            _SaveTimes.Clear();

                                            IsNextAlarm = false;
                                            IsRealarm = false;
                                            IsDrugAccess = false;
                                            IsSaveTime = true;
                                            break;

                                        case NAME.SAVEALARMTIME.END:

                                            IsSaveTime = false;
                                            break;

                                        default:
                                            break;

                                    }
                                }
                                else
                                {

                                    string[] Strings = NewLine.ToString().Split('=');
                                    string[] Values = Strings[1].Split(',');

                                    if (IsNextAlarm && !IsDrugAccess)
                                    {

                                        // 再通知設定
                                        switch (Strings[0])
                                        {

                                            case NAME.NEXTALARM.TIME:
                                                NextAlarm.Timer = method.ConvertToDateTime(Strings[1], DateTime.Now);
                                                break;

                                            default:
                                                break;

                                        }

                                    }
                                    else if (IsRealarm && !IsDrugAccess && AddAlarm != null)
                                    {

                                        // 再通知設定
                                        switch (Strings[0])
                                        {

                                            case NAME.REALARM.TIME:
                                                AddAlarm.Timer = method.ConvertToDateTime(Strings[1], DateTime.Now);
                                                break;

                                            default:
                                                break;

                                        }

                                    }

                                    else if (IsDrugAccess && !Index.Equals(-1))
                                    {

                                        //薬パラメータ
                                        switch (Strings[0])
                                        {

                                            case NAME.ALARMDRUG.INDEX:

                                                if (IsNextAlarm)
                                                {
                                                    NextAlarm.DrugList[Index].Index = method.ConvertToInt32(Strings[1], -1);
                                                }
                                                else if (IsRealarm)
                                                {
                                                    AddAlarm.DrugList[Index].Index = method.ConvertToInt32(Strings[1], -1);
                                                }
                                                break;

                                            case NAME.ALARMDRUG.VOLUME:

                                                if (IsNextAlarm)
                                                {
                                                    NextAlarm.DrugList[Index].Volume = method.ConvertToInt32(Strings[1], -1);
                                                }
                                                else if (IsRealarm)
                                                {
                                                    AddAlarm.DrugList[Index].Volume = method.ConvertToInt32(Strings[1], -1);
                                                }
                                                break;

                                            case NAME.ALARMDRUG.APPOINT:

                                                if (bool.TryParse(Strings[1], out bool AppointResult))
                                                {

                                                    if (IsNextAlarm)
                                                    {
                                                        NextAlarm.DrugList[Index].IsAppoint = AppointResult;
                                                    }
                                                    else if (IsRealarm)
                                                    {
                                                        AddAlarm.DrugList[Index].IsAppoint = AppointResult;
                                                    }

                                                }
                                                break;

                                            case NAME.ALARMDRUG.HOUREACH:

                                                if (bool.TryParse(Strings[1], out bool HourEachResult))
                                                {

                                                    if (IsNextAlarm)
                                                    {
                                                        NextAlarm.DrugList[Index].IsHourEach = HourEachResult;
                                                    }
                                                    else if (IsRealarm)
                                                    {
                                                        AddAlarm.DrugList[Index].IsHourEach = HourEachResult;
                                                    }

                                                }
                                                break;

                                            default:
                                                break;

                                        }

                                    }
                                    else if (IsSaveTime)
                                    {

                                        //薬パラメータ
                                        switch (Strings[0])
                                        {

                                            case NAME.SAVEALARMTIME.TIME:

                                                if (DateTime.TryParse(Strings[1], out DateTime AddTime))
                                                {
                                                    _SaveTimes.Add(AddTime);
                                                }
                                                break;

                                            default:
                                                break;

                                        }

                                    }

                                }

                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                            }
                            finally
                            {
                                NewLine.Clear();
                            }

                        }

                        //メモリ解放
                        NewLine.Clear();
                        NewLine = null;

                    }

                }

            }

        }

        /// <summary>
        /// 服用タイミングの取得
        /// </summary>
        /// <returns>The timing.</returns>
        /// <param name="Value">パラメータ値</param>
        private UserControl.KindTiming GetTiming(string Value)
        {
            switch (Value)
            {
                case NAME.DRUG.BEFOREMEALS: return UserControl.KindTiming.Before;
                case NAME.DRUG.BETWEENMEALS: return UserControl.KindTiming.Between;
                case NAME.DRUG.AFTERMEALS: return UserControl.KindTiming.After;
                default: return UserControl.KindTiming.Before;
            }
        }

        /// <summary>
        /// 一覧表示する服用タイミングメッセージの作成
        /// </summary>
        /// <returns>The timing message.</returns>
        /// <param name="Index">DrugList.Index</param>
        private string MakeTimingMessage(Int32 Index)
        {

            string Return;
            StringBuilder Message = new StringBuilder();

            Message.Clear();

            #region 朝昼夕
            if (DrugList[Index].Breakfast.IsDrug && DrugList[Index].Lunch.IsDrug && DrugList[Index].Dinner.IsDrug
                && DrugList[Index].Breakfast.Kind.Equals(DrugList[Index].Lunch.Kind)
                && DrugList[Index].Breakfast.Kind.Equals(DrugList[Index].Dinner.Kind))
            {
                Message.Append(Resx.Resources.Parameter_Timing_Always);
                Message.Append(SetTimingMessage(DrugList[Index].Breakfast.Kind));
            }
            else
            {

                if (DrugList[Index].Breakfast.IsDrug)
                {
                    Message.Append(Resx.Resources.Parameter_Timing_Breakfast);
                    Message.Append(SetTimingMessage(DrugList[Index].Breakfast.Kind));
                }

                if (DrugList[Index].Lunch.IsDrug)
                {

                    if (Message.Length > 0)
                    {
                        Message.Append(",");
                    }

                    Message.Append(Resx.Resources.Parameter_Timing_Lunch);
                    Message.Append(SetTimingMessage(DrugList[Index].Lunch.Kind));
                }

                if (DrugList[Index].Dinner.IsDrug)
                {

                    if (Message.Length > 0)
                    {
                        Message.Append(",");
                    }

                    Message.Append(Resx.Resources.Parameter_Timing_Dinner);
                    Message.Append(SetTimingMessage(DrugList[Index].Dinner.Kind));
                }

            }
            #endregion

            #region 就寝
            if (DrugList[Index].Sleep.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(Resx.Resources.Parameter_Timing_Sleep);

            }
            #endregion

            #region 頓服
            if (DrugList[Index].ToBeTaken.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(Resx.Resources.Parameter_Timing_ToBeTaken);

            }
            #endregion

            #region 日時指定
            if (DrugList[Index].Appoint.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(DrugList[Index].AppointTime.ToString(Resx.Resources.Parameter_Timing_Appoint.Replace("_", "")));

            }
            #endregion

            #region 時間毎
            if (DrugList[Index].HourEach.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(Resx.Resources.Parameter_Timing_Each.Replace("_n_", DrugList[Index].HourEachTime.ToString()));

            }
            #endregion

            Return = Message.ToString();

            //メモリ解放
            Message.Clear();
            Message = null;

            return Return;

        }

        /// <summary>
        /// 服用タイミングのメッセージ作成
        /// </summary>
        /// <returns>The timing message.</returns>
        /// <param name="Kind">Kind.朝昼夕それぞれの服用タイミング</param>
        private string SetTimingMessage(UserControl.KindTiming Kind)
        {
            switch (Kind)
            {
                case UserControl.KindTiming.Before: return Resx.Resources.Parameter_Timing_Before;
                case UserControl.KindTiming.Between: return Resx.Resources.Parameter_Timing_Between;
                case UserControl.KindTiming.After: return Resx.Resources.Parameter_Timing_After;
                default: return Resx.Resources.Parameter_Timing_Before;
            }
        }

        /// <summary>
        /// ファイル書き込み
        /// </summary>
        /// <param name="UpdateBeforeAlarmTime">前回アラーム時間を更新するか</param>
        public void Save(bool UpdateBeforeAlarmTime)
        {

            //アラーム時間の記憶
            if (UpdateBeforeAlarmTime)
            {
                Setting.BeforeAlarmTime = NextAlarm.Timer.Equals(DateTime.MaxValue) ? DateTime.Now : NextAlarm.Timer;
            }

            lock(_FileLocker)
            {

                //保存パスの取得
                string FilePath = (string)Application.Current.Properties[UserControl.ParameterFullPath];
                string TmpPath = FilePath.Replace(UserControl.FileExtension, ".Tmp");

                //ファイル書き込み開始
                try
                {

                    using (StreamWriter FileData = new StreamWriter(TmpPath, false, Encoding.Unicode))
                    {

                        #region 設定パラメータ
                        FileData.WriteLine(NAME.SETTING.START);
                        FileData.WriteLine(MakeParameter(NAME.SETTING.BREAKFAST, Setting.Breakfast.Start, Setting.Breakfast.Finish));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.LUNCH, Setting.Lunch.Start, Setting.Lunch.Finish));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.DINNER, Setting.Dinner.Start, Setting.Dinner.Finish));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.SLEEP, Setting.Sleep));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.BEFOREMEALS, Setting.MinuteBeforeMeals));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.AFTERMEALS, Setting.MinuteAfterMeals));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.BEFORESLEEP, Setting.MinuteBeforeSleep));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.REALARM, Setting.MinuteRealarm));
                        FileData.WriteLine(MakeParameter(NAME.SETTING.BEFOREALARM, Setting.BeforeAlarmTime, true));
                        FileData.WriteLine(NAME.SETTING.END);
                        #endregion

                        #region 薬パラメータ
                        for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                        {

                            // 時間毎の次回時間を計算
                            if (DrugList[iLoop].HourEach.IsDrug && DrugList[iLoop].HourEachNextTime.Equals(DateTime.MaxValue))
                            {
                                DrugList[iLoop].HourEachNextTime = DateTime.Now.AddHours(DrugList[iLoop].HourEachTime);
                            }

                            FileData.WriteLine("");
                            FileData.WriteLine(NAME.DRUG.START);
                            FileData.WriteLine(MakeParameter(NAME.DRUG.NAME, DrugList[iLoop].Name, false));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.BREAKFAST, DrugList[iLoop].Breakfast.Kind, DrugList[iLoop].Breakfast.Volume, DrugList[iLoop].Breakfast.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.LUNCH, DrugList[iLoop].Lunch.Kind, DrugList[iLoop].Lunch.Volume, DrugList[iLoop].Lunch.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.DINNER, DrugList[iLoop].Dinner.Kind, DrugList[iLoop].Dinner.Volume, DrugList[iLoop].Dinner.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.SLEEP, DrugList[iLoop].Sleep.Volume, DrugList[iLoop].Sleep.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.TOBETAKEN, DrugList[iLoop].ToBeTaken.Volume, DrugList[iLoop].ToBeTaken.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.APPOINTTIME, DrugList[iLoop].AppointTime, DrugList[iLoop].Appoint.Volume, DrugList[iLoop].AppointDayEach, DrugList[iLoop].Appoint.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.HOUREACH, DrugList[iLoop].HourEachTime, DrugList[iLoop].HourEach.Volume, DrugList[iLoop].HourEachNextTime, DrugList[iLoop].HourEach.IsDrug));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.TOTALVOLUME, DrugList[iLoop].TotalVolume));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.PRESCRIPTIOIN, DrugList[iLoop].PrescriptionAlarmVolume));
                            FileData.WriteLine(MakeParameter(NAME.DRUG.REMARKS, DrugList[iLoop].Remarks, true));
                            FileData.WriteLine(NAME.DRUG.END);

                        }
                        #endregion

                    }

                    File.Copy(TmpPath, FilePath, true);
                    File.Delete(TmpPath);

                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                }
                finally
                {

                    //次回アラーム取得
                    SetNextAlarm();

                }

            }

        }

        #region Makeparameter

        /// <summary>
        /// パラメータ文字列形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">値</param>
        /// <param name="IsAllowLine">改行有無</param>
        private string MakeParameter(string Parameter, string Value, bool IsAllowLine)
        {

            Method method = new Method();
            StringBuilder Str = new StringBuilder(Parameter.Length + Value.Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(IsAllowLine ? method.ConvertToCRLF(Value) : Value);

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ食事時形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Kind">服用タイミング</param>
        /// <param name="Volume">数値</param>
        /// <param name="IsDrug">服用するか</param>
        private string MakeParameter(string Parameter, UserControl.KindTiming Kind, Int32 Volume, bool IsDrug)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + NAME.DRUG.BETWEENMEALS.Length + Volume.ToString().Length + IsDrug.ToString().Length + 3);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Volume.ToString());

                switch (Kind)
                {

                    case UserControl.KindTiming.Before:
                        Str.Append(",").Append(NAME.DRUG.BEFOREMEALS);
                        break;

                    case UserControl.KindTiming.Between:
                        Str.Append(",").Append(NAME.DRUG.BETWEENMEALS);
                        break;

                    case UserControl.KindTiming.After:
                        Str.Append(",").Append(NAME.DRUG.AFTERMEALS);
                        break;

                    default:
                        Str.Append(",").Append(NAME.DRUG.BEFOREMEALS);
                        break;

                }

                Str.Append(",").Append(IsDrug.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ数値形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">数値</param>
        private string MakeParameter(string Parameter, Int32 Value)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + Value.ToString().Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Value.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ数値形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">数値</param>
        /// <param name="IsDrug">服用するか</param>
        private string MakeParameter(string Parameter, Int32 Value, bool IsDrug)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + Value.ToString().Length + IsDrug.ToString().Length + 2);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Value.ToString());
                Str.Append(",").Append(IsDrug.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ時間形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">時間</param>
        /// <param name="IsDateTimeFormat">日時フォーマットで保存するか</param>
        private string MakeParameter(string Parameter, DateTime Value, bool IsDateTimeFormat = false)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.TimeFormat.Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Value.ToString(IsDateTimeFormat ? UserControl.DateTimeFormat : UserControl.TimeFormat));

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ時間形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">bool値</param>
        private string MakeParameter(string Parameter, bool Value)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.TimeFormat.Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Value.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ時間範囲形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Start">開始時間</param>
        /// <param name="Finish">終了時間</param>
        private string MakeParameter(string Parameter, DateTime Start, DateTime Finish)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.TimeFormat.Length * 2 + 2);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Start.ToString(UserControl.TimeFormat));
                Str.Append(",").Append(Finish.ToString(UserControl.TimeFormat));

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ指定日時形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="AppointTime">指定日時</param>
        /// <param name="Volume">服用錠</param>
        /// <param name="DayEach">繰り返し日</param>
        /// <param name="IsDrug">服用するか</param>
        private string MakeParameter(string Parameter, DateTime AppointTime, Int32 Volume, Int32 DayEach, bool IsDrug)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.DateTimeFormat.Length + Volume.ToString().Length + IsDrug.ToString().Length + 3);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(AppointTime.ToString(UserControl.DateTimeFormat));
                Str.Append(",").Append(Volume.ToString());
                Str.Append(",").Append(DayEach.ToString());
                Str.Append(",").Append(IsDrug.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ時間毎形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="HourEach">時間毎</param>
        /// <param name="Volume">服用錠</param>
        /// <param name="NextTime">次回アラーム時間</param>
        /// <param name="IsDrug">服用するか</param>
        private string MakeParameter(string Parameter, Int32 HourEach, Int32 Volume, DateTime NextTime, bool IsDrug)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + HourEach.ToString().Length + Volume.ToString().Length + UserControl.DateTimeFormat.Length + IsDrug.ToString().Length + 4);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(HourEach.ToString());
                Str.Append(",").Append(Volume.ToString());
                Str.Append(",").Append(NextTime.ToString(UserControl.DateTimeFormat));
                Str.Append(",").Append(IsDrug.ToString());

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        /// <summary>
        /// パラメータ時間形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Values">時間一覧</param>
        private string MakeParameter(string Parameter, List<DateTime> Values)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.TimeFormat.Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Values.ForEach(Value => 
                {
                    Str.Append(Parameter);
                    Str.Append("=").Append(Value.ToString(UserControl.DateTimeFormat));
                });

                Return = Str.ToString();

            }
            catch (Exception ex)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif

                Return = "";

            }
            finally
            {

                Str.Clear();
                Str = null;

            }

            return Return;

        }

        #endregion

        /// <summary>
        /// アラーム時刻を記憶しておく
        /// </summary>
        private void AddSaveAlarmTime()
        {

            // 未来の時刻のみ一覧に保存
            if (!_IsNextRealarm 
                && !NextAlarm.Timer.Equals(DateTime.MaxValue) 
                && NextAlarm.Timer > DateTime.Now 
                && _SaveTimes.IndexOf(NextAlarm.Timer).Equals(-1))
            {
                _SaveTimes.Add(NextAlarm.Timer);
                _SaveTimes.Sort();
            }

        }

        /// <summary>
        /// 薬服用時の処理
        /// </summary>
        /// <returns><c>true</c>, 薬切れ有, <c>false</c> 在庫十分</returns>
        public bool TakeMedicine()
        {

            PrescriptionList.Clear();

            AddSaveAlarmTime();

            //総量から服用錠を減算
            for (Int32 iLoop = 0; iLoop < NextAlarm.DrugList.Count; iLoop++)
            {

                if (NextAlarm.DrugList[iLoop].IsDrug)
                {

                    Int32 Index = NextAlarm.DrugList[iLoop].Index;
                    Int32 Volume = NextAlarm.DrugList[iLoop].Volume;

                    DrugList[Index].TotalVolume -= Volume;
                    if (DrugList[Index].TotalVolume < 0)
                        DrugList[Index].TotalVolume = 0;

                    // 指定時間による服用なら、次回日時の設定を行う
                    if (NextAlarm.DrugList[iLoop].IsAppoint)
                    {

                        if (DrugList[Index].AppointDayEach < 1)
                        {
                            // 日時指定なしの場合、1回きりなのでFLG解除
                            DrugList[Index].Appoint.IsDrug = false;
                        }
                        else
                        {
                            // 次回日時を再設定
                            DrugList[Index].AppointTime = DrugList[Index].AppointTime.AddDays(DrugList[Index].AppointDayEach);
                        }

                    }

                    //時間毎による服用なら、次回時刻を設定
                    if (NextAlarm.DrugList[iLoop].IsHourEach)
                    {
                        DrugList[Index].HourEachNextTime = NextAlarm.Timer.AddHours(DrugList[Index].HourEachTime);
                    }

                    // 残量チェック
                    if (DrugList[Index].IsPrescriptionAlarm)
                    {
                        PrescriptionList.Add(Index);
                    }

                }

            }

            // パラメータ更新
            Save(true);

            return !PrescriptionList.Count.Equals(0);

        }

        /// <summary>
        /// 次回アラーム情報の設定
        /// </summary>
        private void SetNextAlarm()
        {

            Method method = new Method();
            DateTime Time;

            // 次回アラーム読込済
            if (!_IsLoadNextAlarm)
            {

                #region 履歴登録
                if (!NextAlarm.Timer.Equals(DateTime.MaxValue))
                {

                    if (AlarmHistory.FindIndex(Alarm => { return Alarm.Timer.Equals(NextAlarm.Timer); }).Equals(-1))
                    {

                        UserControl.AlarmInfo History = new UserControl.AlarmInfo() 
                        {
                            Timer = NextAlarm.Timer
                        };

                        NextAlarm.DrugList.ForEach(Drug => 
                        {

                            if (Drug.IsDrug)
                            {

                                History.DrugList.Add(new UserControl.AlarmInfo.Drug
                                {
                                    Index = Drug.Index,
                                    Volume = Drug.Volume,
                                    IsAppoint = Drug.IsAppoint,
                                    IsHourEach = Drug.IsHourEach
                                });
                            
                            }

                        });

                        if (AlarmHistory.Count.Equals(0))
                        {
                            AlarmHistory.Add(History);
                        }
                        else
                        {
                            AlarmHistory.Insert(0, History);
                        }

                        if (AlarmHistory.Count > _AlarmHistoryMaxCount)
                        {
                            AlarmHistory.RemoveAt(_AlarmHistoryMaxCount);
                        }

                    }

                }
                #endregion

                #region 初期化

                // 初期化
                NextAlarm.Timer = DateTime.MaxValue;
                NextAlarm.DrugList.Clear();

                // 前回アラームが再通知である場合はリセット
                if (_IsNextRealarm && !_Realarm.Count.Equals(0))
                {
                    _Realarm.RemoveAt(0);
                }

                _IsNextRealarm = false;

                #endregion

                #region 毎時・指定日時・食事・就寝
                for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                {

                    //毎時
                    if (DrugList[iLoop].HourEach.IsDrug)
                    {
                        CompareToTime(DrugList[iLoop].HourEachNextTime, iLoop, DrugList[iLoop].HourEach.Volume, false, true);
                    }

                    //指定日時
                    if (DrugList[iLoop].Appoint.IsDrug)
                    {
                        CompareToTime(DrugList[iLoop].AppointTime, iLoop, DrugList[iLoop].Appoint.Volume, true, false);
                    }

                    //朝食
                    if (DrugList[iLoop].Breakfast.IsDrug)
                    {

                        Setting.Breakfast.Start = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Breakfast.Start, method);
                        Setting.Breakfast.Finish = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Breakfast.Finish, method);

                        Time = CalcMealsTime(Setting.Breakfast.Start, Setting.Breakfast.Finish, DrugList[iLoop].Breakfast.Kind);
                        Time = GetNextDateTime(Setting.BeforeAlarmTime, Time, method);

                        CompareToTime(Time, iLoop, DrugList[iLoop].Breakfast.Volume, false, false);

                    }

                    //昼食
                    if (DrugList[iLoop].Lunch.IsDrug)
                    {

                        Setting.Lunch.Start = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Lunch.Start, method);
                        Setting.Lunch.Finish = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Lunch.Finish, method);

                        Time = CalcMealsTime(Setting.Lunch.Start, Setting.Lunch.Finish, DrugList[iLoop].Lunch.Kind);
                        Time = GetNextDateTime(Setting.BeforeAlarmTime, Time, method);

                        CompareToTime(Time, iLoop, DrugList[iLoop].Lunch.Volume, false, false);

                    }

                    //夕食
                    if (DrugList[iLoop].Dinner.IsDrug)
                    {

                        Setting.Dinner.Start = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Dinner.Start, method);
                        Setting.Dinner.Finish = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Dinner.Finish, method);

                        Time = CalcMealsTime(Setting.Dinner.Start, Setting.Dinner.Finish, DrugList[iLoop].Dinner.Kind);
                        Time = GetNextDateTime(Setting.BeforeAlarmTime, Time, method);

                        CompareToTime(Time, iLoop, DrugList[iLoop].Dinner.Volume, false, false);

                    }

                    //就寝前
                    if (DrugList[iLoop].Sleep.IsDrug)
                    {

                        Setting.Sleep = GetNextDateTime(Setting.BeforeAlarmTime, Setting.Sleep, method);

                        Time = Setting.Sleep.AddMinutes((-1) * Setting.MinuteBeforeSleep);
                        Time = GetNextDateTime(Setting.BeforeAlarmTime, Time, method);

                        CompareToTime(Time, iLoop, DrugList[iLoop].Sleep.Volume, false, false);

                    }

                }
                #endregion

                #region 再通知
                if (!_Realarm.Count.Equals(0))
                {

                    if (_Realarm[0].Timer <= NextAlarm.Timer)
                    {
    
                        // 異なる時刻なら初期化    
                        if (!_Realarm[0].Timer.Equals(NextAlarm.Timer))    
                        {
                                NextAlarm.Timer = _Realarm[0].Timer;
                                NextAlarm.DrugList.Clear();    
                        }
    
                        // 薬登録    
                        for (Int32 iLoop = 0; iLoop < _Realarm[0].DrugList.Count; iLoop++)    
                        {

                            //指定日時、時間毎の薬については、上の処理で登録されているのでスキップする
                            if (!_Realarm[0].DrugList[iLoop].IsAppoint && !_Realarm[0].DrugList[iLoop].IsHourEach)
                            {

                                NextAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug
                                {
                                    Index = _Realarm[0].DrugList[iLoop].Index,
                                    Volume = _Realarm[0].DrugList[iLoop].Volume,
                                    IsAppoint = _Realarm[0].DrugList[iLoop].IsAppoint,
                                    IsHourEach = _Realarm[0].DrugList[iLoop].IsHourEach
                                });

                            }

                        }
    
                        _IsNextRealarm = true;

                    }

                }
                #endregion

                // 保存したアラーム時間から、過去のものを削除
                for (Int32 iLoop = _SaveTimes.Count - 1; iLoop >= 0; iLoop--)
                {
                    if (_SaveTimes[iLoop] < NextAlarm.Timer)
                    {
                        _SaveTimes.RemoveAt(iLoop);
                    }
                }

            }
            else
            {

                // 次回アラーム読込済、FLG初期化
                _IsLoadNextAlarm = false;

            }

            if (NextAlarm.DrugList.Count > 0)
            {

                // アラーム情報の保存
                SaveNextAlarmParameter();

                // AlarmTimer.csでFLGリセット
                Class.UserControl.ResetNextAlarm = true;

            }
            else
            {

                // 仮に、対象がないのに次回アラームとなってしまった場合は、再帰
                Save(true);

            }

        }

        /// <summary>
        /// 次回アラームの保存
        /// </summary>
        private void SaveNextAlarmParameter()
        {

            // 前回パスの取得
            string FilePath = Application.Current.Properties.ContainsKey(UserControl.AlarmFullPath)
                                ? (string)Application.Current.Properties[UserControl.AlarmFullPath]
                                : "";

            // 前回パスが未記入、またはファイルがなければ新規宣言
            if (FilePath.Length.Equals(0) || !File.Exists(FilePath))
            {

                FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                FilePath = Path.Combine(FilePath, UserControl.AlarmFileName);

                Application.Current.Properties[UserControl.AlarmFullPath] = FilePath;

            }

            string TmpPath = FilePath.Replace(UserControl.FileExtension, ".Tmp");

            lock (_NextAlarmLocker)
            {

                //ファイル書き込み開始
                try
                {

                    using (StreamWriter FileData = new StreamWriter(TmpPath, false, Encoding.Unicode))
                    {

                        #region 次回アラーム
                        FileData.WriteLine("");
                        FileData.WriteLine(NAME.NEXTALARM.START);
                        FileData.WriteLine(MakeParameter(NAME.NEXTALARM.TIME, NextAlarm.Timer, true));

                        for (Int32 jLoop = 0; jLoop < NextAlarm.DrugList.Count; jLoop++)
                        {

                            FileData.WriteLine(NAME.ALARMDRUG.START);
                            FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.INDEX, NextAlarm.DrugList[jLoop].Index));
                            FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.VOLUME, NextAlarm.DrugList[jLoop].Volume));
                            FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.APPOINT, NextAlarm.DrugList[jLoop].IsAppoint));
                            FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.HOUREACH, NextAlarm.DrugList[jLoop].IsHourEach));
                            FileData.WriteLine(NAME.ALARMDRUG.END);

                        }

                        FileData.WriteLine(NAME.NEXTALARM.END);
                        #endregion

                        #region 再通知
                        for (Int32 iLoop = 0; iLoop < _Realarm.Count; iLoop++)
                        {

                            FileData.WriteLine("");
                            FileData.WriteLine(NAME.REALARM.START);
                            FileData.WriteLine(MakeParameter(NAME.REALARM.TIME, _Realarm[iLoop].Timer, true));

                            for (Int32 jLoop = 0; jLoop < _Realarm[iLoop].DrugList.Count; jLoop++)
                            {

                                FileData.WriteLine(NAME.ALARMDRUG.START);
                                FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.INDEX, _Realarm[iLoop].DrugList[jLoop].Index));
                                FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.VOLUME, _Realarm[iLoop].DrugList[jLoop].Volume));
                                FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.APPOINT, _Realarm[iLoop].DrugList[jLoop].IsAppoint));
                                FileData.WriteLine(MakeParameter(NAME.ALARMDRUG.HOUREACH, _Realarm[iLoop].DrugList[jLoop].IsHourEach));
                                FileData.WriteLine(NAME.ALARMDRUG.END);

                            }

                            FileData.WriteLine(NAME.REALARM.END);

                        }
                        #endregion

                        #region 保存日時一覧
                        if (_SaveTimes.Count > 0)
                        {
                            FileData.WriteLine("");
                            FileData.WriteLine(NAME.SAVEALARMTIME.START);
                            FileData.WriteLine(MakeParameter(NAME.SAVEALARMTIME.TIME, _SaveTimes));
                            FileData.WriteLine(NAME.SAVEALARMTIME.END);
                        }
                        #endregion

                    }

                    File.Copy(TmpPath, FilePath, true);
                    File.Delete(TmpPath);

                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                }

            }

        }

        /// <summary>
        /// 次回時間の計算
        /// </summary>
        /// <returns>The next date time.</returns>
        /// <param name="date">Date.</param>
        /// <param name="time">Time.</param>
        /// <param name="method">共通メソッド</param>
        private DateTime GetNextDateTime(DateTime date, DateTime time, Method method = null)
        {

            if (method == null)
            {
                method = new Method();
            }

            if (time <= date)
            {
                return method.ConvertToDateTime(date.Year, date.Month, date.AddDays(1).Day, time.Hour, time.Minute, time);
            }
            else
            {
                return time;
            }

        }

        /// <summary>
        /// 次回アラームとなるか比較、Indexを登録する
        /// </summary>
        /// <param name="time">アラーム候補時刻</param>
        /// <param name="index">薬index</param>
        /// <param name="volume">服用錠数</param>
        /// <param name="isAppoint">If set to <c>true</c> drug is appoint.</param>
        /// <param name="isHourEach">If set to <c>true</c> drug is hour each.</param>
        private void CompareToTime(DateTime time, Int32 index, Int32 volume, bool isAppoint, bool isHourEach)
        {

            // 保存済の時刻は服用済のためスキップ
            if (_SaveTimes.IndexOf(time).Equals(-1))
            {

                // 設定時刻より未来はスキップ
                if (time <= NextAlarm.Timer)
                {

                    // 同時刻でない場合、本データを先に処理するため設定済のデータはクリア
                    if (!time.Equals(NextAlarm.Timer))
                    {

                        NextAlarm.Timer = time;
                        NextAlarm.DrugList.Clear();

                    }

                    // 薬追加
                    NextAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug()
                    {
                        Index = index,
                        Volume = volume,
                        IsAppoint = isAppoint,
                        IsHourEach = isHourEach
                    });


                }

            }

        }

        /// <summary>
        /// 食事時の服用時間の計算
        /// </summary>
        /// <returns>服用時間</returns>
        /// <param name="StartTime">開始時間</param>
        /// <param name="FinishTime">終了時間</param>
        /// <param name="Kind">服用タイミング</param>
        private DateTime CalcMealsTime(DateTime StartTime, DateTime FinishTime, UserControl.KindTiming Kind)
        {

            DateTime Return = DateTime.MaxValue;

            switch (Kind)
            {

                case UserControl.KindTiming.Before:
                    Return = StartTime.AddMinutes((-1) * Setting.MinuteBeforeMeals);
                    break;

                case UserControl.KindTiming.Between:
                    Return = new Method().GetAverageTime(StartTime, FinishTime);
                    break;

                case UserControl.KindTiming.After:
                    Return = FinishTime.AddMinutes(Setting.MinuteAfterMeals);
                    break;

                default:
                    Return = StartTime;
                    break;
            }

            return Return;

        }

        /// <summary>
        /// 再通知設定
        /// </summary>
        /// <param name="AfterMinute">再通知設定時間(分)</param>
        public void SetRealarm(Int32 AfterMinute)
        {

            DateTime NextTime = DateTime.Now.AddMinutes(AfterMinute);   //再通知の時刻

            UserControl.AlarmInfo AddAlarm = new UserControl.AlarmInfo()
            {
                Timer = new Class.Method().ConvertToDateTime(NextTime.Year, NextTime.Month, NextTime.Day, NextTime.Hour, NextTime.Minute, NextTime),    //秒単位を0にする
                DrugList = new List<UserControl.AlarmInfo.Drug>()
            };

            // アラーム時刻の保存
            AddSaveAlarmTime();

            //DrugListのIndexを登録
            for (Int32 iLoop = NextAlarm.DrugList.Count - 1; iLoop > -1; iLoop--)
            {

                // Alarm.xamlにて服用対象となった薬か
                if (!NextAlarm.DrugList[iLoop].IsDrug)
                {

                    AddAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug()
                    {
                        Index = NextAlarm.DrugList[iLoop].Index,
                        Volume = NextAlarm.DrugList[iLoop].Volume,
                        IsAppoint = NextAlarm.DrugList[iLoop].IsAppoint,
                        IsHourEach = NextAlarm.DrugList[iLoop].IsHourEach
                    });

                    // 次回アラームより削除
                    NextAlarm.DrugList.RemoveAt(iLoop);

                }

            }

            if (AddAlarm.DrugList.Count > 0)
            {

                // 同時刻の再通知が設定済みか
                Int32 Index = _Realarm.FindIndex(Alarm => { return Alarm.Timer.Equals(AddAlarm.Timer); });

                if (Index.Equals(-1))
                {
                    _Realarm.Add(AddAlarm);
                }
                else
                {

                    AddAlarm.DrugList.ForEach(Drug => 
                    {

                        _Realarm[Index].DrugList.Add(new UserControl.AlarmInfo.Drug()
                        {
                            Index = Drug.Index,
                            Volume = Drug.Volume,
                            IsAppoint = Drug.IsAppoint,
                            IsHourEach = Drug.IsHourEach
                        });

                    });

                }

                //指定時間、時間毎のアラームの時は、元の設定値を更新
                //上のfor文内では再々通知のときに更新できないためここで再ループ
                for (Int32 iLoop = 0; iLoop < _Realarm.Count; iLoop++)
                {

                    if (_Realarm[iLoop].Timer.Equals(AddAlarm.Timer))
                    {

                        for (Int32 jLoop = 0; jLoop < _Realarm[iLoop].DrugList.Count; jLoop++)
                        {

                            //指定時間
                            if (_Realarm[iLoop].DrugList[jLoop].IsAppoint)
                                DrugList[_Realarm[iLoop].DrugList[jLoop].Index].AppointTime = AddAlarm.Timer;

                            //時間毎
                            if (_Realarm[iLoop].DrugList[jLoop].IsHourEach)
                                DrugList[_Realarm[iLoop].DrugList[jLoop].Index].HourEachNextTime = AddAlarm.Timer;

                        }

                    }

                }

                //時間の昇順に並び替え
                _Realarm.Sort((a, b) => DateTime.Compare(a.Timer, b.Timer));

            }

            // 次回アラームが空の場合、次回アラームの設定
            if (NextAlarm.DrugList.Count.Equals(0))
                Save(true);

        }

        /// <summary>
        /// NextAlarm一覧より服用する薬を選択
        /// </summary>
        /// <param name="index">NextAlarm.DrugList[Index]</param>
        /// <param name="isDrug">服用するか</param>
        public void UpdateDrugFlgNextAlarm(Int32 index, bool isDrug)
        {
            if (-1 < index && index < NextAlarm.DrugList.Count)
                NextAlarm.DrugList[index].IsDrug = isDrug;
        }

        /// <summary>
        /// 対象Indexの薬削除
        /// </summary>
        /// <param name="index">DrugList[Index]</param>
        public void DeleteDrug(Int32 index)
        {

            if (-1 < index && index < DrugList.Count)
            {

                // 再通知
                for (Int32 iLoop = _Realarm.Count - 1; iLoop >= 0; iLoop--)
                {

                    for (Int32 jLoop = _Realarm[iLoop].DrugList.Count - 1; jLoop >= 0; jLoop--)
                    {

                        if (_Realarm[iLoop].DrugList[jLoop].Index.Equals(index))
                        {
                            _Realarm[iLoop].DrugList.RemoveAt(jLoop);
                        }
                        else if(_Realarm[iLoop].DrugList[jLoop].Index > index)
                        {
                            _Realarm[iLoop].DrugList[jLoop].Index -= 1;
                        }

                    }

                    if (_Realarm[iLoop].DrugList.Count.Equals(0))
                    {
                        _Realarm.RemoveAt(iLoop);
                    }

                }

                // 履歴
                for (Int32 iLoop = AlarmHistory.Count - 1; iLoop >= 0; iLoop--)
                {

                    for (Int32 jLoop = AlarmHistory[iLoop].DrugList.Count - 1; jLoop >= 0; jLoop--)
                    {

                        if (AlarmHistory[iLoop].DrugList[jLoop].Index.Equals(index))
                        {
                            AlarmHistory[iLoop].DrugList.RemoveAt(jLoop);
                        }
                        else if (AlarmHistory[iLoop].DrugList[jLoop].Index > index)
                        {
                            AlarmHistory[iLoop].DrugList[jLoop].Index -= 1;
                        }

                    }

                    if (AlarmHistory[iLoop].DrugList.Count.Equals(0))
                    {
                        AlarmHistory.RemoveAt(iLoop);
                    }

                }

                // 薬切れ
                for (Int32 iLoop = PrescriptionList.Count - 1; iLoop >= 0; iLoop--)
                {

                    if (PrescriptionList[iLoop].Equals(index))
                    {
                        PrescriptionList.RemoveAt(iLoop);
                    }
                    else if (PrescriptionList[iLoop] > index)
                    {
                        PrescriptionList[iLoop] -= 1;
                    }

                }

                // 薬一覧
                DrugList.RemoveAt(index);

                // データ保存
                Save(false);

            }

        }

    }
}
