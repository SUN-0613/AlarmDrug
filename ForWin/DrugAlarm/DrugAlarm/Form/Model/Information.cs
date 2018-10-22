using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Information.Model
    /// </summary>
    class Information : IDisposable
    {

        #region コマンド

        /// <summary>
        /// OKコマンド
        /// </summary>
        public Common.DelegateCommand OkCommand;

        #endregion

        #region パラメータ

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        /// <summary>
        /// 表示薬名称一覧
        /// </summary>
        private List<string> _DrugList;

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            if (_DrugList != null)
            {
                _DrugList.Clear();
                _DrugList = null;
            }
        }

        /// <summary>
        /// 表示薬一覧取得
        /// </summary>
        /// <returns></returns>
        public List<string> GetDrugList()
        {

            if (_DrugList == null)
                _DrugList = new List<string>();

            _DrugList.Clear();

            _Parameter.DrugList.ForEach(Drug =>
            {

                if (Drug.IsPrescriptionAlarm
                    && Drug.TotalVolume <= Drug.PrescriptionAlarmVolume)
                {
                    _DrugList.Add(Drug.Name);
                }

            });

            return _DrugList;

        }

    }
}
