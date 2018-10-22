using System;
using System.ComponentModel;
using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Realarm.xaml の相互作用ロジック
    /// </summary>
    public partial class Realarm : Window, IDisposable
    {

        /// <summary>
        /// Realarm.ViewModel
        /// </summary>
        private ViewModel.Realarm _ViewModel;

        /// <summary>
        /// 画面終了可能FLG
        /// </summary>
        private bool IsCloseEnabled = false;

        /// <summary>
        /// new
        /// </summary>
        public Realarm()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Realarm();
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

                case "CallSave":
                    IsCloseEnabled = true;
                    DialogResult = true;
                    break;

                default:
                    break;

            }

        }

    }
}
