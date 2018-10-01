using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

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
        /// 次回アラーム情報
        /// </summary>
        private struct NextAlarmInfo
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
            /// 初期化
            /// </summary>
            public void Initialize()
            {
                Timer = DateTime.MaxValue;
                Index = new List<Int32>();
            }

        }

        /// <summary>
        /// 次回アラーム
        /// </summary>
        private NextAlarmInfo NextAlarm;

        /// <summary>
        /// 再通知
        /// </summary>
        private NextAlarmInfo ReAlarm;

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <param name="Hour">時</param>
        /// <param name="Minute">分</param>
        /// <returns>今日日付のDateTime型</returns>
        private static DateTime GetTodayTime(Int32 Hour, Int32 Minute)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Hour, Minute, 0);
        }

        /// <summary>
        /// 指定時間を今日日付のDateTime型に変換
        /// </summary>
        /// <param name="Time">HH:mm</param>
        /// <returns>今日日付のDateTime型</returns>
        private static DateTime GetTodayTime(string Time)
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
        /// 文字列を整数値に変換
        /// 変換できない場合は引数値を返す
        /// </summary>
        /// <param name="Value">変換文字列</param>
        /// <param name="DefaultValue">デフォルト値</param>
        /// <returns>変換後数値</returns>
        private static Int32 ConvertToInt32(string Value, Int32 DefaultValue)
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
        private static DateTime ConvertDateTime(string Value, DateTime DefaultValue)
        {

            DateTime Return;

            if (Value.Split(':').Length != 2)
            {
                Value += ":00";
            }

            if (!DateTime.TryParse(Value, out Return))
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
        private static string ConvertCRLF(string Str)
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

        /// <summary>
        /// パラメータ名称一覧
        /// </summary>
        private static class NAME
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
                /// 残量アラーム(錠)
                /// </summary>
                public const string PRESCRIPTIOIN = "PrescriptionAlarm";

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
                public bool IsDrug;

                /// <summary>
                /// 服用時間
                /// </summary>
                public KindTiming Kind;

                /// <summary>
                /// 何錠
                /// </summary>
                public Int32 Volume;

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
            public Timing Appoint;

            /// <summary>
            /// 指定時間
            /// </summary>
            public DateTime AppointTime;

            /// <summary>
            /// 時間毎(時)
            /// </summary>
            public Timing HourEach;

            /// <summary>
            /// 時間毎(時)
            /// </summary>
            public Int32 HourEachTime;

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
                Appoint.Initialize(false, KindTiming.Appoint, 0);
                AppointTime = new DateTime(1900, 1, 1, 0, 0, 0);
                HourEach.Initialize(false, KindTiming.TimeEach, 0);
                HourEachTime = 2;
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

            NextAlarm.Initialize();
            ReAlarm.Initialize();

            this.Load();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            Setting = null;

            DrugList.ForEach(Drug =>
                {
                    Drug = null;
                });
            DrugList.Clear();
            DrugList = null;

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        private void Load()
        {

            //前回パスの取得
            string Path = Properties.Settings.Default.ParameterFullPath;

            //前回パスが未記入、またはファイルがなければ新規に作成する
            if (Path.Length == 0 || !System.IO.File.Exists(Path))
            {
                Properties.Settings.Default.ParameterFullPath = SelectDirectory();
            }
            else
            {

                //ファイル読込開始
                using (System.IO.StreamReader file = new System.IO.StreamReader(Path, Encoding.Unicode))
                {

                    StringBuilder NewLine = new StringBuilder();
                    Int32 Index = 0;

                    try
                    {

                        while (!NewLine.Append(file.ReadLine()).Equals(null))
                        {

                            try
                            {

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
                                                Setting.Breakfast.Start = GetTodayTime(Values[0]);
                                                Setting.Breakfast.Finish = GetTodayTime(Values[1]);
                                                break;

                                            case NAME.SETTING.LUNCH:
                                                Setting.Lunch.Start = GetTodayTime(Values[0]);
                                                Setting.Lunch.Finish = GetTodayTime(Values[1]);
                                                break;

                                            case NAME.SETTING.DINNER:
                                                Setting.Dinner.Start = GetTodayTime(Values[0]);
                                                Setting.Dinner.Finish = GetTodayTime(Values[1]);
                                                break;

                                            case NAME.SETTING.SLEEP:
                                                Setting.Sleep = GetTodayTime(Str[1]);
                                                break;

                                            case NAME.SETTING.PRESCRIPTIOIN:
                                                Setting.PrescriptionAlarmVolume = ConvertToInt32(Str[1], Setting.PrescriptionAlarmVolume);
                                                break;

                                            case NAME.SETTING.BEFOREMEALS:
                                                Setting.MinuteBeforeMeals = ConvertToInt32(Str[1], Setting.MinuteBeforeMeals);
                                                break;

                                            case NAME.SETTING.BETWEENMEALS:
                                                Setting.MinuteBetweenMeals = ConvertToInt32(Str[1], Setting.MinuteBetweenMeals);
                                                break;

                                            case NAME.SETTING.AFTERMEALS:
                                                Setting.MinuteAfterMeals = ConvertToInt32(Str[1], Setting.MinuteAfterMeals);
                                                break;

                                            case NAME.SETTING.BEFORESLEEP:
                                                Setting.MinuteBeforeSleep = ConvertToInt32(Str[1], Setting.MinuteBeforeSleep);
                                                break;

                                            case NAME.SETTING.REALARM:
                                                Setting.MinuteReAlarm = ConvertToInt32(Str[1], Setting.MinuteReAlarm);
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
                                                DrugList[Index].Breakfast.Volume = ConvertToInt32(Str[1], DrugList[Index].Breakfast.Volume);
                                                break;

                                            case NAME.DRUG.LUNCH:
                                                DrugList[Index].Lunch.IsDrug = true;
                                                DrugList[Index].Lunch.Volume = ConvertToInt32(Str[1], DrugList[Index].Lunch.Volume);
                                                break;

                                            case NAME.DRUG.DINNER:
                                                DrugList[Index].Dinner.IsDrug = true;
                                                DrugList[Index].Dinner.Volume = ConvertToInt32(Str[1], DrugList[Index].Dinner.Volume);
                                                break;

                                            case NAME.DRUG.TOBETAKEN:
                                                DrugList[Index].ToBeTaken.IsDrug = true;
                                                DrugList[Index].ToBeTaken.Volume = ConvertToInt32(Str[1], DrugList[Index].ToBeTaken.Volume);
                                                break;

                                            case NAME.DRUG.APPOINTTIME:
                                                DrugList[Index].Appoint.IsDrug = true;
                                                DrugList[Index].Appoint.Volume = ConvertToInt32(Str[1], DrugList[Index].Appoint.Volume);
                                                DrugList[Index].AppointTime = ConvertDateTime(Str[2], DrugList[Index].AppointTime);
                                                break;

                                            case NAME.DRUG.HOUREACH:
                                                DrugList[Index].HourEach.IsDrug = true;
                                                DrugList[Index].HourEach.Volume = ConvertToInt32(Str[1], DrugList[Index].HourEach.Volume);
                                                DrugList[Index].HourEachTime = ConvertToInt32(Str[2], DrugList[Index].HourEachTime);
                                                break;

                                            case NAME.DRUG.TOTALVOLUME:
                                                DrugList[Index].TotalVolume = ConvertToInt32(Str[1], DrugList[Index].TotalVolume);
                                                break;

                                            case NAME.DRUG.REMARKS:
                                                DrugList[Index].Name = ConvertCRLF(Str[1]);
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

                    }
                    catch (Exception Ex)
                    {

                    }
                    finally
                    {
                        //メモリ解放
                        NewLine.Clear();
                        NewLine = null;
                    }

                }

                //次回アラーム取得
                SetNextAlarm();

            }

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
                Dialog.RootFolder = Environment.SpecialFolder.MyDocuments;
                Dialog.ShowNewFolderButton = true;

                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    //選択されたフォルダ内にパラメータファイルを保存
                    Ret = System.IO.Path.Combine(Dialog.SelectedPath, Properties.Settings.Default.ParameterFile);

                }

            }

            return Ret;

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
                    file.WriteLine(MakeParameter(NAME.SETTING.PRESCRIPTIOIN, Setting.PrescriptionAlarmVolume));
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

                        if (DrugList[iLoop].Appoint.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.APPOINTTIME, DrugList[iLoop].AppointTime, DrugList[iLoop].Appoint.Volume));
                        }

                        if (DrugList[iLoop].HourEach.IsDrug)
                        {
                            file.WriteLine(MakeParameter(NAME.DRUG.HOUREACH, DrugList[iLoop].HourEachTime, DrugList[iLoop].HourEach.Volume));
                        }

                        file.WriteLine(MakeParameter(NAME.DRUG.TOTALVOLUME, DrugList[iLoop].TotalVolume));
                        file.WriteLine(MakeParameter(NAME.DRUG.REMARKS, DrugList[iLoop].Remarks, true));

                        file.WriteLine(NAME.DRUG.END);

                    }
                    #endregion

                }

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

            StringBuilder Str = new StringBuilder(Parameter.Length + Value.Length + 1);

            try
            {

                Str.Clear();
                Str.Append(Parameter);
                Str.Append("=");

                if (IsAllowLine)
                {
                    Str.Append(ConvertCRLF(Value));
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
        /// 次回アラーム情報の設定
        /// </summary>
        private void SetNextAlarm()
        {

            DateTime Time;

            //初期値
            NextAlarm.Timer = DateTime.MaxValue;
            NextAlarm.Index.Clear();

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
                    CompareToTime(GetNextDateTime(GetTodayTime(DrugList[iLoop].HourEachTime, 0)), iLoop);
                }

                //指定日時
                if (DrugList[iLoop].Appoint.IsDrug)
                {
                    CompareToTime(DrugList[iLoop].AppointTime, iLoop);
                }

                //朝食
                if (DrugList[iLoop].Breakfast.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Breakfast.Start, Setting.Breakfast.Finish, DrugList[iLoop].Breakfast.Kind);
                    CompareToTime(Time, iLoop);
                }

                //昼食
                if (DrugList[iLoop].Lunch.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Lunch.Start, Setting.Lunch.Finish, DrugList[iLoop].Lunch.Kind);
                    CompareToTime(Time, iLoop);
                }

                //夕食
                if (DrugList[iLoop].Dinner.IsDrug)
                {
                    Time = CalcMealsTime(Setting.Dinner.Start, Setting.Dinner.Finish, DrugList[iLoop].Dinner.Kind);
                    CompareToTime(Time, iLoop);
                }

                //就寝前
                if (DrugList[iLoop].Sleep.IsDrug)
                {
                    Time = Setting.Sleep.AddMinutes((-1) * Setting.MinuteBeforeSleep);
                    CompareToTime(Time, iLoop);
                }

            }

        }

        /// <summary>
        /// 次回アラームとなるか比較、Indexを登録する
        /// </summary>
        /// <param name="Timer">アラーム候補時刻</param>
        /// <param name="DrugIndex">薬Index</param>
        private void CompareToTime(DateTime Time, Int32 DrugIndex)
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

    }

}
