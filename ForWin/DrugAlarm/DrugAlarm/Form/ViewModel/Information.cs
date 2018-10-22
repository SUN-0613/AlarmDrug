using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Information.ViewModel
    /// </summary>
    class Information : Common.ViewModelBase, IDisposable
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

        #endregion

        /// <summary>
        /// 表示薬一覧プロパティ
        /// </summary>
        public ObservableCollection<string> DrugList { get; set; }

        /// <summary>
        /// Information.Model
        /// </summary>
        private Model.Information _Model;

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {

            _Model = new Model.Information();

            DrugList = new ObservableCollection<string>();

            _Model.GetDrugList().ForEach(Name => 
            {
                DrugList.Add(Name);
            });

            if (DrugList.Count.Equals(0))
                CallPropertyChanged("CallCancel");

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

            _Model.Dispose();
            _Model = null;
        }

    }
}
