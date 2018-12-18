using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Alarm.ViewModel
    /// </summary>
    public class Alarm : Common.ViewModelBase, IDisposable
    {

        #region 基底

        /// <summary>
        /// Alarm.Model
        /// </summary>
        private Model.Alarm _Model;

        #endregion

        #region コマンド

        /// <summary>
        /// OKコマンドプロパティ
        /// </summary>
        /// <value>The ok command.</value>
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
        /// <returns><c>true</c>, 薬切れ有, <c>false</c> 在庫十分</returns>
        public bool TakeMedicine()
        {
            return _Model.TakeMedicine();
        }

        /// <summary>
        /// 再通知コマンドプロパティ
        /// </summary>
        /// <value>The realarm command.</value>
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

        /// <summary>
        /// 全てチェックコマンド
        /// </summary>
        public Common.DelegateCommand AllCheckCommand
        {
            get
            {

                if (_Model.AllCheckCommand == null)
                {
                    _Model.AllCheckCommand = new Common.DelegateCommand(
                        ()=> 
                        { 
                            _Model.AllCheck();
                            UpdateCheck(true);
                        }, 
                        () => true);
                }

                return _Model.AllCheckCommand;

            }
        }

        /// <summary>
        /// 全てチェック解除コマンド
        /// </summary>
        public Common.DelegateCommand AllUncheckCommand
        {
            get
            {

                if (_Model.AllUncheckCommand == null)
                {
                    _Model.AllUncheckCommand = new Common.DelegateCommand(
                        () => 
                        {
                            _Model.UnallCheck();
                            UpdateCheck(false);
                        },
                        () => true);
                }

                return _Model.AllUncheckCommand;

            }
        }

        /// <summary>
        /// 薬一覧のチェック更新
        /// </summary>
        /// <param name="value">If set to <c>true</c> value.</param>
        private void UpdateCheck(bool value)
        {

            for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
            {
                DrugList[iLoop].IsDrug = value;
            }

        }

        #endregion

        #region BindingProperty

        /// <summary>
        /// 表示薬一覧プロパティ
        /// </summary>
        /// <value>The drug list.</value>
        public ObservableCollection<Class.UserControl.MedicineInfo> DrugList { get; set; }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {

            _Model = new Model.Alarm();

            DrugList = new ObservableCollection<Class.UserControl.MedicineInfo>();

            Int32 index = 0;
            _Model.GetDrugList().ForEach(Drug => 
            {
                DrugList.Add(new Class.UserControl.MedicineInfo(Drug.Name, Drug.Volume, index));
                index++;
            });

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> was occupying.</remarks>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}
