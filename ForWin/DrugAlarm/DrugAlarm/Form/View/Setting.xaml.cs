using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Setting.xaml の相互作用ロジック
    /// </summary>
    public partial class Setting : Window
    {
        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 戻るボタンクリック
        /// </summary>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();

        }

        /// <summary>
        /// 保存ボタンクリック
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("保存します");
            this.Close();

        }

    }
}
