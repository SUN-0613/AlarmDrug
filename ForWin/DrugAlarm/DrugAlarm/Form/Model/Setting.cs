using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Setting.Model
    /// </summary>
    class Setting : IDisposable
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
        private Class.Parameter _Parameter;

        #endregion

        #region コマンド

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 初期化
        /// 編集内容を破棄
        /// </summary>
        public void Initialize()
        {
            _Parameter.Load();
        }

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
        /// 朝食：開始：時Indexプロパティ
        /// </summary>
        public Int32 BreakfastStartHourIndex
        {
            get { return _BreakfastStartHourIndex; }
            set
            {
                _BreakfastStartHourIndex = value;
                SetBreakfastStart();
            }
        }

        /// <summary>
        /// 朝食：開始：分Indexプロパティ
        /// </summary>
        public Int32 BreakfastStartMinuteIndex
        {
            get { return _BreakfastStartMinuteIndex; }
            set
            {
                _BreakfastStartMinuteIndex = value;
                SetBreakfastStart();
            }
        }

        /// <summary>
        /// 朝食：開始：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBreakfastStartHourIndex()
        {
            return GetHourIndex(SetParam.Breakfast.Start.Hour);
        }

        /// <summary>
        /// 朝食：開始：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBreakfastStartMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Breakfast.Start.Minute);
        }

        /// <summary>
        /// 朝食：終了：時Indexプロパティ
        /// </summary>
        public Int32 BreakfastFinishHourIndex
        {
            get { return _BreakfastFinishHourIndex; }
            set
            {
                _BreakfastFinishHourIndex = value;
                SetBreakfastFinish();
            }
        }

        /// <summary>
        /// 朝食：終了：分Indexプロパティ
        /// </summary>
        public Int32 BreakfastFinishMinuteIndex
        {
            get { return _BreakfastFinishMinuteIndex; }
            set
            {
                _BreakfastFinishMinuteIndex = value;
                SetBreakfastFinish();
            }
        }

        /// <summary>
        /// 朝食：終了：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBreakfastFinishHourIndex()
        {
            return GetHourIndex(SetParam.Breakfast.Finish.Hour);
        }

        /// <summary>
        /// 朝食：終了：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBreakfastFinishMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Breakfast.Finish.Minute);
        }

        /// <summary>
        /// 朝食：開始：時Index
        /// </summary>
        private Int32 _BreakfastStartHourIndex = -1;

        /// <summary>
        /// 朝食：開始：分Index
        /// </summary>
        private Int32 _BreakfastStartMinuteIndex = -1;

        /// <summary>
        /// 朝食：終了：時Index
        /// </summary>
        private Int32 _BreakfastFinishHourIndex = -1;

        /// <summary>
        /// 朝食：終了：分Index
        /// </summary>
        private Int32 _BreakfastFinishMinuteIndex = -1;

        /// <summary>
        /// 朝食開始時間の設定
        /// </summary>
        public void SetBreakfastStart()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _BreakfastStartHourIndex && _BreakfastStartHourIndex < _Hour.Count)
                && (-1 < _BreakfastStartMinuteIndex && _BreakfastStartMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_BreakfastStartHourIndex];
                Int32 Minute = _Minute[_BreakfastStartMinuteIndex];

                SetParam.Breakfast.Start = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        /// <summary>
        /// 朝食終了時間の設定
        /// </summary>
        public void SetBreakfastFinish()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _BreakfastFinishHourIndex && _BreakfastFinishHourIndex < _Hour.Count)
                && (-1 < _BreakfastFinishMinuteIndex && _BreakfastFinishMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_BreakfastFinishHourIndex];
                Int32 Minute = _Minute[_BreakfastFinishMinuteIndex];

                SetParam.Breakfast.Finish = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        #endregion

        #region 昼食

        /// <summary>
        /// 昼食：開始：時Indexプロパティ
        /// </summary>
        public Int32 LunchStartHourIndex
        {
            get { return _LunchStartHourIndex; }
            set
            {
                _LunchStartHourIndex = value;
                SetLunchStart();
            }
        }

        /// <summary>
        /// 昼食：開始：分Indexプロパティ
        /// </summary>
        public Int32 LunchStartMinuteIndex
        {
            get { return _LunchStartMinuteIndex; }
            set
            {
                _LunchStartMinuteIndex = value;
                SetLunchStart();
            }
        }

        /// <summary>
        /// 昼食：開始：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetLunchStartHourIndex()
        {
            return GetHourIndex(SetParam.Lunch.Start.Hour);
        }

        /// <summary>
        /// 昼食：開始：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetLunchStartMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Lunch.Start.Minute);
        }

        /// <summary>
        /// 昼食：終了：時Indexプロパティ
        /// </summary>
        public Int32 LunchFinishHourIndex
        {
            get { return _LunchFinishHourIndex; }
            set
            {
                _LunchFinishHourIndex = value;
                SetLunchFinish();
            }
        }

        /// <summary>
        /// 昼食：終了：分Indexプロパティ
        /// </summary>
        public Int32 LunchFinishMinuteIndex
        {
            get { return _LunchFinishMinuteIndex; }
            set
            {
                _LunchFinishMinuteIndex = value;
                SetLunchFinish();
            }
        }

        /// <summary>
        /// 昼食：終了：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetLunchFinishHourIndex()
        {
            return GetHourIndex(SetParam.Lunch.Finish.Hour);
        }

        /// <summary>
        /// 昼食：終了：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetLunchFinishMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Lunch.Finish.Minute);
        }

        /// <summary>
        /// 昼食：開始：時Index
        /// </summary>
        private Int32 _LunchStartHourIndex = -1;

        /// <summary>
        /// 昼食：開始：分Index
        /// </summary>
        private Int32 _LunchStartMinuteIndex = -1;

        /// <summary>
        /// 昼食：終了：時Index
        /// </summary>
        private Int32 _LunchFinishHourIndex = -1;

        /// <summary>
        /// 昼食：終了：分Index
        /// </summary>
        private Int32 _LunchFinishMinuteIndex = -1;

        /// <summary>
        /// 昼食開始時間の設定
        /// </summary>
        public void SetLunchStart()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _LunchStartHourIndex && _LunchStartHourIndex < _Hour.Count)
                && (-1 < _LunchStartMinuteIndex && _LunchStartMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_LunchStartHourIndex];
                Int32 Minute = _Minute[_LunchStartMinuteIndex];

                SetParam.Lunch.Start = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        /// <summary>
        /// 昼食終了時間の設定
        /// </summary>
        public void SetLunchFinish()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _LunchFinishHourIndex && _LunchFinishHourIndex < _Hour.Count)
                && (-1 < _LunchFinishMinuteIndex && _LunchFinishMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_LunchFinishHourIndex];
                Int32 Minute = _Minute[_LunchFinishMinuteIndex];

                SetParam.Lunch.Finish = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        #endregion

        #region 夕食

        /// <summary>
        /// 夕食：開始：時Indexプロパティ
        /// </summary>
        public Int32 DinnerStartHourIndex
        {
            get { return _DinnerStartHourIndex; }
            set
            {
                _DinnerStartHourIndex = value;
                SetDinnerStart();
            }
        }

        /// <summary>
        /// 夕食：開始：分Indexプロパティ
        /// </summary>
        public Int32 DinnerStartMinuteIndex
        {
            get { return _DinnerStartMinuteIndex; }
            set
            {
                _DinnerStartMinuteIndex = value;
                SetDinnerStart();
            }
        }

        /// <summary>
        /// 夕食：開始：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetDinnerStartHourIndex()
        {
            return GetHourIndex(SetParam.Dinner.Start.Hour);
        }

        /// <summary>
        /// 夕食：開始：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetDinnerStartMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Dinner.Start.Minute);
        }

        /// <summary>
        /// 夕食：終了：時Indexプロパティ
        /// </summary>
        public Int32 DinnerFinishHourIndex
        {
            get { return _DinnerFinishHourIndex; }
            set
            {
                _DinnerFinishHourIndex = value;
                SetDinnerFinish();
            }
        }

        /// <summary>
        /// 夕食：終了：分Indexプロパティ
        /// </summary>
        public Int32 DinnerFinishMinuteIndex
        {
            get { return _DinnerFinishMinuteIndex; }
            set
            {
                _DinnerFinishMinuteIndex = value;
                SetDinnerFinish();
            }
        }

        /// <summary>
        /// 夕食：終了：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetDinnerFinishHourIndex()
        {
            return GetHourIndex(SetParam.Dinner.Finish.Hour);
        }

        /// <summary>
        /// 夕食：終了：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetDinnerFinishMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Dinner.Finish.Minute);
        }

        /// <summary>
        /// 夕食：開始：時Index
        /// </summary>
        private Int32 _DinnerStartHourIndex = -1;

        /// <summary>
        /// 夕食：開始：分Index
        /// </summary>
        private Int32 _DinnerStartMinuteIndex = -1;

        /// <summary>
        /// 夕食：終了：時Index
        /// </summary>
        private Int32 _DinnerFinishHourIndex = -1;

        /// <summary>
        /// 夕食：終了：分Index
        /// </summary>
        private Int32 _DinnerFinishMinuteIndex = -1;

        /// <summary>
        /// 夕食開始時間の設定
        /// </summary>
        public void SetDinnerStart()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _DinnerStartHourIndex && _DinnerStartHourIndex < _Hour.Count)
                && (-1 < _DinnerStartMinuteIndex && _DinnerStartMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_DinnerStartHourIndex];
                Int32 Minute = _Minute[_DinnerStartMinuteIndex];

                SetParam.Dinner.Start = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        /// <summary>
        /// 夕食終了時間の設定
        /// </summary>
        public void SetDinnerFinish()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _DinnerFinishHourIndex && _DinnerFinishHourIndex < _Hour.Count)
                && (-1 < _DinnerFinishMinuteIndex && _DinnerFinishMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_DinnerFinishHourIndex];
                Int32 Minute = _Minute[_DinnerFinishMinuteIndex];

                SetParam.Dinner.Finish = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 就寝前：時Indexプロパティ
        /// </summary>
        public Int32 SleepHourIndex
        {
            get { return _SleepHourIndex; }
            set
            {
                _SleepHourIndex = value;
                SetSleep();
            }
        }

        /// <summary>
        /// 就寝前：分Indexプロパティ
        /// </summary>
        public Int32 SleepMinuteIndex
        {
            get { return _SleepMinuteIndex; }
            set
            {
                _SleepMinuteIndex = value;
                SetSleep();
            }
        }

        /// <summary>
        /// 就寝前：時Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetSleepHourIndex()
        {
            return GetHourIndex(SetParam.Sleep.Hour);
        }

        /// <summary>
        /// 就寝前：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetSleepMinuteIndex()
        {
            return GetMinuteIndex(SetParam.Sleep.Minute);
        }

        /// <summary>
        /// 就寝前：時Index
        /// </summary>
        private Int32 _SleepHourIndex = -1;

        /// <summary>
        /// 就寝前：分Index
        /// </summary>
        private Int32 _SleepMinuteIndex = -1;

        /// <summary>
        /// 就寝前時間の設定
        /// </summary>
        public void SetSleep()
        {

            if ((_Hour != null && _Minute != null)
                && (-1 < _SleepHourIndex && _SleepHourIndex < _Hour.Count)
                && (-1 < _SleepMinuteIndex && _SleepMinuteIndex < _Minute.Count))
            {

                Int32 Hour = _Hour[_SleepHourIndex];
                Int32 Minute = _Minute[_SleepMinuteIndex];

                SetParam.Sleep = new Class.Parameter.Method().GetTodayTime(Hour, Minute);

            }

        }

        #endregion

        #region アラーム設定

        /// <summary>
        /// 食前：分Indexプロパティ
        /// </summary>
        public Int32 BeforeMealMinuteIndex
        {
            get { return _BeforeMealMinuteIndex; }
            set
            {
                _BeforeMealMinuteIndex = value;
                SetBeforeMeal();
            }
        }

        /// <summary>
        /// 食前：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBeforeMealMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteBeforeMeals);
        }

        /// <summary>
        /// 食前：分Index
        /// </summary>
        private Int32 _BeforeMealMinuteIndex = -1;

        /// <summary>
        /// 食前アラーム時間の設定
        /// </summary>
        public void SetBeforeMeal()
        {

            if (_AlarmMinute != null
                && (-1 < _BeforeMealMinuteIndex && _BeforeMealMinuteIndex < _AlarmMinute.Count))
            {
                SetParam.MinuteBeforeMeals = _AlarmMinute[_BeforeMealMinuteIndex];
            }

        }

        /// <summary>
        /// 食後：分Indexプロパティ
        /// </summary>
        public Int32 AfterMealMinuteIndex
        {
            get { return _AfterMealMinuteIndex; }
            set
            {
                _AfterMealMinuteIndex = value;
                SetAfterMeal();
            }
        }

        /// <summary>
        /// 食後：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetAfterMealMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteAfterMeals);
        }

        /// <summary>
        /// 食後：分Index
        /// </summary>
        private Int32 _AfterMealMinuteIndex = -1;

        /// <summary>
        /// 食後アラーム時間の設定
        /// </summary>
        public void SetAfterMeal()
        {

            if (_AlarmMinute != null
                && (-1 < _AfterMealMinuteIndex && _AfterMealMinuteIndex < _AlarmMinute.Count))
            {
                SetParam.MinuteAfterMeals = _AlarmMinute[_AfterMealMinuteIndex];
            }

        }

        /// <summary>
        /// 就寝前：分Indexプロパティ
        /// </summary>
        public Int32 BeforeSleepMinuteIndex
        {
            get { return _BeforeSleepMinuteIndex; }
            set
            {
                _BeforeSleepMinuteIndex = value;
                SetBeforeSleep();
            }
        }

        /// <summary>
        /// 就寝前：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetBeforeSleepMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteBeforeSleep);
        }

        /// <summary>
        /// 就寝前：分Index
        /// </summary>
        private Int32 _BeforeSleepMinuteIndex = -1;

        /// <summary>
        /// 就寝前アラーム時間の設定
        /// </summary>
        public void SetBeforeSleep()
        {

            if (_AlarmMinute != null
                && (-1 < _BeforeSleepMinuteIndex && _BeforeSleepMinuteIndex < _AlarmMinute.Count))
            {
                SetParam.MinuteBeforeSleep = _AlarmMinute[_BeforeSleepMinuteIndex];
            }

        }

        /// <summary>
        /// 再通知：分Indexプロパティ
        /// </summary>
        public Int32 ReAlarmMinuteIndex
        {
            get { return _ReAlarmMinuteIndex; }
            set
            {
                _ReAlarmMinuteIndex = value;
                SetReAlarm();
            }
        }

        /// <summary>
        /// 再通知：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetReAlarmMinuteIndex()
        {
            return GetAlarmMinuteIndex(SetParam.MinuteReAlarm);
        }

        /// <summary>
        /// 再通知：分Index
        /// </summary>
        private Int32 _ReAlarmMinuteIndex = -1;

        /// <summary>
        /// 再通知アラーム時間の設定
        /// </summary>
        public void SetReAlarm()
        {

            if (_AlarmMinute != null
                && (-1 < _ReAlarmMinuteIndex && _ReAlarmMinuteIndex < _AlarmMinute.Count))
            {
                SetParam.MinuteReAlarm = _AlarmMinute[_ReAlarmMinuteIndex];
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
        /// 時Listの取得
        /// </summary>
        /// <returns>時List</returns>
        public List<Int32> GetHourList()
        {

            if (_Hour == null)
                _Hour = new List<Int32>();

            _Hour.Clear();

            for (Int32 iLoop = 0; iLoop <= 23; iLoop++)
                _Hour.Add(iLoop);

            return _Hour;

        }

        /// <summary>
        /// 分Listの取得
        /// </summary>
        /// <returns>分List</returns>
        public List<Int32> GetMinuteList()
        {

            if (_Minute == null)
                _Minute = new List<Int32>();

            _Minute.Clear();

            for (Int32 iLoop = 0; iLoop < 60; iLoop += 5)
                _Minute.Add(iLoop);

            return _Minute;

        }

        /// <summary>
        /// アラーム用分Listの取得
        /// </summary>
        /// <returns>分List</returns>
        public List<Int32> GetAlarmMinuteList()
        {

            if (_AlarmMinute == null)
                _AlarmMinute = new List<Int32>();

            _AlarmMinute.Clear();

            for (Int32 iLoop = 0; iLoop <= 60; iLoop += 5)
                _AlarmMinute.Add(iLoop);

            return _AlarmMinute;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;

            SetParam = new Class.Parameter.SettingParameter()
            {
                Breakfast = _Parameter.Setting.Breakfast,
                Lunch = _Parameter.Setting.Lunch,
                Dinner = _Parameter.Setting.Dinner,
                Sleep = _Parameter.Setting.Sleep,
                MinuteBeforeMeals = _Parameter.Setting.MinuteBeforeMeals,
                MinuteBetweenMeals = _Parameter.Setting.MinuteBetweenMeals,
                MinuteAfterMeals = _Parameter.Setting.MinuteAfterMeals,
                MinuteBeforeSleep = _Parameter.Setting.MinuteBeforeSleep,
                MinuteReAlarm = _Parameter.Setting.MinuteReAlarm
            };

        }

        /// <summary>
        /// 終了処理
        /// </summary>
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

        /// <summary>
        /// 時Indexの取得
        /// </summary>
        /// <returns>時List.Index</returns>
        private Int32 GetHourIndex(Int32 Hour)
        {

            Int32 Return = _Hour.IndexOf(Hour);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 分Indexの取得
        /// </summary>
        private Int32 GetMinuteIndex(Int32 Minute)
        {

            Int32 Return = -1;
            for (Int32 iLoop = 0; iLoop < _Minute.Count; iLoop++)
            {
                if (Minute <= _Minute[iLoop])
                {
                    Return = iLoop;
                    break;
                }
            }

            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// アラーム用分Indexの取得
        /// </summary>
        private Int32 GetAlarmMinuteIndex(Int32 Minute)
        {

            Int32 Return = -1;
            for (Int32 iLoop = 0; iLoop < _AlarmMinute.Count; iLoop++)
            {
                if (Minute <= _AlarmMinute[iLoop])
                {
                    Return = iLoop;
                    break;
                }
            }

            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

    }
}
