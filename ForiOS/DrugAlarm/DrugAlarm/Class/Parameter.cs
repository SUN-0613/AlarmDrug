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

            string FilePath;

            //前回パスの取得
            if (Application.Current.Properties.ContainsKey(UserControl.FullPath))
            {
                FilePath = (string)Application.Current.Properties[UserControl.FullPath];
            }
            else
            {
                FilePath = "";
            }

            //前回パスが未記入、またはファイルがなければ新規作成
            if (FilePath.Length.Equals(0) || !File.Exists(FilePath))
            {
                FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                FilePath = Path.Combine(FilePath, UserControl.FileName);

                Application.Current.Properties[UserControl.FullPath] = FilePath;

                if (!File.Exists(FilePath))
                {
                    Save();
                }

            }

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

                                    case NAME.DRUG.TIMING:

                                        switch (Strings[1])
                                        {

                                            case NAME.DRUG.BEFOREMEALS:
                                                DrugList[Index].Breakfast.Kind = UserControl.KindTiming.Before;
                                                DrugList[Index].Lunch.Kind = UserControl.KindTiming.Before;
                                                DrugList[Index].Dinner.Kind = UserControl.KindTiming.Before;
                                                break;

                                            case NAME.DRUG.BETWEENMEALS:
                                                DrugList[Index].Breakfast.Kind = UserControl.KindTiming.Between;
                                                DrugList[Index].Lunch.Kind = UserControl.KindTiming.Between;
                                                DrugList[Index].Dinner.Kind = UserControl.KindTiming.Between;
                                                break;

                                            case NAME.DRUG.AFTERMEALS:
                                                DrugList[Index].Breakfast.Kind = UserControl.KindTiming.After;
                                                DrugList[Index].Lunch.Kind = UserControl.KindTiming.After;
                                                DrugList[Index].Dinner.Kind = UserControl.KindTiming.After;
                                                break;

                                            default:
                                                DrugList[Index].Breakfast.Kind = UserControl.KindTiming.Before;
                                                DrugList[Index].Lunch.Kind = UserControl.KindTiming.Before;
                                                DrugList[Index].Dinner.Kind = UserControl.KindTiming.Before;
                                                break;

                                        }

                                        break;

                                    case NAME.DRUG.BREAKFAST:
                                        DrugList[Index].Breakfast.IsDrug = true;
                                        DrugList[Index].Breakfast.Volume = method.ConvertToInt32(Strings[1], DrugList[Index].Breakfast.Volume);
                                        break;

                                    case NAME.DRUG.LUNCH:
                                        DrugList[Index].Lunch.IsDrug = true;
                                        DrugList[Index].Lunch.Volume = method.ConvertToInt32(Strings[1], DrugList[Index].Lunch.Volume);
                                        break;

                                    case NAME.DRUG.DINNER:
                                        DrugList[Index].Dinner.IsDrug = true;
                                        DrugList[Index].Dinner.Volume = method.ConvertToInt32(Strings[1], DrugList[Index].Dinner.Volume);
                                        break;

                                    case NAME.DRUG.SLEEP:
                                        DrugList[Index].Sleep.IsDrug = true;
                                        DrugList[Index].Sleep.Volume = method.ConvertToInt32(Strings[1], DrugList[Index].Sleep.Volume);
                                        break;

                                    case NAME.DRUG.TOBETAKEN:
                                        DrugList[Index].ToBeTaken.IsDrug = true;
                                        DrugList[Index].ToBeTaken.Volume = method.ConvertToInt32(Strings[1], DrugList[Index].ToBeTaken.Volume);
                                        break;

                                    case NAME.DRUG.APPOINTTIME:
                                        DrugList[Index].Appoint.IsDrug = true;
                                        DrugList[Index].AppointTime = method.ConvertToDateTime(Values[0], DrugList[Index].AppointTime);
                                        DrugList[Index].Appoint.Volume = method.ConvertToInt32(Values[1], DrugList[Index].Appoint.Volume);
                                        break;

                                    case NAME.DRUG.HOUREACH:
                                        DrugList[Index].HourEach.IsDrug = true;
                                        DrugList[Index].HourEachTime = method.ConvertToInt32(Values[0], DrugList[Index].HourEachTime);
                                        DrugList[Index].HourEach.Volume = method.ConvertToInt32(Values[1], DrugList[Index].HourEach.Volume);
                                        DrugList[Index].HourEachNextTime = method.ConvertToDateTime(Values[2], DateTime.Now.AddHours(DrugList[Index].HourEachTime));
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

            //次回アラーム取得
            SetNextAlarm();

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
            bool IsMeals = false;

            Message.Clear();

            #region 朝昼夕
            if (DrugList[Index].Breakfast.IsDrug && DrugList[Index].Lunch.IsDrug && DrugList[Index].Dinner.IsDrug)
            {
                Message.Append(Resx.Resources.Parameter_Timing_Always);
                IsMeals = true;
            }
            else
            {

                if (DrugList[Index].Breakfast.IsDrug)
                {
                    Message.Append(Resx.Resources.Parameter_Timing_Breakfast);
                    IsMeals = true;
                }

                if (DrugList[Index].Lunch.IsDrug)
                {
                    Message.Append(Resx.Resources.Parameter_Timing_Lunch);
                    IsMeals = true;
                }

                if (DrugList[Index].Dinner.IsDrug)
                {
                    Message.Append(Resx.Resources.Parameter_Timing_Dinner);
                    IsMeals = true;
                }

            }
            #endregion

            #region 食前、食間、食後
            if (IsMeals)
            {

                switch (DrugList[Index].Breakfast.Kind)
                {

                    case UserControl.KindTiming.Before:
                        Message.Append(Resx.Resources.Parameter_Timing_Before);
                        break;

                    case UserControl.KindTiming.Between:
                        Message.Append(Resx.Resources.Parameter_Timing_Between);
                        break;

                    case UserControl.KindTiming.After:
                        Message.Append(Resx.Resources.Parameter_Timing_After);
                        break;

                    default:
                        break;

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
        /// ファイル書き込み
        /// </summary>
        public void Save()
        {

            //保存パスの取得
            string FilePath = (string)Application.Current.Properties[UserControl.FullPath];
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
                    #endregion

                    #region 薬パラメータ
                    for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                    {

                        FileData.WriteLine("");
                        FileData.WriteLine(NAME.DRUG.START);
                        FileData.WriteLine(MakeParameter(NAME.DRUG.NAME, DrugList[iLoop].Name, false));

                        if (DrugList[iLoop].Breakfast.IsDrug || DrugList[iLoop].Lunch.IsDrug || DrugList[iLoop].Dinner.IsDrug)
                        {

                            //朝昼夕ともに同設定のため、朝のみで判断
                            switch (DrugList[iLoop].Breakfast.Kind)
                            {

                                case UserControl.KindTiming.Before:
                                    FileData.WriteLine(MakeParameter(NAME.DRUG.TIMING, NAME.DRUG.BEFOREMEALS, false));
                                    break;

                                case UserControl.KindTiming.Between:
                                    FileData.WriteLine(MakeParameter(NAME.DRUG.TIMING, NAME.DRUG.BETWEENMEALS, false));
                                    break;

                                case UserControl.KindTiming.After:
                                    FileData.WriteLine(MakeParameter(NAME.DRUG.TIMING, NAME.DRUG.AFTERMEALS, false));
                                    break;

                                default:
                                    FileData.WriteLine(MakeParameter(NAME.DRUG.TIMING, NAME.DRUG.BEFOREMEALS, false));
                                    break;

                            }

                        }

                        if (DrugList[iLoop].Breakfast.IsDrug)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.BREAKFAST, DrugList[iLoop].Breakfast.Volume));
                        }

                        if (DrugList[iLoop].Lunch.IsDrug)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.LUNCH, DrugList[iLoop].Lunch.Volume));
                        }

                        if (DrugList[iLoop].Dinner.IsDrug)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.DINNER, DrugList[iLoop].Dinner.Volume));
                        }

                        if (DrugList[iLoop].Sleep.IsDrug)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.SLEEP, DrugList[iLoop].Sleep.Volume));
                        }

                        if (DrugList[iLoop].ToBeTaken.IsDrug)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.TOBETAKEN, DrugList[iLoop].ToBeTaken.Volume));
                        }

                        if (DrugList[iLoop].Appoint.IsDrug && DrugList[iLoop].AppointTime > DateTime.Now)
                        {
                            FileData.WriteLine(MakeParameter(NAME.DRUG.APPOINTTIME, DrugList[iLoop].AppointTime, DrugList[iLoop].Appoint.Volume));
                        }
                        else
                        {
                            //過去の場合は服用設定を解除
                            DrugList[iLoop].Appoint.IsDrug = false;
                        }

                        if (DrugList[iLoop].HourEach.IsDrug)
                        {

                            if (DrugList[iLoop].HourEachNextTime.Equals(DateTime.MaxValue))
                            {
                                DrugList[iLoop].HourEachNextTime = DateTime.Now.AddHours(DrugList[iLoop].HourEachTime);
                            }

                            FileData.WriteLine(MakeParameter(NAME.DRUG.HOUREACH, DrugList[iLoop].HourEachTime, DrugList[iLoop].HourEach.Volume, DrugList[iLoop].HourEachNextTime));

                        }

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
        /// パラメータ数値形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">V数値</param>
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
        /// パラメータ時間形式作成
        /// </summary>
        /// <returns>パラメータ出力値</returns>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">時間</param>
        private string MakeParameter(string Parameter, DateTime Value)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.TimeFormat.Length + 1);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(Value.ToString(UserControl.TimeFormat));

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
        private string MakeParameter(string Parameter, DateTime AppointTime, Int32 Volume)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + UserControl.DateTimeFormat.Length + Volume.ToString().Length + 2);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(AppointTime.ToString(UserControl.DateTimeFormat));
                Str.Append(",").Append(Volume.ToString());

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
        private string MakeParameter(string Parameter, Int32 HourEach, Int32 Volume, DateTime NextTime)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + HourEach.ToString().Length + Volume.ToString().Length + UserControl.DateTimeFormat.Length + 3);
            string Return;

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=").Append(HourEach.ToString());
                Str.Append(",").Append(Volume.ToString());
                Str.Append(",").Append(NextTime.ToString(UserControl.DateTimeFormat));

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
        /// 薬服用時の処理
        /// </summary>
        /// <returns><c>true</c>, 薬切れ有, <c>false</c> 在庫十分</returns>
        public bool TakeMedicine()
        {

            bool Return = false;

            //総量から服用錠を減算
            for (Int32 iLoop = 0; iLoop < NextAlarm.DrugList.Count; iLoop++)
            {

                Int32 Index = NextAlarm.DrugList[iLoop].Index;
                Int32 Volume = NextAlarm.DrugList[iLoop].Volume;

                DrugList[Index].TotalVolume -= Volume;
                if (DrugList[Index].TotalVolume < 0)
                {
                    DrugList[Index].TotalVolume = 0;
                }

                //指定時間による服用なら、指定時間の設定を解除する
                if (NextAlarm.DrugList[iLoop].IsAppoint)
                {
                    DrugList[Index].Appoint.IsDrug = false;
                }

                //時間毎による服用なら、次回時刻を設定
                if (NextAlarm.DrugList[iLoop].IsHourEach)
                {
                    DrugList[Index].HourEachNextTime = DateTime.Now.AddHours(DrugList[Index].HourEachTime);
                }

            }

            //パラメータ更新
            Save();

            //残量チェック
            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {

                Return = DrugList[iLoop].IsPrescriptionAlarm;

                if (Return)
                {
                    break;
                }

            }

            return Return;

        }

        /// <summary>
        /// 次回アラーム情報の設定
        /// </summary>
        private void SetNextAlarm()
        {

            Method method = new Method();
            DateTime Time;
            DateTime BeforeAlarmTime;   //前回アラーム時間

            //アラーム時間の記憶
            BeforeAlarmTime = NextAlarm.Timer.Equals(DateTime.MaxValue) ? DateTime.Now : NextAlarm.Timer;

            //初期値
            NextAlarm.Timer = DateTime.MaxValue;
            NextAlarm.DrugList.Clear();

            //前回が再通知の設定時刻だった場合は再通知設定を初期化
            if (IsSetRealarm)
            {
                Realarm.Timer = DateTime.MaxValue;
                Realarm.DrugList.Clear();
            }

            //再通知
            if (Realarm.DrugList.Count > 0 && Realarm.Timer <= NextAlarm.Timer)
            {

                //同時刻ならば追加、現在設定値より前時刻なら新規登録
                if (!NextAlarm.Timer.Equals(Realarm.Timer))
                {
                    NextAlarm.DrugList.Clear();
                }

                //DrugListのIndexを登録
                for (Int32 iLoop = 0; iLoop < Realarm.DrugList.Count; iLoop++)
                {

                    if (NextAlarm.DrugList.FindIndex(Drug => { return Drug.Index.Equals(Realarm.DrugList[iLoop].Index); }).Equals(-1))
                    {

                        NextAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug
                        {
                            Index = Realarm.DrugList[iLoop].Index,
                            Volume = Realarm.DrugList[iLoop].Volume,
                            IsAppoint = Realarm.DrugList[iLoop].IsAppoint,
                            IsHourEach = Realarm.DrugList[iLoop].IsHourEach
                        });

                    }

                }

                //アラーム時刻の更新
                NextAlarm.Timer = Realarm.Timer;
                IsSetRealarm = true;

            }

            //毎時・指定日時・食事・就寝
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
                    Time = CalcMealsTime(BeforeAlarmTime, Setting.Breakfast.Start, Setting.Breakfast.Finish, DrugList[iLoop].Breakfast.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Breakfast.Volume, false, false);
                }

                //昼食
                if (DrugList[iLoop].Lunch.IsDrug)
                {
                    Time = CalcMealsTime(BeforeAlarmTime, Setting.Lunch.Start, Setting.Lunch.Finish, DrugList[iLoop].Lunch.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Lunch.Volume, false, false);
                }

                //夕食
                if (DrugList[iLoop].Dinner.IsDrug)
                {
                    Time = CalcMealsTime(BeforeAlarmTime, Setting.Dinner.Start, Setting.Dinner.Finish, DrugList[iLoop].Dinner.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Dinner.Volume, false, false);
                }

                //就寝前
                if (DrugList[iLoop].Sleep.IsDrug)
                {

                    Time = Setting.Sleep.AddMinutes((-1) * Setting.MinuteBeforeSleep);

                    //取得した時間を超過している場合は翌日にする
                    if (Time < BeforeAlarmTime)
                    {
                        Time = Time.AddDays(1);
                    }

                    CompareToTime(Time, iLoop, DrugList[iLoop].Sleep.Volume, false, false);

                }

            }

        }

        /// <summary>
        /// 次回アラームとなるか比較、Indexを登録する
        /// </summary>
        /// <param name="Time">アラーム候補時刻</param>
        /// <param name="DrugIndex">薬index</param>
        /// <param name="DrugVolume">服用錠数</param>
        /// <param name="DrugIsAppoint">If set to <c>true</c> drug is appoint.</param>
        /// <param name="DrugIsHourEach">If set to <c>true</c> drug is hour each.</param>
        private void CompareToTime(DateTime Time, Int32 DrugIndex, Int32 DrugVolume, bool DrugIsAppoint, bool DrugIsHourEach)
        {

            if (Time <= NextAlarm.Timer)
            {

                //同時刻ならば追加、現在設定値より前時刻なら新規登録
                if (!NextAlarm.Timer.Equals(Time))
                {

                    NextAlarm.DrugList.Clear();
                    IsSetRealarm = false;       //再通知は次回持越

                }

                //DrugListのIndexを登録
                if (NextAlarm.DrugList.FindIndex(Drug => { return Drug.Index.Equals(DrugIndex); }).Equals(-1))
                {
                    NextAlarm.DrugList.Add(new UserControl.AlarmInfo.Drug()
                    {
                        Index = DrugIndex,
                        Volume = DrugVolume,
                        IsAppoint = DrugIsAppoint,
                        IsHourEach = DrugIsHourEach
                    });
                }

                //アラーム時刻の更新
                NextAlarm.Timer = Time;

            }

        }

        /// <summary>
        /// 食事時の服用時間の計算
        /// </summary>
        /// <returns>服用時間</returns>
        /// <param name="BeforeAlarmTime">前回アラーム時間</param>
        /// <param name="StartTime">開始時間</param>
        /// <param name="FinishTime">終了時間</param>
        /// <param name="Kind">服用タイミング</param>
        private DateTime CalcMealsTime(DateTime BeforeAlarmTime, DateTime StartTime, DateTime FinishTime, UserControl.KindTiming Kind)
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

            //取得した時間を超過している場合は翌日にする
            if (Return < BeforeAlarmTime)
            {
                Return = Return.AddDays(1);
            }

            return Return;

        }

        /// <summary>
        /// 再通知設定
        /// </summary>
        /// <param name="AfterMinute">再通知設定時間(分)</param>
        public void SetRealarm(Int32 AfterMinute)
        {

            //DrugListのIndexを登録
            for (Int32 iLoop = 0; iLoop < NextAlarm.DrugList.Count; iLoop++)
            {

                if (Realarm.DrugList.FindIndex(Drug => { return Drug.Index.Equals(NextAlarm.DrugList[iLoop].Index); }).Equals(-1))
                {

                    Realarm.DrugList.Add(new UserControl.AlarmInfo.Drug()
                    {
                        Index = NextAlarm.DrugList[iLoop].Index,
                        Volume = NextAlarm.DrugList[iLoop].Volume,
                        IsAppoint = NextAlarm.DrugList[iLoop].IsAppoint,
                        IsHourEach = NextAlarm.DrugList[iLoop].IsHourEach
                    });

                }

            }

            //設定済時刻と今回設定時刻のうち、近い方に合わせる
            //ただし、設定済時刻が現在時刻より以前であれば今回設定時刻を採用する
            DateTime NextTime = DateTime.Now.AddMinutes(AfterMinute);
            if (NextTime < Realarm.Timer || Realarm.Timer <= DateTime.Now)
            {
                Realarm.Timer = NextTime;
            }

            IsSetRealarm = false;

            //次回アラームの設定
            SetNextAlarm();

        }

    }
}
