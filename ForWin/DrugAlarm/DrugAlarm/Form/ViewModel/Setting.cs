using System;
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
            base.CallPropertyChanged("IsEdited");
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

                if (!_Model.BreakfastStartHourIndex.Equals(value))
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
                if (!_Model.BreakfastStartMinuteIndex.Equals(value))
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
        /// 朝食：終了：時Indexプロパティ
        /// </summary>
        public Int32 BreakfastFinishHourIndex
        {
            get { return _Model.BreakfastFinishHourIndex; }
            set
            {
                if (!_Model.BreakfastFinishHourIndex.Equals(value))
                {

                    _Model.BreakfastFinishHourIndex = value;
                    CallPropertyChanged();

                }
            }
        }

        /// <summary>
        /// 朝食：終了：分プロパティ
        /// </summary>
        public ObservableCollection<string> BreakfastFinishMinute { get; set; }

        /// <summary>
        /// 朝食：終了：分Indexプロパティ
        /// </summary>
        public Int32 BreakfastFinishMinuteIndex
        {
            get { return _Model.BreakfastFinishMinuteIndex; }
            set
            {
                if (!_Model.BreakfastFinishMinuteIndex.Equals(value))
                {

                    _Model.BreakfastFinishMinuteIndex = value;
                    CallPropertyChanged();

                }
            }
        }

