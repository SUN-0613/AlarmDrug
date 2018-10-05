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
