using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Alarm.ViewModel
    /// </summary>
    class Alarm : Common.ViewModelBase, IDisposable
    {

        #region コマンド

        /// <summary>
        /// OKコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand OkCommand
        {
            get
            {

                if (_Model.OkCommand == null)
                {
                    _Model.OkCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallOK"); },
                        () => true);
                }

                return _Model.OkCommand;

            }
        }

        /// <summary>
        /// 薬残量計算
        /// </summary>
        public bool TakeMedicine()
        {
            return _Model.TakeMedicine();
        }

        /// <summary>
        /// 再通知コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand RealarmCommand
        {
            get
            {

                if (_Model.RealarmCommand == null)
                {
                    _Model.RealarmCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallRealarm"); },
                        () => true);
                }

                return _Model.RealarmCommand;

            }
        }

        #endregion

        #region プロパティ

        /// <summary>
        /// 表示薬一覧プロパティ
        /// </summary>
        public ObservableCollection<string> DrugList { get; set; }

        #endregion

        /// <summary>
        /// Alarm.Model
        /// </summary>
        private Model.Alarm _Model;

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {

            _Model = new Model.Alarm();

            DrugList = new ObservableCollection<string>();

            _Model.GetDrugList().ForEach(Name => 
            {
                DrugList.Add(Name);
            });

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _Model.Dispose();
            _Model = null;

            DrugList.Clear();
            DrugList = null;

        }

    }

}
