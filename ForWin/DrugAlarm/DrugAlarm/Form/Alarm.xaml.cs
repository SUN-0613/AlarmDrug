using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DrugAlarm.Class;

namespace DrugAlarm.Form
{
    /// <summary>
    /// Alarm.xaml の相互作用ロジック
    /// </summary>
    public partial class Alarm : Window
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (System.Windows.Application.Current as App).Parameter;

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
