using System.Windows;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// List.xaml の相互作用ロジック
    /// </summary>
    public partial class List : Window
    {

        /// <summary>
        /// List.xamlのViewModel
        /// </summary>
        private ViewModel.List ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            InitializeComponent();

            ViewModel = new ViewModel.List();
            this.DataContext = ViewModel;

        }

        /// <summary>
        /// 設定ボタンクリック
        /// </summary>
        private void Setting_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.CallSetting(this);

        }

        /// <summary>
        /// 新規追加ボタンクリック
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.CallDetailForm(this, true);

        }

        /// <summary>
        /// メニュー：編集クリック
        /// </summary>
        private void Menu_Edit_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.CallDetailForm(this, false);

        }

        /// <summary>
        /// メニュー：削除クリック
        /// </summary>
        private void Menu_Delete_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.DeleteDrug();

        }

        /// <summary>
        /// メニュー：頓服服用クリック
        /// </summary>
        private void Menu_Drug_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.DrugMedicine();

        }

    }

}
