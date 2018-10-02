using System.Windows;
using DrugAlarm.Class;

using System.Collections.ObjectModel;

namespace DrugAlarm.Form
{

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// List.xaml の相互作用ロジック
    /// </summary>
    public partial class List : Window
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (System.Windows.Application.Current as App).Parameter;

        private ObservableCollection<Person> TEST = new ObservableCollection<Person>();

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            InitializeComponent();

            /*
            TEST.Add(new Person { Name = "AAA" });
            TEST.Add(new Person { Name = "BBB" });

            this.DataContext = TEST;
            */

            this.DataContext = _Parameter.DrugList;

        }

        /// <summary>
        /// 設定ボタンクリック
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _Parameter.DebugTest_AddDrug("TEST");
            //TEST.Add(new Person { Name = "CCC" });
            return;

            var form = new Setting
            {
                Owner = this
            };
            form.ShowDialog();

        }

    }

}
