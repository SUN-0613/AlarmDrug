using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using DrugAlarm.Class;
using DrugAlarm.Base;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// List.xamlのViewModel
    /// </summary>
    class List : ViewModelBase, IDisposable
    {

        /// <summary>
        /// DrugParameterより一部抜粋
        /// </summary>
        public class DrugParameter : ViewModelBase
        {

            /// <summary>
            /// 名称
            /// </summary>
            private string _Name;

            /// <summary>
            /// :
            /// </summary>
            private string _Colon;

            /// <summary>
            /// 服用タイミング
            /// </summary>
            private string _DrugTiming;

            /// <summary>
            /// 備考
            /// </summary>
            private string _Remarks;

            /// <summary>
            /// 薬切れFLG
            /// </summary>
            private bool _IsPrescriptionAlarm;

            /// <summary>
            /// 名称プロパティ
            /// </summary>
            public string Name
            {
                get { return _Name; }
                set
                {
                    _Name = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// コロンプロパティ
            /// </summary>
            public string Colon
            {
                get { return _Colon; }
                set
                {
                    _Colon = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 服用タイミングプロパティ
            /// </summary>
            public string DrugTiming
            {
                get { return _DrugTiming; }
                set
                {
                    _DrugTiming = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 備考プロパティ
            /// </summary>
            public string Remarks
            {
                get { return _Remarks; }
                set
                {
                    _Remarks = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 薬切れFLGプロパティ
            /// </summary>
            public bool IsPrescriptionAlarm
            {
                get { return _IsPrescriptionAlarm; }
                set
                {
                    _IsPrescriptionAlarm = value;
                    CallPropertyChanged();
                }
            }

        }

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<DrugParameter> DrugList { get; set; }

        /// <summary>
        /// ListBox.SelectedIndex
        /// </summary>
        private Int32 _SelectedIndex = -1;

        /// <summary>
        /// ListBox.SelectedIndexプロパティ
        /// </summary>
        public Int32 SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (!_SelectedIndex.Equals(value))
                {
                    _SelectedIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter;

        /// <summary>
        /// 100msタイマ
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;
            DrugList = new ObservableCollection<DrugParameter>();

            //タイマ処理
            _Timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.IsEnabled = true;
            _Timer.Start();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            //タイマ終了
            if (_Timer.IsEnabled)
            {
                _Timer.IsEnabled = false;
                _Timer.Stop();
            }
            _Timer.Tick -= new EventHandler(Timer_Tick);

            DrugList.Clear();
            DrugList = null;

        }

        /// <summary>
        /// タイマ処理
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (!DrugList.Count.Equals(_Parameter.DrugList.Count))
            {

                DrugList.Clear();

                _Parameter.DrugList.ForEach(Drug =>
                    {

                        DrugParameter AddDrug = new DrugParameter();

                        AddDrug.Name = Drug.Name;
                        AddDrug.Colon = ":";
                        AddDrug.DrugTiming = Drug.DrugTiming;
                        AddDrug.Remarks = Drug.Remarks;
                        AddDrug.IsPrescriptionAlarm = Drug.IsPrescriptionAlarm;

                        DrugList.Add(AddDrug);

                    });

            }
            else
            {

                for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                {

                    DrugParameter Drug = DrugList[iLoop];
                    Parameter.DrugParameter Param = _Parameter.DrugList[iLoop];

                    if (!Drug.Name.Equals(Param.Name))
                    {
                        Drug.Name = Param.Name;
                    }

                    if (!Drug.DrugTiming.Equals(Param.DrugTiming))
                    {
                        Drug.DrugTiming = Param.DrugTiming;
                    }

                    if (!Drug.Remarks.Equals(Param.Remarks))
                    {
                        Drug.Remarks = Param.Remarks;
                    }

                    if (!Drug.IsPrescriptionAlarm.Equals(Param.IsPrescriptionAlarm))
                    {
                        Drug.IsPrescriptionAlarm = Param.IsPrescriptionAlarm;
                    }

                }

            }

        }

        /// <summary>
        /// 設定画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallSetting(DrugAlarm.Form.List View)
        {

            var form = new Setting
            {
                Owner = View
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 新規追加画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallDetailForm(DrugAlarm.Form.List View, bool IsNewDrug)
        {

            Int32 Index;

            if (IsNewDrug)
            {
                Index = -1;
            }
            else if (-1 < _SelectedIndex && _SelectedIndex < DrugList.Count)
            {
                Index = _SelectedIndex;
            }
            else
            {
                return;
            }

            var form = new Detail(Index)
            {
                Owner = View
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 薬削除
        /// </summary>
        public void DeleteDrug()
        {

            if (-1 < _SelectedIndex && _SelectedIndex < DrugList.Count)
            {

                string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string Message = DrugAlarm.Properties.Resources.List_DeleteMessage.Replace("_DRUG_", DrugList[_SelectedIndex].Name);

                if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    _Parameter.DrugList.RemoveAt(_SelectedIndex);
                    _Parameter.Save();
                    SelectedIndex = -1;
                }

            }

        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        public void DrugMedicine()
        {

            if (-1 < _SelectedIndex && _SelectedIndex < DrugList.Count)
            {

                Parameter.DrugParameter Drug = _Parameter.DrugList[_SelectedIndex];

                if (Drug.ToBeTaken.IsDrug)
                {
                    Drug.TotalVolume -= Drug.ToBeTaken.Volume;
                    _Parameter.Save();
                }
            }

        }

    }
}
