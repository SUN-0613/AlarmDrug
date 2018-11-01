using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DrugAlarm.Form.View
{
  
    /// <summary>
    /// Detail.xaml の相互作用ロジック
    /// </summary>
    public partial class Detail : Window, IDisposable
    {

        /// <summary>
        /// Detail.xamlのViewModel
        /// </summary>
        private ViewModel.Detail _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="DrugIndex">薬Index
        /// -1の場合は新規追加</param>
        public Detail(Int32 DrugIndex = -1)
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Detail(DrugIndex);
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
                    string Message = DrugAlarm.Properties.Resources.Detail_CancelMessage;

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
                    Message = _ViewModel.MakeSaveMessage();

                    if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        _ViewModel.Save();
                        DialogResult = true;
                    }

                    break;

                case "CallTotalVolume": //処方量入力オーバー

                    AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    Message = _ViewModel.MakeTotalVolumeOverMessage();

                    MessageBox.Show(Message, AppName, MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    break;

                case "CallAlarmVolume": //残量入力オーバー

                    AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                    Message = _ViewModel.MakeAlarmVolumeOverMessage();

                    MessageBox.Show(Message, AppName, MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// TextBox.GotFocus
        /// </summary>
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            if (!(e.OriginalSource is TextBox TBox)) return;

            Action SelectAll = TBox.SelectAll;
            Dispatcher.BeginInvoke(SelectAll);

        }

    }
}
