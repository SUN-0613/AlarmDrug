using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// History.ViewModel
    /// </summary>
    public class History : Common.ViewModelBase, IDisposable
    {

        #region Command

        /// <summary>
        /// 戻るコマンド
        /// </summary>
        /// <value>The return command.</value>
        public Common.DelegateCommand ReturnCommand
        {
            get
            {

                if (_Model.ReturnCommand == null)
                {
                    _Model.ReturnCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallReturn"); }, 
                        () => true);
                }

                return _Model.ReturnCommand;

            }
        }

        #endregion

        #region BindingProperty

        /// <summary>
        /// 履歴一覧
        /// </summary>
        public ObservableCollection<Model.History.HistoryInfo> HistoryList { get; set; };

        #endregion

        #region Model

        /// <summary>
        /// The model.History
        /// </summary>
        private Model.History _Model;

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public History()
        {

            _Model = new Model.History();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.History"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.History"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.History"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.History"/> was occupying.</remarks>
        public void Dispose()
        {

            _Model.Dispose();
            _Model = null;

            ClearHistory();

        }

        /// <summary>
        /// 履歴一覧初期化
        /// </summary>
        private void ClearHistory()
        {

            if (HistoryList != null)
            {

                for (Int32 iLoop = 0; iLoop < HistoryList.Count; iLoop++)
                {
                    HistoryList[iLoop].Dispose();
                    HistoryList[iLoop] = null;
                }

                HistoryList.Clear();
                HistoryList = null;

            }

        }

        /// <summary>
        /// 一覧表示
        /// </summary>
        public void ShowList()
        {

            ClearHistory();
            HistoryList = new ObservableCollection<Model.History.HistoryInfo>();

            _Model.GetHistoryList().ForEach(AddData => 
            {

                HistoryList.Add(new Model.History.HistoryInfo()
                {
                    Timer = AddData.Timer 
                });

                for (Int32 iLoop = 0; iLoop < AddData.DrugList.Count; iLoop++)
                {

                    HistoryList[HistoryList.Count - 1].DrugList.Add(new Model.History.Drug() 
                    {
                        Name =  AddData.DrugList[iLoop].Name,
                        Volume = AddData.DrugList[iLoop].Volume
                    });

                }

            });

        }

    }
}
