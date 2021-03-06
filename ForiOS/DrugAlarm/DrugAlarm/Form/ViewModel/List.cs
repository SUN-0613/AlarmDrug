﻿using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// List.ViewModel
    /// </summary>
    public class List : Common.ViewModelBase, IDisposable
    {

        #region コマンド

        /// <summary>
        /// 設定コマンド
        /// </summary>
        private Common.DelegateCommand _SettingCommand;

        /// <summary>
        /// 新規追加コマンド
        /// </summary>
        private Common.DelegateCommand _AddDrugCommand;

        /// <summary>
        /// 編集コマンド
        /// </summary>
        private Common.DelegateCommand<DrugParameter> _EditCommand;

        /// <summary>
        /// 削除コマンド
        /// </summary>
        private Common.DelegateCommand<DrugParameter> _DeleteCommand;

        /// <summary>
        /// 服用コマンド
        /// </summary>
        private Common.DelegateCommand<DrugParameter> _DrugMedicineCommand;

        /// <summary>
        /// アラーム時刻よりも先に服用コマンド
        /// </summary>
        private Common.DelegateCommand _TakeBeforeCommand;

        /// <summary>
        /// ListView.SelectedItemから呼び出されたか
        /// </summary>
        private bool _CallSelectedItem = true;

        /// <summary>
        /// 設定コマンドプロパティ
        /// </summary>
        /// <value>The setting command.</value>
        public Common.DelegateCommand SettingCommand
        {
            get
            {

                if (_SettingCommand == null)
                {
                    _SettingCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallSetting"); },
                        () => true);
                }

                return _SettingCommand;

            }
        }

        /// <summary>
        /// 新規追加コマンドプロパティ
        /// </summary>
        /// <value>The add drug command.</value>
        public Common.DelegateCommand AddDrugCommand
        {
            get
            {

                if (_AddDrugCommand == null)
                {
                    _AddDrugCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallAddDrug"); },
                        () => true);
                }

                return _AddDrugCommand;

            }
        }

        /// <summary>
        /// 編集コマンドプロパティ
        /// </summary>
        /// <value>The edit command.</value>
        public Common.DelegateCommand<DrugParameter> EditCommand
        {
            get
            {

                if (_EditCommand == null)
                {
                    _EditCommand = new Common.DelegateCommand<DrugParameter>(
                        (Item) => 
                        {

                            _CallSelectedItem = false;
                            SelectedItem = Item;
                            _CallSelectedItem = true;

                            if (_Model.IsOkSelectedIndex())
                                CallPropertyChanged("CallEditDrug");

                        },
                        () => true);
                }

                return _EditCommand;

            }
        }

        /// <summary>
        /// 削除コマンドプロパティ
        /// </summary>
        /// <value>The delete command.</value>
        public Common.DelegateCommand<DrugParameter> DeleteCommand
        {
            get
            {

                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new Common.DelegateCommand<DrugParameter>(
                        (Item) =>
                        {

                            _CallSelectedItem = false;
                            SelectedItem = Item;
                            _CallSelectedItem = true;

                            if (_Model.IsOkSelectedIndex())
                                CallPropertyChanged("CallDeleteDrug");

                        },
                        () => true);
                }

                return _DeleteCommand;

            }
        }

        /// <summary>
        /// 服用コマンドプロパティ
        /// </summary>
        /// <value>The drug medicine command.</value>
        public Common.DelegateCommand<DrugParameter> DrugMedicineCommand
        {
            get
            {

                if (_DrugMedicineCommand == null)
                {
                    _DrugMedicineCommand = new Common.DelegateCommand<DrugParameter>(
                        (Item) => 
                        {

                            _CallSelectedItem = false;
                            SelectedItem = Item;
                            _CallSelectedItem = true;

                            CallPropertyChanged("CallDrugMedicine"); 

                        },
                        () => true);
                }

                return _DrugMedicineCommand;

            }
        }

        /// <summary>
        /// アラーム時刻よりも先に服用コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand TakeBeforeCommand
        {
            get
            {

                if (_TakeBeforeCommand == null)
                {

                    _TakeBeforeCommand = new Common.DelegateCommand(
                        () => 
                        {
                            CallPropertyChanged("CallTakeBefore");
                        },
                        () => true);

                }

                return _TakeBeforeCommand;

            }
        }

        /// <summary>
        /// 薬削除メッセージ作成
        /// </summary>
        /// <returns>The delete drug message.</returns>
        public string MakeDeleteDrugMessage()
        {
            _Model.DrugIndex = _Model.SelectedIndex;
            return Resx.Resources.List_DeleteMessage.Replace("_DRUG_", _Model.DrugName);
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
        /// <returns>The drug medicine message.</returns>
        public string MakeDrugMedicineMessage()
        {
            _Model.DrugIndex = _Model.SelectedIndex;
            return Resx.Resources.List_DrugMedicineMessage.Replace("_DRUG_", _Model.DrugName);
        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        /// <returns><c>true</c>正常終了<c>false</c>異常終了</returns>
        public bool DrugMedicine()
        {
            return _Model.DrugMedicine();
        }

        /// <summary>
        /// 先に服用する時の確認メッセージ作成
        /// </summary>
        /// <returns>The take before message.</returns>
        public string MakeTakeBeforeMessage()
        {
            return Resx.Resources.List_TakeBeforeMessage;
        }

        /// <summary>
        /// アラームより先に薬を服用
        /// </summary>
        public void TakeBefore()
        {

            // AlarmTimer.csにて処理
            Class.UserControl.TakeBeforeAlarm = true;
        }

        #endregion

        #region 薬プロパティ

        /// <summary>
        /// DrugParameterより一部抜粋
        /// </summary>
        public class DrugParameter : Common.ViewModelBase
        {

            /// <summary>
            /// List.Model
            /// </summary>
            private Model.List _Model;

            /// <summary>
            /// Model.DrugList.Indexプロパティ
            /// </summary>
            /// <value>The index.</value>
            public Int32 Index { get; private set; }

            /// <summary>
            /// 名称プロパティ
            /// </summary>
            /// <value>The name.</value>
            public string Name
            {
                get { return _Model.DrugList[Index].Name; }
                set
                {
                    if (!_Model.DrugList[Index].Name.Equals(value))
                    {
                        _Model.DrugList[Index].Name = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// コロンプロパティ
            /// </summary>
            /// <value>The colon.</value>
            public string Colon
            {
                get { return _Model.DrugList[Index].Colon; }
                set
                {
                    if (!_Model.DrugList[Index].Colon.Equals(value))
                    {
                        _Model.DrugList[Index].Colon = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 服用タイミングプロパティ
            /// </summary>
            /// <value>The drug timing.</value>
            public string DrugTiming
            {
                get { return _Model.DrugList[Index].DrugTiming; }
                set
                {
                    if (!_Model.DrugList[Index].DrugTiming.Equals(value))
                    {
                        _Model.DrugList[Index].DrugTiming = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 備考プロパティ
            /// </summary>
            /// <value>The remarks.</value>
            public string Remarks
            {
                get { return _Model.DrugList[Index].Remarks; }
                set
                {
                    if (!_Model.DrugList[Index].Remarks.Equals(value))
                    {
                        _Model.DrugList[Index].Remarks = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 薬切れFLGプロパティ
            /// </summary>
            /// <value><c>true</c> if is prescription alarm; otherwise, <c>false</c>.</value>
            public bool IsPrescriptionAlarm
            {
                get { return _Model.DrugList[Index].IsPrescriptionAlarm; }
                set
                {
                    if (!_Model.DrugList[Index].IsPrescriptionAlarm.Equals(value))
                    {
                        _Model.DrugList[Index].IsPrescriptionAlarm = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 頓服服用プロパティ
            /// </summary>
            /// <value><c>true</c> if is to be taken; otherwise, <c>false</c>.</value>
            public bool IsToBeTaken
            {
                get { return _Model.DrugList[Index].IsToBeTaken; }
                set
                {
                    if (!_Model.DrugList[Index].IsToBeTaken.Equals(value))
                    {
                        _Model.DrugList[Index].IsToBeTaken = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// new
            /// </summary>
            /// <param name="Model">Model.</param>
            public DrugParameter(Model.List Model)
            {
                _Model = Model;
                Index = _Model.AddDrugList();
            }

        }

        /// <summary>
        /// 薬一覧
        /// </summary>
        /// <value>The drug list.</value>
        public ObservableCollection<DrugParameter> DrugList { get; set; }

        /// <summary>
        /// ListView.SelectedItem
        /// </summary>
        /// <value>The selected item.</value>
        public DrugParameter SelectedItem
        {
            get 
            {
                return (-1 < _Model.SelectedIndex && _Model.SelectedIndex < DrugList.Count) ? DrugList[_Model.SelectedIndex] : null;
            }
            set
            {

                _Model.SelectedIndex = value != null ? value.Index : -1;
                CallPropertyChanged();

                if (_CallSelectedItem && _Model.IsOkSelectedIndex())
                    CallPropertyChanged("CallEditDrug");

            }
        }

        /// <summary>
        /// ListView.SelectedIndex
        /// </summary>
        /// <value>The index of the selected.</value>
        public Int32 SelectedIndex
        {
            get { return _Model.SelectedIndex; }
        }

        #endregion

        #region Model

        /// <summary>
        /// Model
        /// </summary>
        private Model.List _Model;

        /// <summary>
        /// タイマ処理
        /// </summary>
        public void ShowList()
        {

            try
            {

                NextAlarm = _Model.GetNextAlarmMessage();

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
                            IsPrescriptionAlarm = _Model.DrugIsPrescriptionAlarm,
                            IsToBeTaken = _Model.DrugIsToBeTaken
                        };

                        DrugList.Add(AddDrug);

                    }

                }
                else
                {

                    bool IsPropertyChanged = false;

                    for (Int32 iLoop = 0; iLoop < DrugList.Count; iLoop++)
                    {

                        _Model.DrugIndex = iLoop;

                        if (!DrugList[iLoop].Name.Equals(_Model.DrugName))
                        {
                            DrugList[iLoop].Name = _Model.DrugName;
                            IsPropertyChanged = true;
                        }

                        if (!DrugList[iLoop].DrugTiming.Equals(_Model.DrugTiming))
                        {
                            DrugList[iLoop].DrugTiming = _Model.DrugTiming;
                            IsPropertyChanged = true;
                        }

                        if (!DrugList[iLoop].Remarks.Equals(_Model.DrugRemarks))
                        {
                            DrugList[iLoop].Remarks = _Model.DrugRemarks;
                            IsPropertyChanged = true;
                        }

                        if (!DrugList[iLoop].IsPrescriptionAlarm.Equals(_Model.DrugIsPrescriptionAlarm))
                        {
                            DrugList[iLoop].IsPrescriptionAlarm = _Model.DrugIsPrescriptionAlarm;
                            IsPropertyChanged = true;
                        }

                        if (!DrugList[iLoop].IsToBeTaken.Equals(_Model.DrugIsToBeTaken))
                        {
                            DrugList[iLoop].IsToBeTaken = _Model.DrugIsToBeTaken;
                            IsPropertyChanged = true;
                        }

                    }

                    if (IsPropertyChanged)
                    {
                        CallPropertyChanged(nameof(DrugList));
                    }

                }

            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
            }

        }

        #endregion

        /// <summary>
        /// 次回アラーム時間
        /// </summary>
        private string _NextAlarm = "";

        /// <summary>
        /// 次回アラーム時間プロパティ
        /// </summary>
        /// <value>The next alarm.</value>
        public string NextAlarm
        {
            get { return _NextAlarm; }
            set
            {
                if (!_NextAlarm.Equals(value))
                {
                    _NextAlarm = value;
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

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.List"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.List"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.List"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.List"/> was occupying.</remarks>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}
