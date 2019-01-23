using System;
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

        /// <summary>
        /// 表示薬名称一覧
        /// </summary>
        private List<Class.UserControl.MedicineInfo> _DrugList;

        /// <summary>
        /// 表示薬一覧取得
        /// </summary>
        /// <returns>The drug list.</returns>
        public List<Class.UserControl.MedicineInfo> GetDrugList()
        {

            if (_DrugList == null)
            {
                _DrugList = new List<Class.UserControl.MedicineInfo>();
            }

            _DrugList.Clear();

            for (Int32 iLoop = 0; iLoop < _Parameter.NextAlarm.DrugList.Count; iLoop++)
            {

                Int32 index = _Parameter.NextAlarm.DrugList[iLoop].Index;

                if (!_Parameter.NextAlarm.DrugList[iLoop].IsDrug)
                {
                    _DrugList.Add(new Class.UserControl.MedicineInfo(_Parameter.DrugList[index].Name
                                                                    , _Parameter.NextAlarm.DrugList[iLoop].Volume
                                                                    , iLoop));
                }

            }

            return _DrugList;

        }

        #endregion

        #region List

        /// <summary>
        /// 分List
        /// </summary>
        private List<Int32> _RealarmMinute;

        /// <summary>
        /// 分Index
        /// </summary>
        public Int32 RealarmMinuteIndex = -1;

        /// <summary>
        /// 分List取得
        /// </summary>
        /// <returns>The alarm minute list.</returns>
        public List<Int32> GetRealarmMinuteList()
        {

            if (_RealarmMinute == null)
                _RealarmMinute = new List<Int32>();

            _RealarmMinute.Clear();

            for (Int32 iLoop = 5; iLoop <= 60; iLoop += 5)
                _RealarmMinute.Add(iLoop);

            return _RealarmMinute;

        }

        /// <summary>
        /// 指定時間の該当分Index取得
        /// </summary>
        /// <returns>指定時間(分)</returns>
        /// <param name="Minute">Minute.</param>
        public Int32 GetRealarmMinuteIndex(Int32 Minute)
        {
            Int32 Return = -1;

            if (_RealarmMinute == null)
                return Return;

            for (Int32 iLoop = 0; iLoop < _RealarmMinute.Count; iLoop++)
            {
                if (Minute <= _RealarmMinute[iLoop])
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
            return GetRealarmMinuteIndex(_Parameter.Setting.MinuteRealarm);
        }

        #endregion

        #region Command

        /// <summary>
        /// キャンセルコマンド
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンド
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Parameter.SetRealarm(_RealarmMinute[RealarmMinuteIndex]);
        }

        /// <summary>
        /// スキップコマンド
        /// </summary>
        public Common.DelegateCommand SkipCommand;

        /// <summary>
        /// スキップ
        /// </summary>
        public void Skip()
        {
            _Parameter.Skip();
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
            if (_RealarmMinute != null)
            {
                _RealarmMinute.Clear();
                _RealarmMinute = null;
            }

            if (_DrugList != null)
            {
                _DrugList.Clear();
                _DrugList = null;
            }
        }

    }
}
