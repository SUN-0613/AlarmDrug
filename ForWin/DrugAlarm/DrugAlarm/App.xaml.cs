using System.Windows;
using System.Threading;
using System.Reflection;
using DrugAlarm.Base;
using DrugAlarm.Class;

namespace DrugAlarm
{

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// パラメータプロパティ
        /// </summary>
        public Parameter Parameter { get; set; }

        /// <summary>
        /// タスクトレイ表示
        /// </summary>
        private TaskTray _TaskTray;

        /// <summary>
        /// 初回起動
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {

            string AppName = Assembly.GetExecutingAssembly().Location;      //実行パス取得
            AppName = System.IO.Path.GetFileNameWithoutExtension(AppName);  //実行ファイル名取得
            Mutex mutex = new Mutex(false, AppName);                        //多重起動情報取得

            //多重起動確認
            if (mutex.WaitOne(0, false))
            {

                //基本処理
                base.OnStartup(e);

                //パラメータ処理
                Parameter = new Parameter();

                //タスクトレイ表示
                ShutdownMode = ShutdownMode.OnExplicitShutdown;
                _TaskTray = new TaskTray();

            }
            else
            {

                MessageBox.Show("多重起動防止", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {

            //基本処理
            base.OnExit(e);

            //パラメータ終了処理
            Parameter.Dispose();

            //タスクトレイから削除
            _TaskTray.Dispose();

        }

    }

}
