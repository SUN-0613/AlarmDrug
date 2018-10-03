using System.Windows;
using DrugAlarm.Class;

namespace DrugAlarm.Form
{

    /// <summary>
    /// List.xaml の相互作用ロジック
    /// </summary>
    public partial class List : Window
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (System.Windows.Application.Current as App).Parameter;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            InitializeComponent();

            //binding
            this.DataContext = _Parameter.DrugList;

#if DEBUG
            _Parameter.DebugTest_AddDrug("TEST");
#endif

        }

        /// <summary>
        /// 設定ボタンクリック
        /// </summary>
        private void Setting_Click(object sender, RoutedEventArgs e)
        {

            var form = new Setting
            {
                Owner = this
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 新規追加ボタンクリック
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {

            var form = new Detail
            {
                Owner = this
            };
            form.ShowDialog();

        }

    }

}
