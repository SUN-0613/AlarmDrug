using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Reflection;

namespace DrugAlarm.Base
{

    /// <summary>
    /// タスクトレイ表示クラス
    /// </summary>
    public partial class TaskTray : Component
    {

        /// <summary>
        /// new
        /// </summary>
        public TaskTray()
        {

            InitializeComponent();

            AddMenuItem();

        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="container"></param>
        public TaskTray(IContainer container)
        {

            container.Add(this);

            InitializeComponent();

            AddMenuItem();

        }

        /// <summary>
        /// 右クリックメニューの追加
        /// </summary>
        private void AddMenuItem()
        {

            ToolStripMenuItem AddMenuItem;

            AddMenuItem = new ToolStripMenuItem();
            AddMenuItem.Name = "ShowList";
            AddMenuItem.Text = Properties.Resources.Menu_ShowList;
            AddMenuItem.Click += new EventHandler(MenuItem_ShowList_Click);

            Menu.Items.Add(AddMenuItem);

            AddMenuItem = new ToolStripMenuItem();
            AddMenuItem.Name = "Exit";
            AddMenuItem.Text = Properties.Resources.Menu_Exit;
            AddMenuItem.Click += new EventHandler(MenuItem_Exit_Click);

            Menu.Items.Add(AddMenuItem);

        }

        /// <summary>
        /// 一覧表示
        /// </summary>
        private void MenuItem_ShowList_Click(object sender, EventArgs e)
        {

            var ListForm = new DrugAlarm.Form.List();
            ListForm.Show();

        }

        /// <summary>
        /// 終了
        /// </summary>
        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
