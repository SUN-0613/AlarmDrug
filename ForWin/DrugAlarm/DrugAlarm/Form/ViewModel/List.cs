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
        /// new
        /// </summary>
        public List()
        {

            _Model = new Model.List();
            DrugList = new ObservableCollection<DrugParameter>();

            //タイマ処理
            _Model.Timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            _Model.Timer.Tick += new EventHandler(Timer_Tick);
            _Model.Timer.IsEnabled = true;
            _Model.Timer.Start();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            //タイマ終了
            if (_Model.Timer.IsEnabled)
            {
                _Model.Timer.IsEnabled = false;
                _Model.Timer.Stop();
            }
            _Model.Timer.Tick -= new EventHandler(Timer_Tick);

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
        /// 薬削除メッセージ作成
        /// </summary>
        public string MakeDeleteDrugMessage()
        {
            _Model.DrugIndex = SelectedIndex;
            return DrugAlarm.Properties.Resources.List_DeleteMessage.Replace("_DRUG_", _Model.DrugName);
        }

        /// <summary>
        /// 選択している薬の削除
        /// </summary>
        public void DeleteDrug()
        {
            _Model.DeleteDrug();
        }

        /// <summary>
        /// 服用メッセージ作成
        /// </summary>
        /// <returns></returns>
        public string MakeDrugMedicineMessage()
        {
            _Model.DrugIndex = SelectedIndex;
            return DrugAlarm.Properties.Resources.List_DrugMedicineMessage.Replace("_DRUG_", _Model.DrugName);
        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        public void DrugMedicine()
        {
            _Model.DrugMedicine();
        }

        /// <summary>
        /// 設定コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SettingCommand
        {
            get
            {

                if (_Model.SettingCommand == null)
                {
                    _Model.SettingCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallSetting"); },
                        () => true);
                }

                return _Model.SettingCommand;

            }
        }

        /// <summary>
        /// 新規追加コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand AddDrugCommand
        {
            get
            {

                if (_Model.AddDrugCommand == null)
                {
                    _Model.AddDrugCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallAddDrug"); },
                        () => true);
                }

                return _Model.AddDrugCommand;

            }
        }

        /// <summary>
        /// 編集コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand EditCommand
        {
            get
            {

                if (_Model.EditCommand == null)
                {
                    _Model.EditCommand = new Common.DelegateCommand(
                        () => {
                            if (-1 < _Model.SelectedIndex && _Model.SelectedIndex < DrugList.Count)
                                CallPropertyChanged("CallEditDrug");
                        },
                        () => true);
                }

                return _Model.EditCommand;

            }
        }

        /// <summary>
        /// 削除コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand DeleteCommand
        {
            get
            {

                if (_Model.DeleteCommand == null)
                {
                    _Model.DeleteCommand = new Common.DelegateCommand(
                        () => {
                            if (-1 < _Model.SelectedIndex && _Model.SelectedIndex < DrugList.Count)
                                CallPropertyChanged("CallDeleteDrug");
                        },
                        () => true);
                }

                return _Model.DeleteCommand;

            }
        }

        /// <summary>
        /// 服用コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand DrugMedicineCommand
        {
            get
            {

                if (_Model.DrugMedicineCommand == null)
                {
                    _Model.DrugMedicineCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallDrugMedicine"); },
                        () => true);
                }

                return _Model.DrugMedicineCommand;

            }
        }

    }
}
