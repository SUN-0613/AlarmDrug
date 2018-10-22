using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Realarm.Model
    /// </summary>
    class Realarm : IDisposable
    {

        #region パラメータ

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        #endregion

        #region コマンド

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Parameter.SetRealarm(_AlarmMinute[AlarmMinuteIndex]);
        }

        #endregion

        #region プロパティ

        /// <summary>
        /// 食前：分Index
        /// </summary>
        public Int32 AlarmMinuteIndex = -1;

        /// <summary>
        /// 食前：分Indexの取得
        /// </summary>
        /// <returns></returns>
        public Int32 GetAlarmMinuteIndex()
        {
            return GetAlarmMinuteIndex(_Parameter.Setting.MinuteReAlarm);
        }

        #endregion

        #region List

        /// <summary>
        /// アラーム用分List
        /// </summary>
        private List<Int32> _AlarmMinute;

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

        /// <summary>
        /// アラーム用分Indexの取得
        /// </summary>
        public Int32 GetAlarmMinuteIndex(Int32 Minute)
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

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Realarm()
        {
            _Parameter = (System.Windows.Application.Current as App).Parameter;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
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
