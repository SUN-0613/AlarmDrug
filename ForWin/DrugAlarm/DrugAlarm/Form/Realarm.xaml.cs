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

namespace DrugAlarm.Form
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
