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
