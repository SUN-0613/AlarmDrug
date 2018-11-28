﻿using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Realarm.Model
    /// </summary>
    public class Realarm : IDisposable
    {

        #region 基底

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        #endregion

        #region List

        /// <summary>
        /// 分List
        /// </summary>
        private List<Int32> _AlarmMinute;

        /// <summary>
        /// 分Index
        /// </summary>
        public Int32 AlarmMinuteIndex = -1;

        /// <summary>
        /// 分List取得
        /// </summary>
        /// <returns>The alarm minute list.</returns>
        public List<Int32> GetAlarmMinuteList()
        {

            if (_AlarmMinute == null)
                _AlarmMinute = new List<Int32>();

            _AlarmMinute.Clear();

            for (Int32 iLoop = 5; iLoop < 60; iLoop += 5)
                _AlarmMinute.Add(iLoop);

            return _AlarmMinute;

        }

        /// <summary>
        /// 指定時間の該当分Index取得
        /// </summary>
        /// <returns>指定時間(分)</returns>
        /// <param name="Minute">Minute.</param>
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

            if (Return.Equals(-1))
                Return = 0;

            return Return;

        }

        /// <summary>
        /// パラメータ初期設定の分Index取得
        /// </summary>
        /// <returns>The alarm minute index.</returns>
        public Int32 GetAlarmMinuteIndex()
        {
            return GetAlarmMinuteIndex(_Parameter.Setting.MinuteRealarm);
        }

        #endregion

        #region Command

        /// <summary>
        /// 保存コマンド
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

        /// <summary>
        /// new
        /// </summary>
        public Realarm()
        {
            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.Realarm"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.Realarm"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.Realarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.Realarm"/> was occupying.</remarks>
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
