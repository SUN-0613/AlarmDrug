using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Collections.ObjectModel;

namespace DrugAlarm.Class
{

    /// <summary>
    /// 設定パラメータ
    /// </summary>
    /// <remarks>
    /// 
    /// パラメータはファイルに保存する
    /// 保存場所は初回起動時にフォルダ選択を行う。
    /// フォルダ、ファイル名はSettings.settingsにて管理
    /// 
    /// パラメータファイルでは設定データと薬データ郡を管理
    /// パラメータ値は改行区切で管理
    /// 
    /// 設定データはSettingStart,SettingEndで囲む
    /// 薬データはDrugStart,DrugEndで囲む
    /// 各々のパラメータ名称はclass NAMEに記載
    /// ファイルへの記入方法は「パラメータ名称=パラメータ値」
    /// パラメータ値が時間範囲の場合は開始時間,終了時間とカンマで区切る
    /// 備考欄は改行入力を許可し、ファイル保存時は_CRLF_に置換
    /// 
    /// </remarks>
    public class Parameter : IDisposable
    {

        /// <summary>
        /// パラメータ時間フォーマット
        /// </summary>
        private const string TimeFormat = "HH:mm";

        /// <summary>
        /// 範囲時間
        /// </summary>
        public class BetweenTime
        {

            /// <summary>
            /// 開始時間
            /// </summary>
            public DateTime Start;

            /// <summary>
            /// 終了時間
            /// </summary>
            public DateTime Finish;

            /// <summary>
            /// new
            /// </summary>
            public BetweenTime()
            {
                Start = DateTime.MaxValue;
                Finish = DateTime.MaxValue;
            }

            /// <summary>
            /// 開始・終了時間を比較し、前後逆なら並び替え
            /// </summary>
            public void CompareTimes()
            {
                if (Start.CompareTo(Finish).Equals(1))
                {
                    DateTime Tmp = Start;
                    Start = Finish;
                    Finish = Tmp;
                }
            }

        }

        /// <summary>
        /// 次回アラーム情報
        /// </summary>
        public class AlarmInfo
        {

            /// <summary>
            /// アラーム時間
            /// </summary>
            public DateTime Timer;

            /// <summary>
            /// Parameter.DrugList[Index]
            /// </summary>
            public List<Int32> Index;

            /// <summary>
            /// 服用錠
            /// </summary>
            public List<Int32> Volume;

            /// <summary>
            /// new
            /// </summary>
            public AlarmInfo()
            {
                Timer = DateTime.MaxValue;
                Index = new List<Int32>();
                Volume = new List<Int32>();
            }

        }

        /// <summary>
        /// 次回アラームプロパティ
        /// </summary>
        public AlarmInfo NextAlarm { get; private set; }

        /// <summary>
        /// 再通知
        /// </summary>
        private AlarmInfo ReAlarm;

        /// <summary>
        /// パラメータメソッド一覧
        /// </summary>
        public class Method
        {

            /// <summary>
            /// 指定時間を今日日付のDateTime型に変換
            /// </summary>
            /// <param name="Hour">時</param>
            /// <param name="Minute">分</param>
            /// <returns>今日日付のDateTime型</returns>
            public DateTime GetTodayTime(Int32 Hour, Int32 Minute)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);
            }

