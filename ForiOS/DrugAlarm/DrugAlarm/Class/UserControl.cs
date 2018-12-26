using System;
using System.Collections.Generic;

namespace DrugAlarm.Class
{

    /// <summary>
    /// 独自型
    /// </summary>
    public class UserControl
    {

        /// <summary>
        /// 次回アラーム再設定
        /// </summary>
        public static bool ResetNextAlarm = false;

        /// <summary>
        /// アラーム時刻よりも先に服用する
        /// </summary>
        public static bool TakeBeforeAlarm = false;

        /// <summary>
        /// Realarm.xamlの処理後、Alarm.xamlの処理を続ける
        /// </summary>
        public static bool IsWaitToRunRealarm = false;

        /// <summary>
        /// 秒付きの時間フォーマット
        /// </summary>
        public const string TimeSecFormat = "HH:mm:ss";

        /// <summary>
        /// パラメータ時間フォーマット
        /// </summary>
        public const string TimeFormat = "HH:mm";

        /// <summary>
        /// パラメータ保存用指定日時フォーマット
        /// </summary>
        public const string DateTimeFormat = "yyyy/MM/dd HH:mm";

        /// <summary>
        /// ユーザデータ保存場所
        /// </summary>
        public const string ParameterFullPath = "ParameterFullPath";

        /// <summary>
        /// パラメータファイル名
        /// パスなし
        /// </summary>
        public const string ParameterFileName = "DrugAlarm.Prm";

        /// <summary>
        /// アラーム履歴保存場所
        /// </summary>
        public const string AlarmFullPath = "AlarmFullPath";

        /// <summary>
        /// アラーム履歴保持ファイル名 パスなし
        /// </summary>
        public const string AlarmFileName = "NextAlarm.Prm";

        /// <summary>
        /// パラメータファイル拡張子
        /// </summary>
        public const string FileExtension = ".Prm";

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
        public class AlarmInfo : IDisposable
        {

            /// <summary>
            /// 薬クラス
            /// </summary>
            public class Drug
            {

                /// <summary>
                /// Parameter.DrugList[Index]
                /// </summary>
                public Int32 Index;

                /// <summary>
                /// 服用錠
                /// </summary>
                public Int32 Volume;

                /// <summary>
                /// 指定時間使用FLG
                /// </summary>
                public bool IsAppoint;

                /// <summary>
                /// 時間毎使用FLG
                /// </summary>
                public bool IsHourEach;

                /// <summary>
                /// 服用FLG
                /// </summary>
                public bool IsDrug;

                /// <summary>
                /// new
                /// </summary>
                public Drug()
                {
                    Index = -1;
                    Volume = 0;
                    IsAppoint = false;
                    IsHourEach = false;
                    IsDrug = true;
                }

            }

            /// <summary>
            /// アラーム時間
            /// </summary>
            public DateTime Timer;

            /// <summary>
            /// 薬List
            /// </summary>
            public List<Drug> DrugList;

            /// <summary>
            /// new
            /// </summary>
            public AlarmInfo()
            {
                Timer = DateTime.MaxValue;
                DrugList = new List<Drug>();
            }

            /// <summary>
            /// 終了処理
            /// </summary>
            /// <remarks>Call <see cref="Dispose"/> when you are finished using the
            /// <see cref="T:DrugAlarm.Class.Parameter.AlarmInfo"/>. The <see cref="Dispose"/> method leaves the
            /// <see cref="T:DrugAlarm.Class.Parameter.AlarmInfo"/> in an unusable state. After calling
            /// <see cref="Dispose"/>, you must release all references to the
            /// <see cref="T:DrugAlarm.Class.Parameter.AlarmInfo"/> so the garbage collector can reclaim the memory that
            /// the <see cref="T:DrugAlarm.Class.Parameter.AlarmInfo"/> was occupying.</remarks>
            public void Dispose()
            {
                DrugList.Clear();
                DrugList = null;
            }

        }

        /// <summary>
        /// 服用情報
        /// </summary>
        public class MedicineInfo : Common.ViewModelBase
        {

            /// <summary>
            /// 薬品名
            /// </summary>
            private string _Name;

            /// <summary>
            /// 服用量
            /// </summary>
            private Int32 _Volume;

            /// <summary>
            /// Parameter.NextAlarm[Index]
            /// </summary>
            private Int32 _Index;

            /// <summary>
            /// 服用するか
            /// </summary>
            private bool _IsDrug;

            /// <summary>
            /// 薬品名プロパティ
            /// </summary>
            public string Name 
            { 
                get { return _Name; }
                private set
                {
                    if (!_Name.Equals(value))
                    {
                        _Name = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 服用量プロパティ
            /// </summary>
            public Int32 Volume 
            { 
                get { return _Volume; }
                private set
                {
                    if (!_Volume.Equals(value))
                    {
                        _Volume = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// Parameter.NextAlarm[Index]プロパティ
            /// </summary>
            public Int32 Index
            {
                get { return _Index; }
                set
                {
                    if (!_Index.Equals(value))
                    {
                        _Index = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 服用するかプロパティ
            /// </summary>
            public bool IsDrug
            {
                get { return _IsDrug; }
                set
                {
                    if (!_IsDrug.Equals(value))
                    {
                        _IsDrug = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// new
            /// </summary>
            /// <param name="name">Name.</param>
            /// <param name="volume">Volume.</param>
            /// <param name="index">Parameter.NextAlarm[Index]</param>
            public MedicineInfo(string name, Int32 volume, Int32 index)
            {
                _Name = name;
                _Volume = volume;
                _Index = index;
                _IsDrug = true;
            }

        }

        /// <summary>
        /// 服用時間
        /// </summary>
        /// <value>The kind timing.</value>
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
            /// 服用するか
            /// </summary>
            public bool IsDrug;

            /// <summary>
            /// 服用時間
            /// </summary>
            public KindTiming Kind;

            /// <summary>
            /// 服用錠数
            /// </summary>
            public Int32 Volume;

            /// <summary>
            /// new
            /// </summary>
            public Timing()
            {
                IsDrug = false;
                Kind = KindTiming.None;
                Volume = 0;
            }

        }

    }
}
