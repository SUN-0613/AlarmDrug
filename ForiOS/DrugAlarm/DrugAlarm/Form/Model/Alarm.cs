using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Alarm.Model
    /// </summary>
    public class Alarm : IDisposable
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

                _DrugList.Add(new Class.UserControl.MedicineInfo(_Parameter.DrugList[index].Name
                                                                , _Parameter.NextAlarm.DrugList[iLoop].Volume
                                                                , iLoop));
            }

            return _DrugList;

        }

        #endregion

        #region コマンド

        /// <summary>
        /// OKコマンド
        /// </summary>
        public Common.DelegateCommand OkCommand;

        /// <summary>
        /// 薬残量計算
        /// </summary>
        /// <returns><c>true</c>, 薬切れ有, <c>false</c> 在庫十分</returns>
        public bool TakeMedicine()
        {

            // 服用FLGの更新
            _DrugList.ForEach(Drug => 
            {
                _Parameter.UpdateDrugFlgNextAlarm(Drug.Index, Drug.IsDrug);
            });

            return _Parameter.TakeMedicine();
        }

        /// <summary>
        /// 再通知コマンド
        /// </summary>
        public Common.DelegateCommand RealarmCommand;

        /// <summary>
        /// 全てチェックコマンド
        /// </summary>
        public Common.DelegateCommand AllCheckCommand;

        /// <summary>
        /// 全てチェック
        /// </summary>
        public void AllCheck()
        {
            _DrugList.ForEach(Drug => 
            {
                Drug.IsDrug = true;
            });
        }

        /// <summary>
        /// 全てチェック解除コマンド
        /// </summary>
        public Common.DelegateCommand AllUncheckCommand;

        /// <summary>
        /// 全てチェック解除
        /// </summary>
        public void UnallCheck()
        {
            _DrugList.ForEach(Drug => 
            {
                Drug.IsDrug = false;
            });
        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {
            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.Alarm"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.Alarm"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.Alarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.Alarm"/> was occupying.</remarks>
        public void Dispose()
        {
            if (_DrugList != null)
            {
                _DrugList.Clear();
                _DrugList = null;
            }
        }

    }
}
