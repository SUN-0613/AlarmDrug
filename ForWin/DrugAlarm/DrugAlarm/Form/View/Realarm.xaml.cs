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
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                default:
                    break;

            }

        }

    }
}