            /// <summary>
            /// 指定時間を今日日付のDateTime型に変換
            /// </summary>
            /// <param name="Time">HH:mm</param>
            /// <returns>今日日付のDateTime型</returns>
            public DateTime GetTodayTime(string Time)
            {

                string[] Values = Time.Split(':');
                Int32 Hour;
                Int32 Minute;

                if (!Int32.TryParse(Values[0], out Hour))
                {
                    Hour = 0;
                }
                if (!Int32.TryParse(Values[1], out Minute))
                {
                    Minute = 0;
                }

                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);
            }

            /// <summary>
            /// 次回時間の取得
            /// </summary>
            /// <param name="Time">設定時間</param>
            public DateTime GetNextDateTime(DateTime Time)
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
            /// 文字列を整数値に変換
            /// 変換できない場合は引数値を返す
            /// </summary>
            /// <param name="Value">変換文字列</param>
            /// <param name="DefaultValue">デフォルト値</param>
            /// <returns>変換後数値</returns>
            public Int32 ConvertToInt32(string Value, Int32 DefaultValue)
            {
                Int32 Return;

                if (!Int32.TryParse(Value, out Return))
                {
                    Return = DefaultValue;
                }

                return Return;

            }

            /// <summary>
            /// 文字列を日付型に変換
            /// 変換できない場合は引数値を返す
            /// </summary>
            /// <param name="Value">変換文字列</param>
            /// <param name="DefaultValue">デフォルト値</param>
            /// <returns>変換後日付型</returns>
            public DateTime ConvertDateTime(string Value, DateTime DefaultValue)
            {


                if (Value.Split(':').Length != 2)
                {
                    Value += ":00";
                }

                if (!DateTime.TryParse(Value, out DateTime Return))
                {
                    Return = DefaultValue;
                }

                return Return;

            }

            /// <summary>
            /// 改行変換
            /// CRLF→"_CRLF_"
            /// または"_CRLF_"→CRLF
            /// </summary>
            /// <param name="Str">対象文字列</param>
            /// <returns>変換後文字列</returns>
            public string ConvertCRLF(string Str)
            {

                const string CRLF = "\r\n";
                const string _CRLF_ = "_CRLF_";

                if (Str.Contains(CRLF))
                {
                    return Str.Replace(CRLF, _CRLF_);
                }
                else
                {
                    return Str.Replace(_CRLF_, CRLF);
                }
            }

        }

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
        public class SettingParameter : Method
        {

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
                Breakfast = new BetweenTime
                {
                    Start = GetTodayTime(6, 0),
                    Finish = GetTodayTime(6, 30)
                };
                Lunch = new BetweenTime
                {
                    Start = GetTodayTime(12, 0),
                    Finish = GetTodayTime(12, 30)
                };
                Dinner = new BetweenTime
                {
                    Start = GetTodayTime(18, 0),
                    Finish = GetTodayTime(18, 30)
                };
                Sleep = GetTodayTime(23, 0);
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
            public class Timing
            {

                /// <summary>
                /// 服用プロパティ
                /// </summary>
                public bool IsDrug;

                /// <summary>
                /// 服用時間プロパティ
                /// </summary>
                public KindTiming Kind;

                /// <summary>
                /// 錠数プロパティ
                /// </summary>
                public Int32 Volume;

                /// <summary>
                /// new
                /// </summary>
                public Timing()
                {
                    this.IsDrug = false;
                    this.Kind = KindTiming.None;
                    this.Volume = 0;
                }

            }

            /// <summary>
            /// 設定データ読込・書込中
            /// </summary>
            public bool IsAccess;

            /// <summary>
            /// 名称プロパティ
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
            public Timing Appoint;

            /// <summary>
            /// 指定時間プロパティ
            /// </summary>
            public DateTime AppointTime;

            /// <summary>
            /// 時間毎(時)
            /// </summary>
            public Timing HourEach;

            /// <summary>
            /// 時間毎(時)プロパティ
            /// </summary>
            public Int32 HourEachTime;

            /// <summary>
            /// 処方量
            /// </summary>
            private Int32 _TotalVolume;

            /// <summary>
            /// お知らせを表示する薬残量
            /// </summary>
            private Int32 _PrescriptionAlarmVolume;

            /// <summary>
            /// 処方量プロパティ
            /// </summary>
            public Int32 TotalVolume
            {
                get { return _TotalVolume; }
                set
                {
                    if (value < 0)
                    {
                        _TotalVolume = 0;
                    }
                    else
                    {
                        _TotalVolume = value;
                    }
                    UpdatePrescription();
                }
            }

            /// <summary>
            /// お知らせを表示する薬残量プロパティ
            /// </summary>
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
            /// 薬切れお知らせアラームプロパティ
            /// </summary>
            public bool IsPrescriptionAlarm;

            /// <summary>
            /// 備考プロパティ
            /// </summary>
            public string Remarks;

            /// <summary>
            /// 服用タイミングを一覧に表示プロパティ
            /// </summary>
            public string DrugTiming;

            /// <summary>
            /// 初期値設定
            /// </summary>
            public DrugParameter()
            {

                IsAccess = false;
                Name = "";
                Breakfast = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.None,
                    Volume = 0
                };
                Lunch = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.None,
                    Volume = 0
                };
                Dinner = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.None,
                    Volume = 0
                };
                Sleep = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.Before,
                    Volume = 0
                };
                ToBeTaken = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.None,
                    Volume = 0
                };
                Appoint = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.Appoint,
                    Volume = 0
                };
                AppointTime = DateTime.Now;
                HourEach = new Timing
                {
                    IsDrug = false,
                    Kind = KindTiming.TimeEach,
                    Volume = 0
                };
                HourEachTime = 2;
                TotalVolume = 0;
                PrescriptionAlarmVolume = 0;
                Remarks = "";
                DrugTiming = "";

            }

            /// <summary>
            /// お知らせ薬残量更新
            /// </summary>
            private void UpdatePrescription()
            {
                IsPrescriptionAlarm = (_TotalVolume <= _PrescriptionAlarmVolume);
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

            NextAlarm = new AlarmInfo();
            ReAlarm = new AlarmInfo();

            this.Load();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            Setting = null;

            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {
                DrugList[iLoop] = null;
            }
            DrugList.Clear();
            DrugList = null;

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        public void Load()
        {

            //前回パスの取得
            string Path = Properties.Settings.Default.ParameterFullPath;

            //前回パスが未記入、またはファイルがなければ新規に作成する
            if (Path.Length == 0 || !System.IO.File.Exists(Path))
            {

                Path = SelectDirectory();

                Properties.Settings.Default.ParameterFullPath = Path;
                Properties.Settings.Default.Save();

                if (!System.IO.File.Exists(Path))
                {
                    this.Save();
                }

            }

            //ファイル読込開始
            using (System.IO.StreamReader file = new System.IO.StreamReader(Path, Encoding.Unicode))
            {

                Method method = new Method();
                StringBuilder NewLine = new StringBuilder();
                Int32 Index = 0;

                DrugList.Clear();

                while (!file.EndOfStream)
                {

                    try
                    {

                        NewLine.Append(file.ReadLine());

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

                            string[] Str = NewLine.ToString().Split('=');
                            string[] Values = Str[1].Split(',');

                            if (Setting.IsAccess)
                            {
                                //設定パラメータ
                                switch (Str[0])
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
                                        Setting.Sleep = method.GetTodayTime(Str[1]);
                                        break;

                                    case NAME.SETTING.BEFOREMEALS:
                                        Setting.MinuteBeforeMeals = method.ConvertToInt32(Str[1], Setting.MinuteBeforeMeals);
                                        break;

                                    case NAME.SETTING.BETWEENMEALS:
                                        Setting.MinuteBetweenMeals = method.ConvertToInt32(Str[1], Setting.MinuteBetweenMeals);
                                        break;

                                    case NAME.SETTING.AFTERMEALS:
                                        Setting.MinuteAfterMeals = method.ConvertToInt32(Str[1], Setting.MinuteAfterMeals);
                                        break;

                                    case NAME.SETTING.BEFORESLEEP:
                                        Setting.MinuteBeforeSleep = method.ConvertToInt32(Str[1], Setting.MinuteBeforeSleep);
                                        break;

                                    case NAME.SETTING.REALARM:
                                        Setting.MinuteReAlarm = method.ConvertToInt32(Str[1], Setting.MinuteReAlarm);
                                        break;

                                    default:
                                        break;

                                }

                            }
                            else if (DrugList[Index].IsAccess)
                            {

                                //薬パラメータ
                                switch (Str[0])
                                {
                                    case NAME.DRUG.NAME:
                                        DrugList[Index].Name = Str[1];
                                        break;

                                    case NAME.DRUG.BEFOREMEALS:
                                        DrugList[Index].Breakfast.Kind = DrugParameter.KindTiming.Before;
                                        DrugList[Index].Lunch.Kind = DrugParameter.KindTiming.Before;
                                        DrugList[Index].Dinner.Kind = DrugParameter.KindTiming.Before;
                                        break;

                                    case NAME.DRUG.BETWEENMEALS:
                                        DrugList[Index].Breakfast.Kind = DrugParameter.KindTiming.Between;
                                        DrugList[Index].Lunch.Kind = DrugParameter.KindTiming.Between;
                                        DrugList[Index].Dinner.Kind = DrugParameter.KindTiming.Between;
                                        break;

                                    case NAME.DRUG.AFTERMEALS:
                                        DrugList[Index].Breakfast.Kind = DrugParameter.KindTiming.After;
                                        DrugList[Index].Lunch.Kind = DrugParameter.KindTiming.After;
                                        DrugList[Index].Dinner.Kind = DrugParameter.KindTiming.After;
                                        break;

                                    case NAME.DRUG.BREAKFAST:
                                        DrugList[Index].Breakfast.IsDrug = true;
                                        DrugList[Index].Breakfast.Volume = method.ConvertToInt32(Str[1], DrugList[Index].Breakfast.Volume);
                                        break;

                                    case NAME.DRUG.LUNCH:
                                        DrugList[Index].Lunch.IsDrug = true;
                                        DrugList[Index].Lunch.Volume = method.ConvertToInt32(Str[1], DrugList[Index].Lunch.Volume);
                                        break;

                                    case NAME.DRUG.DINNER:
                                        DrugList[Index].Dinner.IsDrug = true;
                                        DrugList[Index].Dinner.Volume = method.ConvertToInt32(Str[1], DrugList[Index].Dinner.Volume);
                                        break;

                                    case NAME.DRUG.TOBETAKEN:
                                        DrugList[Index].ToBeTaken.IsDrug = true;
                                        DrugList[Index].ToBeTaken.Volume = method.ConvertToInt32(Str[1], DrugList[Index].ToBeTaken.Volume);
                                        break;

                                    case NAME.DRUG.APPOINTTIME:
                                        DrugList[Index].Appoint.IsDrug = true;
                                        DrugList[Index].AppointTime = method.ConvertDateTime(Values[0], DrugList[Index].AppointTime);
                                        DrugList[Index].Appoint.Volume = method.ConvertToInt32(Values[1], DrugList[Index].Appoint.Volume);
                                        break;

                                    case NAME.DRUG.HOUREACH:
                                        DrugList[Index].HourEach.IsDrug = true;
                                        DrugList[Index].HourEachTime = method.ConvertToInt32(Values[0], DrugList[Index].HourEachTime);
                                        DrugList[Index].HourEach.Volume = method.ConvertToInt32(Values[1], DrugList[Index].HourEach.Volume);
                                        break;

                                    case NAME.DRUG.TOTALVOLUME:
                                        DrugList[Index].TotalVolume = method.ConvertToInt32(Str[1], DrugList[Index].TotalVolume);
                                        break;

                                    case NAME.DRUG.PRESCRIPTIOIN:
                                        DrugList[Index].PrescriptionAlarmVolume = method.ConvertToInt32(Str[1], DrugList[Index].PrescriptionAlarmVolume);
                                        break;

                                    case NAME.DRUG.REMARKS:
                                        DrugList[Index].Remarks = method.ConvertCRLF(Str[1]);
                                        break;

                                    default:
                                        break;

                                }

                            }

                        }

                    }
                    catch (Exception ex)
                    {

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
        /// パラメータファイルを保存するフォルダを選択し、フルパスを返す
        /// </summary>
        /// <returns>パラメータファイルフルパス</returns>
        private string SelectDirectory()
        {

            string Ret = "";

            //フォルダ選択ダイアログ
            using (FolderBrowserDialog Dialog = new FolderBrowserDialog())
            {

                Dialog.Description = Properties.Resources.FolderDialog_Title;
                Dialog.RootFolder = Environment.SpecialFolder.MyComputer;
                Dialog.ShowNewFolderButton = true;

                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    //選択されたフォルダ内にパラメータファイルを保存
                    Ret = System.IO.Path.Combine(Dialog.SelectedPath, Properties.Settings.Default.ParameterFile);

                }
                else
                {
                    System.Windows.Application.Current.Shutdown();
                }

            }

            return Ret;

        }

        /// <summary>
        /// 一覧表示する服用タイミングメッセージの作成
        /// </summary>
        /// <param name="Index">薬パラメータIndex</param>
        /// <returns>メッセージ</returns>
        private string MakeTimingMessage(Int32 Index)
        {

            string Return;
            StringBuilder Message = new StringBuilder();
            bool IsAlways = false;
            bool IsMeals = false;

            Message.Clear();

            if (DrugList[Index].Breakfast.IsDrug
                && DrugList[Index].Lunch.IsDrug
                && DrugList[Index].Dinner.IsDrug)
            {
                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Always);
                IsAlways = true;
                IsMeals = true;
            }

            if (!IsAlways && DrugList[Index].Breakfast.IsDrug)
            {
                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Breakfast);
                IsMeals = true;
            }

            if (!IsAlways && DrugList[Index].Lunch.IsDrug)
            {
                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Lunch);
                IsMeals = true;
            }

            if (!IsAlways && DrugList[Index].Dinner.IsDrug)
            {
                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Dinner);
                IsMeals = true;
            }

            if (IsMeals)
            {
                switch (DrugList[Index].Breakfast.Kind)
                {

                    case DrugParameter.KindTiming.Before:
                        Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Before);
                        break;

                    case DrugParameter.KindTiming.Between:
                        Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Between);
                        break;

                    case DrugParameter.KindTiming.After:
                        Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_After);
                        break;

                    default:
                        break;

                }
            }

            if (DrugList[Index].Sleep.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Sleep);

            }

            if (DrugList[Index].ToBeTaken.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_ToBeTaken);

            }

            if (DrugList[Index].Appoint.IsDrug)
            {

                string Format = DrugAlarm.Properties.Resources.Parameter_Timing_Appoint.Replace("_", "");

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(DrugList[Index].AppointTime.ToString(Format));

            }

            if (DrugList[Index].HourEach.IsDrug)
            {

                if (Message.Length > 0)
                {
                    Message.Append(",");
                }

                Message.Append(DrugAlarm.Properties.Resources.Parameter_Timing_Each.Replace("_n_", DrugList[Index].HourEachTime.ToString()));

            }

            Return = Message.ToString();

            Message.Clear();
            Message = null;

            return Return;

        }

        /// <summary>
        /// ファイル書込
        /// </summary>
        public void Save()
        {

            //前回パスの取得 : this.Load()でパス作成済みなのでエラー対策はしない
            string Path = Properties.Settings.Default.ParameterFullPath;

            //書込用ファイルパスの取得
            string TmpPath = Path.Replace(".Prm", ".Tmp");

            StringBuilder Str = new StringBuilder();

            //ファイル書込開始
            try
            {
                
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(TmpPath, false, Encoding.Unicode))
                {

                    #region 設定パラメータ
                    file.WriteLine(NAME.SETTING.START);
                    file.WriteLine(MakeParameter(NAME.SETTING.BREAKFAST, Setting.Breakfast.Start, Setting.Breakfast.Finish));
                    file.WriteLine(MakeParameter(NAME.SETTING.LUNCH, Setting.Lunch.Start, Setting.Lunch.Finish));
                    file.WriteLine(MakeParameter(NAME.SETTING.DINNER, Setting.Dinner.Start, Setting.Dinner.Finish));
                    file.WriteLine(MakeParameter(NAME.SETTING.SLEEP, Setting.Sleep));
                    file.WriteLine(MakeParameter(NAME.SETTING.BEFOREMEALS, Setting.MinuteBeforeMeals));
                    file.WriteLine(MakeParameter(NAME.SETTING.BETWEENMEALS, Setting.MinuteBetweenMeals));
                    file.WriteLine(MakeParameter(NAME.SETTING.AFTERMEALS, Setting.MinuteAfterMeals));
                    file.WriteLine(MakeParameter(NAME.SETTING.BEFORESLEEP, Setting.MinuteBeforeSleep));
                    file.WriteLine(MakeParameter(NAME.SETTING.REALARM, Setting.MinuteReAlarm));
                    file.WriteLine(NAME.SETTING.END);
                    #endregion

                    #region 薬パラメータ
                    for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                    {
                        file.WriteLine("");

                        file.WriteLine(NAME.DRUG.START);

                        file.WriteLine(MakeParameter(NAME.DRUG.NAME, DrugList[iLoop].Name, false));

                        //朝昼夕ともに同設定のため、朝のみで判断
                        switch (DrugList[iLoop].Breakfast.Kind)
                        {

                            case DrugParameter.KindTiming.Before:
                                file.WriteLine(NAME.DRUG.BEFOREMEALS);
                                break;

                            case DrugParameter.KindTiming.Between:
                                file.WriteLine(NAME.DRUG.BETWEENMEALS);
                                break;

                            case DrugParameter.KindTiming.After:
                                file.WriteLine(NAME.DRUG.AFTERMEALS);
                                break;

                            default:
                                file.WriteLine(NAME.DRUG.BEFOREMEALS);
                                break;

                        }

                        if (DrugList[iLoop].Breakfast.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.BREAKFAST, DrugList[iLoop].Breakfast.Volume));
                        }

                        if (DrugList[iLoop].Lunch.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.LUNCH, DrugList[iLoop].Lunch.Volume));
                        }

                        if (DrugList[iLoop].Dinner.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.DINNER, DrugList[iLoop].Dinner.Volume));
                        }

                        if (DrugList[iLoop].ToBeTaken.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.TOBETAKEN, DrugList[iLoop].ToBeTaken.Volume));
                        }

                        //指定日時が設定されていても、過去であれば保存しない
                        if (DrugList[iLoop].Appoint.IsDrug && DrugList[iLoop].AppointTime > DateTime.Now)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.APPOINTTIME, DrugList[iLoop].AppointTime, DrugList[iLoop].Appoint.Volume));
                        }
                        else
                        {
                            //過去の場合は服用設定を解除
                            DrugList[iLoop].Appoint.IsDrug = false;
                        }

                        if (DrugList[iLoop].HourEach.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.HOUREACH, DrugList[iLoop].HourEachTime, DrugList[iLoop].HourEach.Volume));
                        }

                        file.WriteLine(MakeParameter(NAME.DRUG.TOTALVOLUME, DrugList[iLoop].TotalVolume));
                        file.WriteLine(MakeParameter(NAME.DRUG.PRESCRIPTIOIN, DrugList[iLoop].PrescriptionAlarmVolume));
                        file.WriteLine(MakeParameter(NAME.DRUG.REMARKS, DrugList[iLoop].Remarks, true));

                        file.WriteLine(NAME.DRUG.END);

                    }
                    #endregion

                }

                System.IO.File.Copy(TmpPath, Path, true);

            }
            catch (Exception ex)
            {

            }
            finally
            {

                Str.Clear();
                Str = null;

                //次回アラーム取得
                SetNextAlarm();

            }

        }

        /// <summary>
        /// パラメータ文字列形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">文字列</param>
        /// <param name="IsAllowLine">改行有無</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, string Value, bool IsAllowLine)
        {

            Method method = new Method();
            StringBuilder Str = new StringBuilder(Parameter.Length + Value.Length + 1);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");

                if (IsAllowLine)
                {
                    Str.Append(method.ConvertCRLF(Value));
                }
                else
                {
                    Str.Append(Value);
                }

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// パラメータ数値形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">数値</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, Int32 Value)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + TimeFormat.Length + 1);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");
                Str.Append(Value.ToString());

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// パラメータ時間形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Value">時間</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, DateTime Value)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + TimeFormat.Length + 1);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");
                Str.Append(Value.ToString(TimeFormat));

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// パラメータ時間範囲形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="Start">開始時間</param>
        /// <param name="Finish">終了時間</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, DateTime Start, DateTime Finish)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + TimeFormat.Length * 2 + 2);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");
                Str.Append(Start.ToString(TimeFormat));
                Str.Append(",");
                Str.Append(Finish.ToString(TimeFormat));

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// パラメータ指定日時形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="AppointTime">指定日時</param>
        /// <param name="Volume">錠</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, DateTime AppointTime, Int32 Volume)
        {

            const string AppointFormat = "yyyy/MM/dd HH:mm";

            StringBuilder Str = new StringBuilder(Parameter.Length + AppointFormat.Length + Volume.ToString().Length + 2);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");
                Str.Append(AppointTime.ToString(AppointFormat));
                Str.Append(",");
                Str.Append(Volume.ToString());

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// パラメータ時間毎形式作成
        /// </summary>
        /// <param name="Parameter">パラメータ名</param>
        /// <param name="HourEach">時間毎</param>
        /// <param name="Volume">錠</param>
        /// <returns>パラメータ出力値</returns>
        private string MakeParameter(string Parameter, Int32 HourEach, Int32 Volume)
        {

            StringBuilder Str = new StringBuilder(Parameter.Length + HourEach.ToString().Length + Volume.ToString().Length + 2);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");
                Str.Append(HourEach.ToString());
                Str.Append(",");
                Str.Append(Volume.ToString());

                return Str.ToString();

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                Str.Clear();
                Str = null;
            }

        }

        /// <summary>
        /// 薬服用時の処理
        /// </summary>
        /// <returns>True:薬切れ有、False:在庫十分</returns>
        public bool TakeMedicine()
        {

            bool Return = false;

            //総量から服用錠を減算
            for (Int32 iLoop = 0; iLoop < NextAlarm.Index.Count; iLoop++)
            {

                Int32 Index = NextAlarm.Index[iLoop];
                Int32 Volume = NextAlarm.Volume[iLoop];

                DrugList[Index].TotalVolume -= Volume;

            }

            //パラメータ更新
            Save();

            //次回アラームの設定
            SetNextAlarm();

            return Return;

        }

        /// <summary>
        /// 次回アラーム情報の設定
        /// </summary>
        private void SetNextAlarm()
        {

            Method method = new Method();
            DateTime Time;

            //初期値
            NextAlarm.Timer = DateTime.MaxValue;
            NextAlarm.Index.Clear();
            NextAlarm.Volume.Clear();

            //再通知
            if (NextAlarm.Timer <= ReAlarm.Timer)  
            {

                //同時刻ならば追加、現在設定値より前時刻なら新規に登録
                if (NextAlarm.Timer != ReAlarm.Timer)
                {
                    NextAlarm.Index.Clear();
                }

                //DrugListのIndexを登録
                for (Int32 iLoop = 0; iLoop < ReAlarm.Index.Count; iLoop++)
                {
                    NextAlarm.Index.Add(ReAlarm.Index[iLoop]);
                    NextAlarm.Volume.Add(ReAlarm.Volume[iLoop]);
                }

                //アラーム時刻の更新
                NextAlarm.Timer = ReAlarm.Timer;

            }

            //毎時・指定日時
            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {

                //毎時
                if (DrugList[iLoop].HourEach.IsDrug)
                {
                    CompareToTime(method.GetNextDateTime(method.GetTodayTime(DrugList[iLoop].HourEachTime, 0)), iLoop, DrugList[iLoop].HourEach.Volume);
                }

                //指定日時
                if (DrugList[iLoop].Appoint.IsDrug)
                {
                    CompareToTime(DrugList[iLoop].AppointTime, iLoop, DrugList[iLoop].Appoint.Volume);
                }

                //朝食
                if (DrugList[iLoop].Breakfast.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Breakfast.Start, Setting.Breakfast.Finish, DrugList[iLoop].Breakfast.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Breakfast.Volume);
                }

                //昼食
                if (DrugList[iLoop].Lunch.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Lunch.Start, Setting.Lunch.Finish, DrugList[iLoop].Lunch.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Lunch.Volume);
                }

                //夕食
                if (DrugList[iLoop].Dinner.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Dinner.Start, Setting.Dinner.Finish, DrugList[iLoop].Dinner.Kind);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Dinner.Volume);
                }

                //就寝前
                if (DrugList[iLoop].Sleep.IsDrug)
                {
                    Time = Setting.Sleep.AddMinutes((-1) * Setting.MinuteBeforeSleep);
                    CompareToTime(Time, iLoop, DrugList[iLoop].Sleep.Volume);
                }

            }

        }

        /// <summary>
        /// 再通知設定
        /// </summary>
        /// <param name="AfterMinute">再通知設定時間(分)</param>
        public void SetRealarm(Int32 AfterMinute)
        {

            ReAlarm.Index.Clear();
            ReAlarm.Volume.Clear();

            //DrugListのIndexを登録
            for (Int32 iLoop = 0; iLoop < NextAlarm.Index.Count; iLoop++)
            {
                ReAlarm.Index.Add(NextAlarm.Index[iLoop]);
                ReAlarm.Volume.Add(NextAlarm.Volume[iLoop]);
            }

            ReAlarm.Timer = DateTime.Now.AddMinutes(AfterMinute);

        }

        /// <summary>
        /// 次回アラームとなるか比較、Indexを登録する
        /// </summary>
        /// <param name="Time">アラーム候補時刻</param>
        /// <param name="DrugIndex">薬Index</param>
        /// <param name="Volume">服用錠</param>
        private void CompareToTime(DateTime Time, Int32 DrugIndex, Int32 Volume)
        {

            //同時刻ならば追加、現在設定値より前時刻なら新規に登録
            if (NextAlarm.Timer != Time)
            {
                NextAlarm.Index.Clear();
            }

            //DrugListのIndexを登録
            if (NextAlarm.Index.IndexOf(DrugIndex) == -1)
            {
                NextAlarm.Index.Add(DrugIndex);
                NextAlarm.Volume.Add(Volume);
            }

            //アラーム時刻の更新
            NextAlarm.Timer = Time;

        }

        /// <summary>
        /// 食事時の服用時間の計算
        /// </summary>
        /// <param name="StartTime">開始時間</param>
        /// <param name="FinishTime">終了時間</param>
        /// <param name="Kind">服用タイミング</param>
        /// <returns>服用時間</returns>
        private DateTime CalcMealsTime(DateTime StartTime, DateTime FinishTime, DrugParameter.KindTiming Kind)
        {

            switch (Kind)
            {

                case DrugParameter.KindTiming.Before:   //食前
                    return StartTime.AddMinutes((-1) * Setting.MinuteBeforeMeals);

                case DrugParameter.KindTiming.Between:  //食間
                    return StartTime.AddMinutes(Setting.MinuteBetweenMeals);

                case DrugParameter.KindTiming.After:    //食後
                    return FinishTime.AddMinutes(Setting.MinuteAfterMeals);

                default:    //ここにはこない
                    return StartTime;

            }

        }

        /// <summary>
        /// デバッグ用:薬の追加
        /// </summary>
        public void DebugTest_AddDrug(string DrugName)
        {

            var AddDrug = new DrugParameter
            {
                Name = DrugName
            };

            DrugList.Insert(DrugList.Count, AddDrug);

        }

    }

}
