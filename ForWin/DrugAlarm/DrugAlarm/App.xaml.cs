using System.Windows;
using System.Threading;
using System.Reflection;
using DrugAlarm.Form;

namespace DrugAlarm
{

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// 初回起動
        /// </summary>
        private void App_Startup(object sender, StartupEventArgs e)
        {

            string AppName = Assembly.GetExecutingAssembly().Location;      //実行パス取得
            AppName = System.IO.Path.GetFileNameWithoutExtension(AppName);  //実行ファイル名取得
            Mutex mutex = new Mutex(false, AppName);                        //多重起動情報取得

            //多重起動確認
            if (mutex.WaitOne(0, false))
            {

                //画面表示
                List MainForm = new List();
                MainForm.ShowDialog();

            }
            else
            {

                MessageBox.Show("多重起動防止", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();

            }

        }

    }

}
