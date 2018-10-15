using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DrugAlarm.Class
{

    /// <summary>
    /// タスクトレイ表示クラス
    /// </summary>
    public partial class TaskTray : Component
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (App.Current as App).Parameter;

        /// <summary>
        /// タイマ処理
        /// </summary>
        private AlarmTimer _Timer;

        /// <summary>
        /// new
        /// </summary>
        public TaskTray()
        {

            InitializeComponent();

            Initialize();

        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="container"></param>
        public TaskTray(IContainer container)
        {

            container.Add(this);

            InitializeComponent();

            Initialize();

        }

        /// <summary>
        /// 初期処理
        /// </summary>
        private void Initialize()
        {

            //右クリックメニュー追加
            AddMenuItem();

            //タイマ処理開始
            _Timer = new AlarmTimer();

        }

        /// <summary>
        /// 右クリックメニューの追加
        /// </summary>
        private void AddMenuItem()
        {

            ToolStripMenuItem AddMenuItem;

            #region 一覧表示
            AddMenuItem = new ToolStripMenuItem
            {
                Name = "ShowList",
                Text = Properties.Resources.Menu_ShowList
            };
            AddMenuItem.Click += new EventHandler(MenuItem_ShowList_Click);

            Menu.Items.Add(AddMenuItem);
            #endregion

            #region 終了
            AddMenuItem = new ToolStripMenuItem
            {
                Name = "Exit",
                Text = Properties.Resources.Menu_Exit
            };
            AddMenuItem.Click += new EventHandler(MenuItem_Exit_Click);

            Menu.Items.Add(AddMenuItem);
            #endregion

        }

        /// <summary>
        /// 一覧表示
        /// </summary>
        private void MenuItem_ShowList_Click(object sender, EventArgs e)
        {

            var ListForm = new DrugAlarm.Form.View.List();
            ListForm.Show();

        }

        /// <summary>
        /// 終了
        /// </summary>
        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {

            //タイマ処理終了
            _Timer.Dispose();
            _Timer = null;

            //プログラム終了
            System.Windows.Application.Current.Shutdown();

        }

    }
}
