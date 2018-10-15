using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Information.xaml の相互作用ロジック
    /// </summary>
    public partial class Information : Window
    {
        /// <summary>
        /// new
        /// </summary>
        public Information()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OKボタンクリック
        /// </summary>
        private void OK_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("OKです");
            this.Close();

        }

    }
}
