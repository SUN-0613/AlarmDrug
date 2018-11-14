using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Detail.ViewModel
    /// </summary>
    public class Detail : Common.ViewModelBase
    {

        #region 基底

        private Model.Detail _Model;

        /// <summary>
        /// 編集したか
        /// </summary>
        /// <value><c>true</c> if is edited; otherwise, <c>false</c>.</value>
        public bool IsEdited
        {
            get { return _Model.IsEdited; }
            set { _Model.IsEdited = value; }
        }

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="PropertyName">Property name.</param>
        /// <param name="StackFrameIndex">Stack frame index.</param>
        protected override void CallPropertyChanged(string PropertyName = "", int StackFrameIndex = 1)
        {

            base.CallPropertyChanged(PropertyName, StackFrameIndex + 1);

            IsEdited = true;
            base.CallPropertyChanged("IsEdited");

        }

        #endregion

        #region コマンド

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        /// <value>The cancel command.</value>
        public Common.DelegateCommand CancelCommand
        {
            get
            {

                if (_Model.CancelCommand == null)
                {
                    _Model.CancelCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallCancel"); },
                        () => true);
                }

                return _Model.CancelCommand;

            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _Model.Initialize();
        }

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        /// <value>The save command.</value>
        public Common.DelegateCommand SaveCommand
        {
            get
            {

                if (_Model.SaveCommand == null)
                {
                    _Model.SaveCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallSave"); },
                        ()=>true);
                }

                return _Model.SaveCommand;

            }
        }

        /// <summary>
        /// 保存確認メッセージ作成
        /// </summary>
        /// <returns>The save message.</returns>
        public string MakeSaveMessage()
        {
            return Resx.Resources.Detail_SaveMessage.Replace("_DRUG_", Name);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Model.Save();
        }

        #endregion

        #region Const

        /// <summary>
        /// 処方量最大値
        /// </summary>
        private const Int32 MaxTotalVolume = 3000;

        /// <summary>
        /// お知らせ錠数最大値
        /// </summary>
        private const Int32 MaxAlarmVolume = 100;

        /// <summary>
        /// 処方量入力オーバーメッセージ作成
        /// </summary>
        /// <returns>The total volume over message.</returns>
        public string MakeTotalVolumeOverMessage()
        {
            return Resx.Resources.Detail_TotalVolumeOverMessage.Replace("_TOTALVOLUME_", MaxTotalVolume.ToString());
        }

        /// <summary>
        /// 薬残量お知らせ入力オーバーメッセージ作成
        /// </summary>
        /// <returns>The alarm volume over message.</returns>
        public string MakeAlarmVolumeOverMessage()
        {
            return Resx.Resources.Detail_AlarmVolumeOverMessage.Replace("_ALARMVOLUME_", MaxAlarmVolume.ToString());
        }

        #endregion

        #region 薬プロパティ

        /// <summary>
        /// 薬名称プロパティ
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _Model.Drug.Name; }
            set
            {
                _Model.Drug.Name = value;
                CallPropertyChanged();
            }
        }

        #endregion

        public Detail()
        {
        }
    }
}
