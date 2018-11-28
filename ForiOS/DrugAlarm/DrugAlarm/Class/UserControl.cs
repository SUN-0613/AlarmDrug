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
        public const string FullPath = "ParameterFullPath";

        /// <summary>
        /// パラメータファイル名
        /// パスなし
        /// </summary>
        public const string FileName = "DrugAlarm.Prm";

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
                /// new
                /// </summary>
                public Drug()
                {
                    Index = -1;
                    Volume = 0;
                    IsAppoint = false;
                    IsHourEach = false;
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
