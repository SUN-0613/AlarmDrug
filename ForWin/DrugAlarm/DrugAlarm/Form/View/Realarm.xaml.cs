using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Realarm.xaml の相互作用ロジック
    /// </summary>
    public partial class Realarm : Window
    {
        public Realarm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 設定ボタンクリック
        /// </summary>
        private void Setting_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("設定します");
            this.Close();

        }

    }
}