#endregion

        #region 昼食プロパティ

        /// <summary>
        /// 昼食：開始：時プロパティ
        /// </summary>
        public ObservableCollection<string> LunchStartHour { get; set; }

        /// <summary>
        /// 昼食：開始：時Indexプロパティ
        /// </summary>
        public Int32 LunchStartHourIndex
        {
            get { return _Model.LunchStartHourIndex; }
            set
            {
                if (!_Model.LunchStartHourIndex.Equals(value))
                {
                    _Model.LunchStartHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 昼食：開始：分プロパティ
        /// </summary>
        public ObservableCollection<string> LunchStartMinute { get; set; }

        /// <summary>
        /// 昼食：開始：分Indexプロパティ
        /// </summary>
        public Int32 LunchStartMinuteIndex
        {
            get { return _Model.LunchStartMinuteIndex; }
            set
            {
                if (!_Model.LunchStartMinuteIndex.Equals(value))
                {
                    _Model.LunchStartMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 昼食：終了：時プロパティ
        /// </summary>
        public ObservableCollection<string> LunchFinishHour { get; set; }

        /// <summary>
        /// 昼食：終了：時Indexプロパティ
        /// </summary>
        public Int32 LunchFinishHourIndex
        {
            get { return _Model.LunchFinishHourIndex; }
            set
            {
                if (!_Model.LunchFinishHourIndex.Equals(value))
                {
                    _Model.LunchFinishHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 昼食：終了：分プロパティ
        /// </summary>
        public ObservableCollection<string> LunchFinishMinute { get; set; }

        /// <summary>
        /// 昼食：終了：分Indexプロパティ
        /// </summary>
        public Int32 LunchFinishMinuteIndex
        {
            get { return _Model.LunchFinishMinuteIndex; }
            set
            {
                if (!_Model.LunchFinishMinuteIndex.Equals(value))
                {
                    _Model.LunchFinishMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 夕食プロパティ

        /// <summary>
        /// 夕食：開始：時プロパティ
        /// </summary>
        public ObservableCollection<string> DinnerStartHour { get; set; }

        /// <summary>
        /// 夕食：開始：時Indexプロパティ
        /// </summary>
        public Int32 DinnerStartHourIndex
        {
            get { return _Model.DinnerStartHourIndex; }
            set
            {
                if (!_Model.DinnerStartHourIndex.Equals(value))
                {
                    _Model.DinnerStartHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 夕食：開始：分プロパティ
        /// </summary>
        public ObservableCollection<string> DinnerStartMinute { get; set; }

        /// <summary>
        /// 夕食：開始：分Indexプロパティ
        /// </summary>
        public Int32 DinnerStartMinuteIndex
        {
            get { return _Model.DinnerStartMinuteIndex; }
            set
            {
                if (!_Model.DinnerStartMinuteIndex.Equals(value))
                {
                    _Model.DinnerStartMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 夕食：終了：時プロパティ
        /// </summary>
        public ObservableCollection<string> DinnerFinishHour { get; set; }

        /// <summary>
        /// 夕食：終了：時Indexプロパティ
        /// </summary>
        public Int32 DinnerFinishHourIndex
        {
            get { return _Model.DinnerFinishHourIndex; }
            set
            {
                if (!_Model.DinnerFinishHourIndex.Equals(value))
                {
                    _Model.DinnerFinishHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 夕食：終了：分プロパティ
        /// </summary>
        public ObservableCollection<string> DinnerFinishMinute { get; set; }

        /// <summary>
        /// 夕食：終了：分Indexプロパティ
        /// </summary>
        public Int32 DinnerFinishMinuteIndex
        {
            get { return _Model.DinnerFinishMinuteIndex; }
            set
            {
                if (!_Model.DinnerFinishMinuteIndex.Equals(value))
                {
                    _Model.DinnerFinishMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 就寝前プロパティ

        /// <summary>
        /// 就寝前：時プロパティ
        /// </summary>
        public ObservableCollection<string> SleepHour { get; set; }

        /// <summary>
        /// 就寝前：時Indexプロパティ
        /// </summary>
        public Int32 SleepHourIndex
        {
            get { return _Model.SleepHourIndex; }
            set
            {
                if (!_Model.SleepHourIndex.Equals(value))
                {
                    _Model.SleepHourIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 就寝前：分プロパティ
        /// </summary>
        public ObservableCollection<string> SleepMinute { get; set; }

        /// <summary>
        /// 就寝前：分Indexプロパティ
        /// </summary>
        public Int32 SleepMinuteIndex
        {
            get { return _Model.SleepMinuteIndex; }
            set
            {
                if (!_Model.SleepMinuteIndex.Equals(value))
                {
                    _Model.SleepMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region アラーム設定プロパティ

        /// <summary>
        /// アラーム設定：食前：プロパティ
        /// </summary>
        public ObservableCollection<string> BeforeMealMinute { get; set; }

        /// <summary>
        /// アラーム設定：食前：Indexプロパティ
        /// </summary>
        public Int32 BeforeMealMinuteIndex
        {
            get { return _Model.BeforeMealMinuteIndex; }
            set
            {
                if (!_Model.BeforeMealMinuteIndex.Equals(value))
                {
                    _Model.BeforeMealMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// アラーム設定：食後：プロパティ
        /// </summary>
        public ObservableCollection<string> AfterMealMinute { get; set; }

        /// <summary>
        /// アラーム設定：食後：Indexプロパティ
        /// </summary>
        public Int32 AfterMealMinuteIndex
        {
            get { return _Model.AfterMealMinuteIndex; }
            set
            {
                if (!_Model.AfterMealMinuteIndex.Equals(value))
                {
                    _Model.AfterMealMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// アラーム設定：就寝前：プロパティ
        /// </summary>
        public ObservableCollection<string> BeforeSleepMinute { get; set; }

        /// <summary>
        /// アラーム設定：就寝前：Indexプロパティ
        /// </summary>
        public Int32 BeforeSleepMinuteIndex
        {
            get { return _Model.BeforeSleepMinuteIndex; }
            set
            {
                if (!_Model.BeforeSleepMinuteIndex.Equals(value))
                {
                    _Model.BeforeSleepMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// アラーム設定：再通知：プロパティ
        /// </summary>
        public ObservableCollection<string> ReAlarmMinute { get; set; }

        /// <summary>
        /// アラーム設定：再通知：Indexプロパティ
        /// </summary>
        public Int32 ReAlarmMinuteIndex
        {
            get { return _Model.ReAlarmMinuteIndex; }
            set
            {
                if (!_Model.ReAlarmMinuteIndex.Equals(value))
                {
                    _Model.ReAlarmMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

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

            LunchStartHour = new ObservableCollection<string>();
            LunchStartMinute = new ObservableCollection<string>();
            LunchFinishHour = new ObservableCollection<string>();
            LunchFinishMinute = new ObservableCollection<string>();

            DinnerStartHour = new ObservableCollection<string>();
            DinnerStartMinute = new ObservableCollection<string>();
            DinnerFinishHour = new ObservableCollection<string>();
            DinnerFinishMinute = new ObservableCollection<string>();

            SleepHour = new ObservableCollection<string>();
            SleepMinute = new ObservableCollection<string>();

            BeforeMealMinute = new ObservableCollection<string>();
            AfterMealMinute = new ObservableCollection<string>();
            BeforeSleepMinute = new ObservableCollection<string>();
            ReAlarmMinute = new ObservableCollection<string>();

            _Model.GetHourList().ForEach(Hour =>
            {

                string Str = Hour.ToString("00");

                BreakfastStartHour.Add(Str);
                BreakfastFinishHour.Add(Str);

                LunchStartHour.Add(Str);
                LunchFinishHour.Add(Str);

                DinnerStartHour.Add(Str);
                DinnerFinishHour.Add(Str);

                SleepHour.Add(Str);

            });

            _Model.GetMinuteList().ForEach(Minute =>
            {

                string Str = Minute.ToString("00");

                BreakfastStartMinute.Add(Str);
                BreakfastFinishMinute.Add(Str);

                LunchStartMinute.Add(Str);
                LunchFinishMinute.Add(Str);

                DinnerStartMinute.Add(Str);
                DinnerFinishMinute.Add(Str);

                SleepMinute.Add(Str);

            });

            _Model.GetAlarmMinuteList().ForEach(Minute =>
            {

                string Str = Minute.ToString("00");

                BeforeMealMinute.Add(Str);
                AfterMealMinute.Add(Str);
                BeforeSleepMinute.Add(Str);
                ReAlarmMinute.Add(Str);

            });

            BreakfastStartHourIndex = _Model.GetBreakfastStartHourIndex();
            BreakfastStartMinuteIndex = _Model.GetBreakfastStartMinuteIndex();
            BreakfastFinishHourIndex = _Model.GetBreakfastFinishHourIndex();
            BreakfastFinishMinuteIndex = _Model.GetBreakfastFinishMinuteIndex();

            LunchStartHourIndex = _Model.GetLunchStartHourIndex();
            LunchStartMinuteIndex = _Model.GetLunchStartMinuteIndex();
            LunchFinishHourIndex = _Model.GetLunchFinishHourIndex();
            LunchFinishMinuteIndex = _Model.GetLunchFinishMinuteIndex();

            DinnerStartHourIndex = _Model.GetDinnerStartHourIndex();
            DinnerStartMinuteIndex = _Model.GetDinnerStartMinuteIndex();
            DinnerFinishHourIndex = _Model.GetDinnerFinishHourIndex();
            DinnerFinishMinuteIndex = _Model.GetDinnerFinishMinuteIndex();

            SleepHourIndex = _Model.GetSleepHourIndex();
            SleepMinuteIndex = _Model.GetSleepMinuteIndex();

            BeforeMealMinuteIndex = _Model.GetBeforeMealMinuteIndex();
            AfterMealMinuteIndex = _Model.GetAfterMealMinuteIndex();
            BeforeSleepMinuteIndex = _Model.GetBeforeSleepMinuteIndex();
            ReAlarmMinuteIndex = _Model.GetReAlarmMinuteIndex();

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            BreakfastStartHour.Clear();
            BreakfastStartMinute.Clear();
            BreakfastFinishHour.Clear();
            BreakfastFinishMinute.Clear();

            LunchStartHour.Clear();
            LunchStartMinute.Clear();
            LunchFinishHour.Clear();
            LunchFinishMinute.Clear();

            DinnerStartHour.Clear();
            DinnerStartMinute.Clear();
            DinnerFinishHour.Clear();
            DinnerFinishMinute.Clear();

            SleepHour.Clear();
            SleepMinute.Clear();

            BeforeMealMinute.Clear();
            AfterMealMinute.Clear();
            BeforeSleepMinute.Clear();
            ReAlarmMinute.Clear();

            BreakfastStartHour = null;
            BreakfastStartMinute = null;
            BreakfastFinishHour = null;
            BreakfastFinishMinute = null;

            LunchStartHour = null;
            LunchStartMinute = null;
            LunchFinishHour = null;
            LunchFinishMinute = null;

            DinnerStartHour = null;
            DinnerStartMinute = null;
            DinnerFinishHour = null;
            DinnerFinishMinute = null;

            SleepHour = null;
            SleepMinute = null;

            BeforeMealMinute = null;
            AfterMealMinute = null;
            BeforeSleepMinute = null;
            ReAlarmMinute = null;

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
