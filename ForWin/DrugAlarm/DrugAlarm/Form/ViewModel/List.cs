using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// List.xamlのViewModel
    /// </summary>
    class List : Common.ViewModelBase, IDisposable
    {

        /// <summary>
        /// List.xamlのModel
        /// </summary>
        private Model.List _Model;

        /// <summary>
        /// DrugParameterより一部抜粋
        /// </summary>
        public class DrugParameter : Common.ViewModelBase
        {

            /// <summary>
            /// List.xamlのModel
            /// </summary>
            private Model.List _Model;

            /// <summary>
            /// _Model.DrugList.Index
            /// </summary>
            private Int32 _Index;

            /// <summary>
            /// 名称プロパティ
            /// </summary>
            public string Name
            {
                get { return _Model.DrugList[_Index].Name; }
                set
                {
                    _Model.DrugList[_Index].Name = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// コロンプロパティ
            /// </summary>
            public string Colon
            {
                get { return _Model.DrugList[_Index].Colon; }
                set
                {
                    _Model.DrugList[_Index].Colon = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 服用タイミングプロパティ
            /// </summary>
            public string DrugTiming
            {
                get { return _Model.DrugList[_Index].DrugTiming; }
                set
                {
                    _Model.DrugList[_Index].DrugTiming = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 備考プロパティ
            /// </summary>
            public string Remarks
            {
                get { return _Model.DrugList[_Index].Remarks; }
                set
                {
                    _Model.DrugList[_Index].Remarks = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 薬切れFLGプロパティ
            /// </summary>
            public bool IsPrescriptionAlarm
            {
                get { return _Model.DrugList[_Index].IsPrescriptionAlarm; }
                set
                {
                    _Model.DrugList[_Index].IsPrescriptionAlarm = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// new
            /// </summary>
            /// <param name="Index">_Model.DrugList.Index</param>
            public DrugParameter(Model.List Model)
            {
                _Model = Model;
                _Index = _Model.AddDrugList();
            }

        }

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<DrugParameter> DrugList { get; set; }

        /// <summary>
        /// ListBox.SelectedIndexプロパティ
        /// </summary>
        public Int32 SelectedIndex
        {
            get
            {
                return _Model.SelectedIndex;
            }
            set
            {
                if (!_Model.SelectedIndex.Equals(value))
                {
                    _Model.SelectedIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 100msタイマ
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            _Model = new Model.List();
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

            _Model.Dispose();

        }

        /// <summary>
        /// タイマ処理
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (!DrugList.Count.Equals(_Model.GetDrugCount))
            {

                DrugList.Clear();
                _Model.ClearDrugList();

                for (Int32 iLoop = 0; iLoop < _Model.GetDrugCount; iLoop++)
                {

                    _Model.DrugIndex = iLoop;

                    DrugParameter AddDrug = new DrugParameter(_Model)
                    {
                        Name = _Model.DrugName,
                        Colon = ":",
                        DrugTiming = _Model.DrugTiming,
                        Remarks = _Model.DrugRemarks,
                        IsPrescriptionAlarm = _Model.DrugIsPrescriptionAlarm
                    };

                    DrugList.Add(AddDrug);

                }

            }
            else
            {

                for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                {

                    DrugParameter Drug = DrugList[iLoop];
                    _Model.DrugIndex = iLoop;

                    if (!Drug.Name.Equals(_Model.DrugName))
                    {
                        Drug.Name = _Model.DrugName;
                    }

                    if (!Drug.DrugTiming.Equals(_Model.DrugTiming))
                    {
                        Drug.DrugTiming = _Model.DrugTiming;
                    }

                    if (!Drug.Remarks.Equals(_Model.DrugRemarks))
                    {
                        Drug.Remarks = _Model.DrugRemarks;
                    }

                    if (!Drug.IsPrescriptionAlarm.Equals(_Model.DrugIsPrescriptionAlarm))
                    {
                        Drug.IsPrescriptionAlarm = _Model.DrugIsPrescriptionAlarm;
                    }

                }

            }

        }

        /// <summary>
        /// 設定画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallSetting(View.List View)
        {

            var form = new View.Setting
            {
                Owner = View
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 新規追加画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallDetailForm(View.List View, bool IsNewDrug)
        {

            Int32 Index;

            if (IsNewDrug)
            {
                Index = -1;
            }
            else if (-1 < _Model.SelectedIndex && _Model.SelectedIndex < DrugList.Count)
            {
                Index = _Model.SelectedIndex;
            }
            else
            {
                return;
            }

            var form = new View.Detail(Index)
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

            if (-1 < _Model.SelectedIndex && _Model.SelectedIndex < DrugList.Count)
            {

                _Model.DrugIndex = _Model.SelectedIndex;

                string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string Message = DrugAlarm.Properties.Resources.List_DeleteMessage.Replace("_DRUG_", _Model.DrugName);

                if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    _Model.DeleteDrug();
                }

            }

        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        public void DrugMedicine()
        {
            _Model.DrugMedicine();
        }

    }
}
