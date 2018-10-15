using System;
using System.Windows;

namespace DrugAlarm.Form.View
{
  
    /// <summary>
    /// Detail.xaml の相互作用ロジック
    /// </summary>
    public partial class Detail : Window
    {

        /// <summary>
        /// Detail.xamlのViewModel
        /// </summary>
        private ViewModel.Detail ViewModel;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="DrugIndex">薬Index
        /// -1の場合は新規追加</param>
        public Detail(Int32 DrugIndex = -1)
        {

            InitializeComponent();

            ViewModel = new ViewModel.Detail(DrugIndex);
            this.DataContext = ViewModel.Bind;

        }

        /// <summary>
        /// 戻るボタンクリック
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.Initialize();
            this.Close();

        }

        /// <summary>
        /// 保存ボタンクリック
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.Save();
            this.Close();

        }

    }
}
