using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Alarm.Model
    /// </summary>
    class Alarm : IDisposable
    {

        #region コマンド

        /// <summary>
        /// OKコマンド
        /// </summary>
        public Common.DelegateCommand OkCommand;

        /// <summary>
        /// 再通知コマンド
        /// </summary>
        public Common.DelegateCommand RealarmCommand;

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
        public Alarm()
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

            _Parameter.NextAlarm.Index.ForEach(Index => 
            {
                _DrugList.Add(_Parameter.DrugList[Index].Name);
            });

            return _DrugList;

        }

        /// <summary>
        /// 薬残量計算
        /// </summary>
        public bool TakeMedicine()
        {
            return _Parameter.TakeMedicine();
        }

    }

}
