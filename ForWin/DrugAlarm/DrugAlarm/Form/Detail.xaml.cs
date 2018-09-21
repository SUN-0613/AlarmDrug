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
    /// Detail.xaml の相互作用ロジック
    /// </summary>
    public partial class Detail : Window
    {
        /// <summary>
        /// new
        /// </summary>
        public Detail()
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
