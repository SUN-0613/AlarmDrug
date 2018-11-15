using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Setting.Model
    /// </summary>
    public class Setting : IDisposable
    {

        #region パラメータ

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited;

        /// <summary>
        /// 設定パラメータ
        /// </summary>
        public Class.Parameter.SettingParameter SetParam;

        /// <summary>
        /// パラメータ
        /// </summary>
        public Class.Parameter _Parameter;

        /// <summary>
        /// 食事時間の取得
        /// </summary>
        /// <returns>The meet time.</returns>
        private DateTime GetMeetTime(List<Int32> hour, List<Int32> minute, Int32 hourIndex, Int32 minuteIndex, DateTime defaultValue)
        {

            if ((hour != null && minute != null)
                && (-1 < hourIndex && hourIndex < hour.Count)
                && (-1 < minuteIndex && minuteIndex < minute.Count))
            {

                Int32 Hour = hour[hourIndex];
                Int32 Minute = minute[minuteIndex];

                return new Class.Method().GetTodayTime(Hour, Minute);

            }
            else
            {
                return defaultValue;
            }

        }

        #endregion

        #region コマンド

        /// <summary>
        /// キャンセルコマンド
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _Parameter.Load();
        }

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {

            //開始・終了時間の整合性チェック
            SetParam.Breakfast.CompareTimes();
            SetParam.Lunch.CompareTimes();
            SetParam.Dinner.CompareTimes();

            _Parameter.Setting = SetParam;
            _Parameter.Save();
            _Parameter.Load();

        }

        #endregion

        #region 朝食

        /// <summary>
        /// 時Index
        /// 朝食
        /// 開始
        /// </summary>
        private Int32 _BreakfastStartHourIndex = -1;

        /// <summary>
        /// 時Index
        /// 朝食
        /// 終了
        /// </summary>
        private Int32 _BreakfastFinishHourIndex = -1;

        /// <summary>
        /// 分Index
        /// 朝食
        /// 開始
        /// </summary>
        private Int32 _BreakfastStartMinuteIndex = -1;

        /// <summary>
        /// 分Index
        /// 朝食
        /// 終了
        /// </summary>
        private Int32 _BreakfastFinishMinuteIndex = -1;

        /// <summary>
        /// 時間の設定
        /// 朝食
        /// 開始
        /// </summary>
        private void SetBreakfastStart()
        {
            SetParam.Breakfast.Start = GetMeetTime(_Hour, _Minute, _BreakfastStartHourIndex, _BreakfastStartMinuteIndex, SetParam.Breakfast.Start);
        }

        /// <summary>
        /// 時間の設定
        /// 朝食
        /// 終了
        /// </summary>
        private void SetBreakfastFinish()
        {
            SetParam.Breakfast.Finish = GetMeetTime(_Hour, _Minute, _BreakfastFinishHourIndex, _BreakfastFinishMinuteIndex, SetParam.Breakfast.Finish);
        }

        /// <summary>
        /// 時Indexプロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The index of the breakfast start hour.</value>
        public Int32 BreakfastStartHourIndex
        {
            get { return _BreakfastStartHourIndex; }
            set 
            {
                if (!_BreakfastStartHourIndex.Equals(value))
                {
                    _BreakfastStartHourIndex = value;
                    SetBreakfastStart();
                }
            }
        }

        /// <summary>
        /// 時Indexプロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The index of the breakfast finish hour.</value>
        public Int32 BreakfastFinishHourIndex
        {
            get { return _BreakfastFinishHourIndex; }
            set
            {
                if (!_BreakfastFinishHourIndex.Equals(value))
                {
                    _BreakfastFinishHourIndex = value;
                    SetBreakfastFinish();
                }
            }
        }

        /// <summary>
        /// 分Indexプロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The index of the breakfast start minute.</value>
        public Int32 BreakfastStartMinuteIndex
        {
            get { return _BreakfastStartMinuteIndex; }
            set
            {
                if (!_BreakfastStartMinuteIndex.Equals(value))
                {
                    _BreakfastStartMinuteIndex = value;
                    SetBreakfastStart();
                }
            }
        }

        /// <summary>
        /// 分Indexプロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The index of the breakfast finish minute.</value>
        public Int32 BreakfastFinishMinuteIndex
        {
            get { return _BreakfastFinishMinuteIndex; }
            set
            {
                if (!_BreakfastFinishMinuteIndex.Equals(value))
                {
                    _BreakfastFinishMinuteIndex = value;
                    SetBreakfastFinish();
                }
            }
        }

        #endregion

        #region 昼食
         /// <summary>         /// 時Index         /// 昼食         /// 開始         /// </summary>         private Int32 _LunchStartHourIndex = -1;          /// <summary>         /// 時Index         /// 昼食         /// 終了         /// </summary>         private Int32 _LunchFinishHourIndex = -1;          /// <summary>         /// 分Index         /// 昼食         /// 開始         /// </summary>         private Int32 _LunchStartMinuteIndex = -1;          /// <summary>         /// 分Index         /// 昼食         /// 終了         /// </summary>         private Int32 _LunchFinishMinuteIndex = -1;          /// <summary>         /// 時間の設定         /// 昼食         /// 開始         /// </summary>         private void SetLunchStart()         {             SetParam.Lunch.Start = GetMeetTime(_Hour, _Minute, _LunchStartHourIndex, _LunchStartMinuteIndex, SetParam.Lunch.Start);         }          /// <summary>         /// 時間の設定         /// 昼食         /// 終了         /// </summary>         private void SetLunchFinish()         {             SetParam.Lunch.Finish = GetMeetTime(_Hour, _Minute, _LunchFinishHourIndex, _LunchFinishMinuteIndex, SetParam.Lunch.Finish);         }          /// <summary>         /// 時Indexプロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The index of the Lunch start hour.</value>         public Int32 LunchStartHourIndex         {             get { return _LunchStartHourIndex; }             set              {                 if (!_LunchStartHourIndex.Equals(value))                 {                     _LunchStartHourIndex = value;                     SetLunchStart();                 }             }         }          /// <summary>         /// 時Indexプロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The index of the Lunch finish hour.</value>         public Int32 LunchFinishHourIndex         {             get { return _LunchFinishHourIndex; }             set             {                 if (!_LunchFinishHourIndex.Equals(value))                 {                     _LunchFinishHourIndex = value;                     SetLunchFinish();                 }             }         }          /// <summary>         /// 分Indexプロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The index of the Lunch start minute.</value>         public Int32 LunchStartMinuteIndex         {             get { return _LunchStartMinuteIndex; }             set             {                 if (!_LunchStartMinuteIndex.Equals(value))                 {                     _LunchStartMinuteIndex = value;                     SetLunchStart();                 }             }         }          /// <summary>         /// 分Indexプロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The index of the Lunch finish minute.</value>         public Int32 LunchFinishMinuteIndex         {             get { return _LunchFinishMinuteIndex; }             set             {                 if (!_LunchFinishMinuteIndex.Equals(value))                 {                     _LunchFinishMinuteIndex = value;                     SetLunchFinish();                 }             }         } 
        #endregion

        #region 夕食
         /// <summary>         /// 時Index         /// 夕食         /// 開始         /// </summary>         private Int32 _DinnerStartHourIndex = -1;          /// <summary>         /// 時Index         /// 夕食         /// 終了         /// </summary>         private Int32 _DinnerFinishHourIndex = -1;          /// <summary>         /// 分Index         /// 夕食         /// 開始         /// </summary>         private Int32 _DinnerStartMinuteIndex = -1;          /// <summary>         /// 分Index         /// 夕食         /// 終了         /// </summary>         private Int32 _DinnerFinishMinuteIndex = -1;          /// <summary>         /// 時間の設定         /// 夕食         /// 開始         /// </summary>         private void SetDinnerStart()         {             SetParam.Dinner.Start = GetMeetTime(_Hour, _Minute, _DinnerStartHourIndex, _DinnerStartMinuteIndex, SetParam.Dinner.Start);         }          /// <summary>         /// 時間の設定         /// 夕食         /// 終了         /// </summary>         private void SetDinnerFinish()         {             SetParam.Dinner.Finish = GetMeetTime(_Hour, _Minute, _DinnerFinishHourIndex, _DinnerFinishMinuteIndex, SetParam.Dinner.Finish);         }          /// <summary>         /// 時Indexプロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The index of the Dinner start hour.</value>         public Int32 DinnerStartHourIndex         {             get { return _DinnerStartHourIndex; }             set              {                 if (!_DinnerStartHourIndex.Equals(value))                 {                     _DinnerStartHourIndex = value;                     SetDinnerStart();                 }             }         }          /// <summary>         /// 時Indexプロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The index of the Dinner finish hour.</value>         public Int32 DinnerFinishHourIndex         {             get { return _DinnerFinishHourIndex; }             set             {                 if (!_DinnerFinishHourIndex.Equals(value))                 {                     _DinnerFinishHourIndex = value;                     SetDinnerFinish();                 }             }         }          /// <summary>         /// 分Indexプロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The index of the Dinner start minute.</value>         public Int32 DinnerStartMinuteIndex         {             get { return _DinnerStartMinuteIndex; }             set             {                 if (!_DinnerStartMinuteIndex.Equals(value))                 {                     _DinnerStartMinuteIndex = value;                     SetDinnerStart();                 }             }         }          /// <summary>         /// 分Indexプロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The index of the Dinner finish minute.</value>         public Int32 DinnerFinishMinuteIndex         {             get { return _DinnerFinishMinuteIndex; }             set             {                 if (!_DinnerFinishMinuteIndex.Equals(value))                 {                     _DinnerFinishMinuteIndex = value;                     SetDinnerFinish();                 }             }         } 
        #endregion

        #region 就寝前
         /// <summary>         /// 時Index         /// 就寝前         /// </summary>         private Int32 _SleepHourIndex = -1;          /// <summary>         /// 分Index         /// 就寝前         /// </summary>         private Int32 _SleepMinuteIndex = -1;          /// <summary>         /// 時間の設定         /// 就寝前         /// </summary>         private void SetSleep()         {             SetParam.Sleep = GetMeetTime(_Hour, _Minute, _SleepHourIndex, _SleepMinuteIndex, SetParam.Sleep);         }          /// <summary>         /// 時Indexプロパティ         /// 就寝前         /// </summary>         /// <value>The index of the Sleep  hour.</value>         public Int32 SleepHourIndex         {             get { return _SleepHourIndex; }             set              {                 if (!_SleepHourIndex.Equals(value))                 {                     _SleepHourIndex = value;                     SetSleep();                 }             }         }          /// <summary>         /// 分Indexプロパティ         /// 就寝前         /// </summary>         /// <value>The index of the Sleep  minute.</value>         public Int32 SleepMinuteIndex         {             get { return _SleepMinuteIndex; }             set             {                 if (!_SleepMinuteIndex.Equals(value))                 {                     _SleepMinuteIndex = value;                     SetSleep();                 }             }         } 
        #endregion

        #region アラーム設定

        /// <summary>
        /// 分Index
        /// 食前
        /// </summary>
        private Int32 _BeforeMealMinuteIndex = -1;

        /// <summary>
        /// 分Index
        /// 食後
        /// </summary>
        private Int32 _AfterMealMinuteIndex = -1;

        /// <summary>
        /// 分Index
        /// 就寝前
        /// </summary>
        private Int32 _BeforeSleepMinuteIndex = -1;

        /// <summary>
        /// 分Index
        /// 再通知
        /// </summary>
        private Int32 _RealarmMinuteIndex = -1;

        /// <summary>
        /// 分Indexの取得
        /// アラーム
        /// 食前
        /// </summary>
        /// <returns>The before meal minute index.</returns>
        public Int32 GetBeforeMealMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteBeforeMeals);
        }

        /// <summary>
        /// 分Indexの取得
        /// アラーム
        /// 食後
        /// </summary>
        /// <returns>The after meal minute index.</returns>
        public Int32 GetAfterMealMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteAfterMeals);
        }

        /// <summary>
        /// 分Indexの取得
        /// アラーム
        /// 就寝前
        /// </summary>
        /// <returns>The before sleep minute index.</returns>
        public Int32 GetBeforeSleepMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteBeforeSleep);
        }

        /// <summary>
        /// 分Indexの取得
        /// アラーム
        /// 再通知
        /// </summary>
        /// <returns>The realarm minute index.</returns>
        public Int32 GetRealarmMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteRealarm);
        }

        /// <summary>
        /// Listより指定Indexの値を取得
        /// </summary>
        /// <returns>The minute.</returns>
        /// <param name="list">List.</param>
        /// <param name="index">Index.</param>
        /// <param name="defaultValue">Default value.</param>
        private Int32 GetMinute(List<Int32> list, Int32 index, Int32 defaultValue)
        {
            if ((list != null)
                && (-1 < index && index < list.Count))
            {
                return list[index];
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// アラーム時間の取得
        /// 食前
        /// </summary>
        private void SetBeforeMeal()
        {
            SetParam.MinuteBeforeMeals = GetMinute(_AlarmMinute, _BeforeMealMinuteIndex, SetParam.MinuteBeforeMeals);
        }

        /// <summary>
        /// アラーム時間の取得
        /// 食後
        /// </summary>
        private void SetAfterMeal()
        {
            SetParam.MinuteAfterMeals = GetMinute(_AlarmMinute, _AfterMealMinuteIndex, SetParam.MinuteAfterMeals);
        }

        /// <summary>
        /// アラーム時間の取得
        /// 就寝前
        /// </summary>
        private void SetBeforeSleep()
        {
            SetParam.MinuteBeforeSleep = GetMinute(_AlarmMinute, _BeforeSleepMinuteIndex, SetParam.MinuteBeforeSleep);
        }

        /// <summary>
        /// アラーム時間の取得
        /// 再通知
        /// </summary>
        private void SetRealarm()
        {
            SetParam.MinuteRealarm = GetMinute(_AlarmMinute, _RealarmMinuteIndex, SetParam.MinuteRealarm);
        }

        /// <summary>
        /// 分Indexプロパティ
        /// アラーム
        /// 食前
        /// </summary>
        /// <value>The index of the before meal minute.</value>
        public Int32 BeforeMealMinuteIndex
        {
            get { return _BeforeMealMinuteIndex; }
            set
            {
                if (!_BeforeMealMinuteIndex.Equals(value))
                {
                    _BeforeMealMinuteIndex = value;
                    SetBeforeMeal();
                }
            }
        }

        /// <summary>
        /// 分Indexプロパティ
        /// アラーム
        /// 食後
        /// </summary>
        /// <value>The index of the after meal minute.</value>
        public Int32 AfterMealMinuteIndex
        {
            get { return _AfterMealMinuteIndex; }
            set
            {
                if (!_AfterMealMinuteIndex.Equals(value))
                {
                    _AfterMealMinuteIndex = value;
                    SetAfterMeal();
                }
            }
        }

        /// <summary>
        /// 分Indexプロパティ
        /// アラーム
        /// 就寝前
        /// </summary>
        /// <value>The index of the before sleep minute.</value>
        public Int32 BeforeSleepMinuteIndex
        {
            get { return _BeforeSleepMinuteIndex; }
            set
            {
                if (!_BeforeSleepMinuteIndex.Equals(value))
                {
                    _BeforeSleepMinuteIndex = value;
                    SetBeforeSleep();
                }
            }
        }

        /// <summary>
        /// 分Indexプロパティ
        /// アラーム
        /// 再通知
        /// </summary>
        /// <value>The index of the realarm minute.</value>
        public Int32 RealarmMinuteIndex
        {
            get { return _RealarmMinuteIndex; }
            set
            {
                if (!_RealarmMinuteIndex.Equals(value))
                {
                    _RealarmMinuteIndex = value;
                    SetRealarm();
                }
            }
        }

        #endregion

        #region List

        /// <summary>
        /// 時List
        /// </summary>
        private List<Int32> _Hour;

        /// <summary>
        /// 分List
        /// </summary>
        private List<Int32> _Minute;

        /// <summary>
        /// アラーム用分List
        /// </summary>
        private List<Int32> _AlarmMinute;

        /// <summary>
        /// 時Listの作成
        /// </summary>
        /// <returns>The hour list.</returns>
        private List<Int32> GetHourList()
        {

            if (_Hour == null)
            {

                _Hour = new List<Int32>();
                _Hour.Clear();

                for (Int32 iLoop = 0; iLoop < 24; iLoop++)
                {
                    _Hour.Add(iLoop);
                }

            }

            return _Hour;

        }

        /// <summary>
        /// 分Listの作成
        /// </summary>
        /// <returns>The minute list.</returns>
        private List<Int32> GetMinuteList()
        {
            if (_Minute == null)
            {

                _Minute = new List<Int32>();
                _Minute.Clear();

                for (Int32 iLoop = 0; iLoop < 60; iLoop += 5)
                {
                    _Minute.Add(iLoop);
                }

            }

            return _Minute;

        }

        /// <summary>
        /// アラーム用分Listの作成
        /// </summary>
        /// <returns>The alarm minute list.</returns>
        private List<Int32> GetAlarmMinuteList()
        {

            if (_AlarmMinute == null)
            {

                _AlarmMinute = new List<Int32>();
                _AlarmMinute.Clear();

                for (Int32 iLoop = 0; iLoop <= 60; iLoop += 5)
                {
                    _AlarmMinute.Add(iLoop);
                }

            }

            return _AlarmMinute;

        }

        /// <summary>
        /// 時Indexの取得
        /// </summary>
        /// <returns>The hour index.</returns>
        /// <param name="Hour">Hour.</param>
        private Int32 GetHourIndex(Int32 Hour)
        {
            return new Class.Method().GetDefaultIndex(_Hour, Hour);
        }

        /// <summary>
        /// 分Indexの取得
        /// </summary>
        /// <returns>The minute index.</returns>
        /// <param name="Minute">Minute.</param>
        private Int32 GetMinuteIndex(Int32 Minute)
        {
            return GetMinuteDefaultIndex(_Minute, Minute);
        }

        /// <summary>
        /// アラーム用分Indexの取得
        /// </summary>
        /// <returns>The alarm minute index.</returns>
        /// <param name="Minute">Minute.</param>
        private Int32 GetAlarmMinuteIndex(Int32 Minute)
        {
            return GetMinuteDefaultIndex(_AlarmMinute, Minute);
        }

        /// <summary>
        /// 分Listから該当Indexの取得
        /// </summary>
        /// <returns>The minute default index.</returns>
        /// <param name="list">List.</param>
        /// <param name="value">Value.</param>
        private Int32 GetMinuteDefaultIndex(List<Int32> list, Int32 value)
        {

            Int32 Return = -1;

            for (Int32 iLoop = 0; iLoop < list.Count; iLoop++)
            {
                if (value <= list[iLoop])
                {
                    Return = iLoop;
                    break;
                }
            }

            if (Return.Equals(-1))
            {
                Return = 0;
            }

            return Return;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;

            SetParam = new Class.Parameter.SettingParameter()
            {
                Breakfast = _Parameter.Setting.Breakfast,
                Lunch = _Parameter.Setting.Lunch,
                Dinner = _Parameter.Setting.Dinner,
                Sleep = _Parameter.Setting.Sleep,
                MinuteBeforeMeals = _Parameter.Setting.MinuteBeforeMeals,
                MinuteAfterMeals = _Parameter.Setting.MinuteAfterMeals,
                MinuteBeforeSleep = _Parameter.Setting.MinuteBeforeSleep,
                MinuteRealarm = _Parameter.Setting.MinuteRealarm
            };

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.Setting"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.Setting"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.Setting"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.Setting"/> was occupying.</remarks>
        public void Dispose()
        {

            if (_Hour != null)
            {
                _Hour.Clear();
                _Hour = null;
            }

            if (_Minute != null)
            {
                _Minute.Clear();
                _Minute = null;
            }

        }

    }
}
