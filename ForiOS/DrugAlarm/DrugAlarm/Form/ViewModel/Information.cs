using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Information.ViewModel
    /// </summary>
    public class Information : Common.ViewModelBase, IDisposable
    {


        #region 基底

        /// <summary>
        /// Information.Model
        /// </summary>
        private Model.Information _Model;

        #endregion

        #region BindingProperty

        /// <summary>
        /// 表示薬一覧プロパティ
        /// </summary>
        /// <value>The drug list.</value>
        public ObservableCollection<Class.UserControl.MedicineInfo> DrugList { get; set; }

        #endregion

        #region CommandProperty

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

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {

            _Model = new Model.Information();

            DrugList = new ObservableCollection<Class.UserControl.MedicineInfo>();

            _Model.GetDrugList().ForEach(Drug => 
            {
                DrugList.Add(new Class.UserControl.MedicineInfo(Drug.Name, Drug.Volume, -1));
            });

            if (DrugList.Count.Equals(0))
                CallPropertyChanged("CallCancel");

        }

        /// <summary>
        /// 処理終了
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Information"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Information"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Information"/> so the garbage collector can reclaim the memory that
        /// the <see cref="T:DrugAlarm.Form.ViewModel.Information"/> was occupying.</remarks>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}
