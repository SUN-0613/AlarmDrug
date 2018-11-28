﻿using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Alarm.ViewModel
    /// </summary>
    public class Alarm : Common.ViewModelBase, IDisposable
    {

        #region 基底

        /// <summary>
        /// Alarm.Model
        /// </summary>
        private Model.Alarm _Model;

        #endregion

        #region コマンド

        /// <summary>
        /// OKコマンドプロパティ
        /// </summary>
        /// <value>The ok command.</value>
        public Common.DelegateCommand OkCommand
        {
            get
            {
                if (_Model.OkCommand == null)
                {
                    _Model.OkCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallOK"); }, 
                        () => true);
                }

                return _Model.OkCommand;

            }
        }

        /// <summary>
        /// 薬残量計算
        /// </summary>
        /// <returns><c>true</c>, 薬切れ有, <c>false</c> 在庫十分</returns>
        public bool TakeMedicine()
        {
            return _Model.TakeMedicine();
        }

        /// <summary>
        /// 再通知コマンドプロパティ
        /// </summary>
        /// <value>The realarm command.</value>
        public Common.DelegateCommand RealarmCommand
        {
            get
            {

                if (_Model.RealarmCommand == null)
                {
                    _Model.RealarmCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallRealarm"); },
                        () => true);
                }

                return _Model.RealarmCommand;

            }
        }

        #endregion

        #region BindingProperty

        /// <summary>
        /// 表示薬一覧プロパティ
        /// </summary>
        /// <value>The drug list.</value>
        public ObservableCollection<string> DrugList { get; set; }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {

            _Model = new Model.Alarm();

            DrugList = new ObservableCollection<string>();

            _Model.GetDrugList().ForEach(Name => 
            {
                DrugList.Add(Name);
            });

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Alarm"/> was occupying.</remarks>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}