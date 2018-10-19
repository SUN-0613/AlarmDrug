using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{
    /// <summary>
    /// Setting.ViewModel
    /// </summary>
    class Setting : Common.ViewModelBase, IDisposable
    {

        #region パラメータ

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="PropertyName">Changedイベントを発生させたいプロパティ名</param>
        protected override void CallPropertyChanged(string PropertyName = "")
        {
            base.CallPropertyChanged(PropertyName);
            IsEdited = true;    //編集FLGの更新
        }

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited
        {
            get
            {
                return _Model.IsEdited;
            }
            set
            {
                _Model.IsEdited = value;
                CallPropertyChanged();
            }
        }

        #endregion

        #region 朝食プロパティ

        /// <summary>
        /// 朝食：開始：時プロパティ
        /// </summary>
        public ObservableCollection<string> BreakfastStartHour { get; set; }

        /// <summary>
        /// 朝食：開始：時Indexプロパティ
        /// </summary>
        public Int32 BreakfastStartHourIndex
        {
            get { return _Model.BreakfastStartHourIndex; }
            set
            {
                if (!_Model.BreakfastStartHourIndex.Equals(-1))
                {
                    _Model.BreakfastStartHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 朝食：開始：分プロパティ
        /// </summary>
        public ObservableCollection<string> BreakfastStartMinute { get; set; }

        /// <summary>
        /// 朝食：開始：分Indexプロパティ
        /// </summary>
        public Int32 BreakfastStartMinuteIndex
        {
            get { return _Model.BreakfastStartMinuteIndex; }
            set
            {
                if (!_Model.BreakfastStartMinuteIndex.Equals(-1))
                {
                    _Model.BreakfastStartMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 朝食：終了：時プロパティ
        /// </summary>
        public ObservableCollection<string> BreakfastFinishHour { get; set; }

        /// <summary>
        /// 朝食：終了：分プロパティ
        /// </summary>
        public ObservableCollection<string> BreakfastFinishMinute { get; set; }

        #endregion

        /// <summary>
        /// Setting.Model
        /// </summary>
        private Model.Setting _Model;

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {
            _Model = new Model.Setting();

            BreakfastStartHour = new ObservableCollection<string>();
            BreakfastStartMinute = new ObservableCollection<string>();
            BreakfastFinishHour = new ObservableCollection<string>();
            BreakfastFinishMinute = new ObservableCollection<string>();

            _Model.GetHourList().ForEach(Hour =>
            {

                string Str = Hour.ToString("00");

                BreakfastStartHour.Add(Str);
                BreakfastFinishHour.Add(Str);

            });

            _Model.GetMinuteList().ForEach(Minute =>
            {

                string Str = Minute.ToString("00");

                BreakfastStartMinute.Add(Str);
                BreakfastFinishMinute.Add(Str);

            });

            BreakfastStartHourIndex = _Model.GetBreakfastStartHourIndex();
            BreakfastStartMinuteIndex = _Model.GetBreakfastStartMinuteIndex();


            IsEdited = false;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _Model.Dispose();
            _Model = null;
        }

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
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
        public Common.DelegateCommand SaveCommand
        {
            get
            {

                if (_Model.SaveCommand == null)
                {
                    _Model.SaveCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallSave"); },
                        () => true);
                }

                return _Model.SaveCommand;

            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Model.Save();
        }

    }
}
