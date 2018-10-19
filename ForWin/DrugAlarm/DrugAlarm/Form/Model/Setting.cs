using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 朝食：開始：時Index
        /// </summary>
        private Int32 _BreakfastStartHourIndex;

        /// <summary>
        /// 朝食：開始：分Index
        /// </summary>
        private Int32 _BreakfastStartMinuteIndex;

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
        public Int32 GetMinuteIndex(Int32 Minute)
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
    }
}
