using System;
using System.ComponentModel;
using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Setting.xaml の相互作用ロジック
    /// </summary>
    public partial class Setting : Window, IDisposable
    {

        /// <summary>
        /// Setting.ViewModel
        /// </summary>
        private ViewModel.Setting _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Setting();
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

                case "CallCancel":      //キャンセルボタンクリック

                    string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    string Message = DrugAlarm.Properties.Resources.Setting_CancelMessage;

                    if (_ViewModel.IsEdited)
                    {

                        if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            _ViewModel.Initialize();
                            DialogResult = false;
                        }

                    }
                    else
                    {
                        DialogResult = false;
                    }

                    break;

                case "CallSave":        //保存ボタンクリック

                    AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    Message = DrugAlarm.Properties.Resources.Setting_SaveMessage;

                    if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        _ViewModel.Save();
                        DialogResult = true;
                    }

                    break;

                default:
                    break;

            }

        }

    }
}
