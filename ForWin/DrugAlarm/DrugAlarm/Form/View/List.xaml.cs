using System;
using System.Windows;
using System.ComponentModel;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// List.xaml の相互作用ロジック
    /// </summary>
    public partial class List : Window, IDisposable
    {

        /// <summary>
        /// List.xamlのViewModel
        /// </summary>
        private ViewModel.List _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.List();
            this.DataContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _ViewModel.Dispose();
            _ViewModel = null;

            _ViewModel.PropertyChanged -= OnPropertyChanged;

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallSetting":     //設定ボタンクリック

                    var Setting = new View.Setting
                    {
                        Owner = this
                    };
                    Setting.ShowDialog();

                    break;

                case "CallAddDrug":     //新規追加ボタンクリック

                    var AddDrug = new View.Detail(-1)
                    {
                        Owner = this
                    };
                    AddDrug.ShowDialog();
                    
                    break;

                case "CallEditDrug":        //編集メニュークリック

                    var EditDrug = new View.Detail(_ViewModel.SelectedIndex)
                    {
                        Owner = this
                    };
                    EditDrug.ShowDialog();

                    break;

                case "CallDeleteDrug":      //削除メニュークリック

                    string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    string Message = _ViewModel.MakeDeleteDrugMessage();

                    if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        _ViewModel.DeleteDrug();
                    }

                    break;

                case "CallDrugMedicine":    //服用メニュークリック

                    _ViewModel.DrugMedicine();

                    AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    Message = _ViewModel.MakeDrugMedicineMessage();

                    MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information);

                    break;

                default:
                    break;

            }

        }

    }

}
