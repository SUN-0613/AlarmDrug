using System.Windows;

namespace DrugAlarm.Form.View
{
    /// <summary>
    /// Alarm.xaml の相互作用ロジック
    /// </summary>
    public partial class Alarm : Window
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter = (System.Windows.Application.Current as App).Parameter;

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 完了ボタンクリック
        /// </summary>
        private void OK_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("完了します");
            this.Close();

        }

        /// <summary>
        /// 再通知ボタンクリック
        /// </summary>
        private void Realarm_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("再通知します");
            this.Close();

        }

    }
}
