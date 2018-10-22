using System;
using System.ComponentModel;
using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Alarm.xaml の相互作用ロジック
    /// </summary>
    public partial class Alarm : Window, IDisposable
    {

        /// <summary>
        /// Alarm.ViewModel
        /// </summary>
        private ViewModel.Alarm _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Alarm();
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

                case "CallOk":      //OKボタンクリック

                    //残り錠の計算、次回アラーム設定
                    if (_ViewModel.TakeMedicine())
                    {
                        //薬切れアラーム表示
                        var Information = new View.Information
                        {
                            Owner = this
                        };
                        Information.ShowDialog();

                    }

                    DialogResult = true;
                    break;

                case "CallRealarm": //再通知ボタンクリック

                    //再通知設定表示
                    var Realarm = new View.Realarm
                    {
                        Owner = this
                    };
                    Realarm.ShowDialog();

                    DialogResult = false;
                    break;

                default:
                    break;

            }

        }

    }
}
