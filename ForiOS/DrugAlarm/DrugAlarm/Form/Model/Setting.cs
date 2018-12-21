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
            _Parameter.Save(false);
            _Parameter.Load();

        }

        #endregion

        #region 朝食

        /// <summary>
        /// 時間プロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The breakfast start.</value>
        public TimeSpan BreakfastStart
        {
            get { return SetParam.Breakfast.Start.TimeOfDay; }
            set
            {
                if (!SetParam.Breakfast.Start.TimeOfDay.Equals(value))
                    SetParam.Breakfast.Start = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The breakfast finish.</value>
        public TimeSpan BreakfastFinish
        {
            get { return SetParam.Breakfast.Finish.TimeOfDay; }
            set
            {
                if (!SetParam.Breakfast.Finish.TimeOfDay.Equals(value))
                    SetParam.Breakfast.Finish = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        #endregion

        #region 昼食

        /// <summary>
        /// 時間プロパティ
        /// 昼食
        /// 開始
        /// </summary>
        /// <value>The lunch start.</value>
        public TimeSpan LunchStart
        {
            get { return SetParam.Lunch.Start.TimeOfDay; }
            set
            {
                if (!SetParam.Lunch.Start.TimeOfDay.Equals(value))
                    SetParam.Lunch.Start = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 昼食
        /// 終了
        /// </summary>
        /// <value>The lunch finish.</value>
        public TimeSpan LunchFinish
        {
            get { return SetParam.Lunch.Finish.TimeOfDay; }
            set
            {
                if (!SetParam.Lunch.Finish.TimeOfDay.Equals(value))
                    SetParam.Lunch.Finish = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        #endregion

        #region 夕食

        /// <summary>
        /// 時間プロパティ
        /// 夕食
        /// 開始
        /// </summary>
        /// <value>The dinner start.</value>
        public TimeSpan DinnerStart
        {
            get { return SetParam.Dinner.Start.TimeOfDay; }
            set
            {
                if (!SetParam.Dinner.Start.TimeOfDay.Equals(value))
                    SetParam.Dinner.Start = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 夕食
        /// 終了
        /// </summary>
        /// <value>The dinner finish.</value>
        public TimeSpan DinnerFinish
        {
            get { return SetParam.Dinner.Finish.TimeOfDay; }
            set
            {
                if (!SetParam.Dinner.Finish.TimeOfDay.Equals(value))
                    SetParam.Dinner.Finish = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 時間プロパティ
        /// 就寝前
        /// </summary>
        /// <value>The sleep start.</value>
        public TimeSpan SleepStart
        {
            get { return SetParam.Sleep.TimeOfDay; }
            set
            {
                if (!SetParam.Sleep.TimeOfDay.Equals(value))
                    SetParam.Sleep = new Class.Method().GetTodayTime(value.Hours, value.Minutes);
            }
        }

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
        /// アラーム用分List
        /// </summary>
        private List<Int32> _AlarmMinute;

        /// <summary>
        /// アラーム用分Listの作成
        /// </summary>
        /// <returns>The alarm minute list.</returns>
        public List<Int32> GetAlarmMinuteList()
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

            if (_AlarmMinute != null)
            {
                _AlarmMinute.Clear();
                _AlarmMinute = null;
            }

        }

    }
}
