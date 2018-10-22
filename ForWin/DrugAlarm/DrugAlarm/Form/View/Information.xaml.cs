using System;
using System.ComponentModel;
using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Information.xaml の相互作用ロジック
    /// </summary>
    public partial class Information : Window, IDisposable
    {

        /// <summary>
        /// 終了処理許可
        /// </summary>
        private bool IsCloseEnabled = false;

        /// <summary>
        /// Information.ViewModel
        /// </summary>
        private ViewModel.Information _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {
            InitializeComponent();

            _ViewModel = new ViewModel.Information();
            DataContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!IsCloseEnabled)
                e.Cancel = true;
        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallOK":      //OKボタンクリック
                    IsCloseEnabled = true;
                    DialogResult = true;
                    break;

                case "CallCancel":  //キャンセル処理
                    IsCloseEnabled = true;
                    DialogResult = false;
                    break;

                default:
                    break;

            }

        }

    }
}
