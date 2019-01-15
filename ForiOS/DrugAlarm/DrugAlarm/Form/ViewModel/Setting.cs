using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Setting.ViewModel
    /// </summary>
    public class Setting : Common.ViewModelBase, IDisposable
    {

        #region 基底

        /// <summary>
        /// Setting.Model
        /// </summary>
        private Model.Setting _Model;

        /// <summary>
        /// 編集したか
        /// </summary>
        /// <value><c>true</c> if is edited; otherwise, <c>false</c>.</value>
        public bool IsEdited
        {
            get { return _Model.IsEdited; }
            set
            {
                _Model.IsEdited = value;
                CallPropertyChanged("IsEdited");
            }
        }

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="PropertyName">Property name.</param>
        /// <param name="StackFrameIndex">Stack frame index.</param>
        protected override void CallPropertyChanged(string PropertyName = "", int StackFrameIndex = 1)
        {

            base.CallPropertyChanged(PropertyName, StackFrameIndex + 1);

            if (!PropertyName.Equals(nameof(IsEdited)) && !(PropertyName.Length > 4 && PropertyName.Substring(0, 4).ToUpper().Equals("CALL")))
                IsEdited = true;

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

        #endregion

        #region BindingProperty

        #region 朝食

        /// <summary>
        /// 時間プロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The breakfast start.</value>
        public TimeSpan BreakfastStart
        {
            get { return _Model.BreakfastStart; }
            set
            {
                if (!_Model.BreakfastStart.Equals(value))
                {

                    _Model.BreakfastStart = value;
                    CallPropertyChanged();

                    BreakfastFinish = CompareToStartTime(BreakfastStart, BreakfastFinish);

                }
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The breakfast finish.</value>
        public TimeSpan BreakfastFinish
        {
            get { return _Model.BreakfastFinish; }
            set
            {
                if (!_Model.BreakfastFinish.Equals(value))
                {

                    _Model.BreakfastFinish = value;
                    CallPropertyChanged();

                    BreakfastStart = CompareToFinishTime(BreakfastStart, BreakfastFinish);

                }
            }
        }

        #endregion

        #region 昼食

        /// <summary>
        /// 時間プロパティ
        /// 昼食
        /// 開始
        /// </summary>
        /// <value>The lunch start.</value>
        public TimeSpan LunchStart
        {
            get { return _Model.LunchStart; }
            set
            {
                if (!_Model.LunchStart.Equals(value))
                {

                    _Model.LunchStart = value;
                    CallPropertyChanged();

                    LunchFinish = CompareToStartTime(LunchStart, LunchFinish);

                }
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 昼食
        /// 終了
        /// </summary>
        /// <value>The lunch finish.</value>
        public TimeSpan LunchFinish
        {
            get { return _Model.LunchFinish; }
            set
            {
                if (!_Model.LunchFinish.Equals(value))
                {

                    _Model.LunchFinish = value;
                    CallPropertyChanged();

                    LunchStart = CompareToFinishTime(LunchStart, LunchFinish);

                }
            }
        }

        #endregion

        #region 夕食

        /// <summary>
        /// 時間プロパティ
        /// 夕食
        /// 開始
        /// </summary>
        /// <value>The dinner start.</value>
        public TimeSpan DinnerStart
        {
            get { return _Model.DinnerStart; }
            set
            {
                if (!_Model.DinnerStart.Equals(value))
                {

                    _Model.DinnerStart = value;
                    CallPropertyChanged();

                    DinnerFinish = CompareToStartTime(DinnerStart, DinnerFinish);

                }
            }
        }

        /// <summary>
        /// 時間プロパティ
        /// 夕食
        /// 終了
        /// </summary>
        /// <value>The dinner finish.</value>
        public TimeSpan DinnerFinish
        {
            get { return _Model.DinnerFinish; }
            set
            {
                if (!_Model.DinnerFinish.Equals(value))
                {

                    _Model.DinnerFinish = value;
                    CallPropertyChanged();

                    DinnerStart = CompareToFinishTime(DinnerStart, DinnerFinish);

                }
            }
        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 時間プロパティ
        /// 就寝
        /// 開始
        /// </summary>
        /// <value>The sleep start.</value>
        public TimeSpan SleepStart
        {
            get { return _Model.SleepStart; }
            set
            {
                if(!_Model.SleepStart.Equals(value))
                {
                    _Model.SleepStart = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region アラーム設定

        /// <summary>
        /// アラーム設定プロパティ
        /// 食前
        /// </summary>
        /// <value>The before meal minute.</value>
        public ObservableCollection<string> BeforeMealMinute { get; set; }

        /// <summary>
        /// アラーム設定Indexプロパティ
        /// 食前
        /// </summary>
        /// <value>The index of the before meal minute.</value>
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
        /// アラーム設定プロパティ
        /// 食後
        /// </summary>
        /// <value>The after meal minute.</value>
        public ObservableCollection<string> AfterMealMinute { get; set; }

        /// <summary>
        /// アラーム設定Indexプロパティ
        /// 食後
        /// </summary>
        /// <value>The index of the after meal minute.</value>
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
        /// アラーム設定プロパティ
        /// 就寝前
        /// </summary>
        /// <value>The before sleep minute.</value>
        public ObservableCollection<string> BeforeSleepMinute { get; set; }

        /// <summary>
        /// アラーム設定Indexプロパティ
        /// 就寝前
        /// </summary>
        /// <value>The index of the before sleep minute.</value>
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
        /// アラーム設定プロパティ
        /// 再通知
        /// </summary>
        /// <value>The realarm minute.</value>
        public ObservableCollection<string> RealarmMinute { get; set; }

        /// <summary>
        /// アラーム設定Indexプロパティ
        /// 再通知
        /// </summary>
        /// <value>The index of the realarm minute.</value>
        public Int32 RealarmMinuteIndex
        {
            get { return _Model.RealarmMinuteIndex; }
            set 
            {
                if (!_Model.RealarmMinuteIndex.Equals(value))
                {
                    _Model.RealarmMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 前回アラーム日付
        /// </summary>
        private DateTime _SelectBeforeAlarmDate;

        /// <summary>
        /// 前回アラーム時間
        /// </summary>
        private TimeSpan _SelectBeforeAlarmTime;

        /// <summary>
        /// 前回アラーム日付プロパティ
        /// </summary>
        /// <value>The before alarm date.</value>
        public DateTime BeforeAlarmDate
        {
            get { return _Model.BeforeAlarm; }
            set
            {
                if (!(_Model.BeforeAlarm.Year.Equals(value.Year)
                    && _Model.BeforeAlarm.Month.Equals(value.Month)
                    && _Model.BeforeAlarm.Day.Equals(value.Day)))
                {
                    _SelectBeforeAlarmDate = value;
                    SetBeforeAlarm();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 前回アラーム時間プロパティ
        /// </summary>
        /// <value>The before alarm time.</value>
        public TimeSpan BeforeAlarmTime
        {
            get { return _Model.BeforeAlarm.TimeOfDay; }
            set
            {
                if (!(_Model.BeforeAlarm.Hour.Equals(value.Hours)
                    && _Model.BeforeAlarm.Minute.Equals(value.Minutes)))
                {
                    _SelectBeforeAlarmTime = value;
                    SetBeforeAlarm();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 前回アラームの設定
        /// </summary>
        private void SetBeforeAlarm()
        {

            _Model.BeforeAlarm = new Class.Method().ConvertToDateTime(
                                                    _SelectBeforeAlarmDate.Year
                                                    , _SelectBeforeAlarmDate.Month
                                                    , _SelectBeforeAlarmDate.Day
                                                    , _SelectBeforeAlarmTime.Hours
                                                    , _SelectBeforeAlarmTime.Minutes
                                                    , _Model.BeforeAlarm);

        }

        #endregion

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            _Model = new Model.Setting();

            BeforeMealMinute = new ObservableCollection<string>();
            AfterMealMinute = new ObservableCollection<string>();
            BeforeSleepMinute = new ObservableCollection<string>();
            RealarmMinute = new ObservableCollection<string>();

            _Model.GetAlarmMinuteList().ForEach(Minute => 
            {
                string Str = Minute.ToString("00");

                BeforeMealMinute.Add(Str);
                AfterMealMinute.Add(Str);
                BeforeSleepMinute.Add(Str);
                RealarmMinute.Add(Str);

            });

            BeforeMealMinuteIndex = _Model.GetBeforeMealMinuteIndex();
            AfterMealMinuteIndex = _Model.GetAfterMealMinuteIndex();
            BeforeSleepMinuteIndex = _Model.GetBeforeSleepMinuteIndex();
            RealarmMinuteIndex = _Model.GetRealarmMinuteIndex();

            _SelectBeforeAlarmDate = _Model.BeforeAlarm;
            _SelectBeforeAlarmTime = _Model.BeforeAlarm.TimeOfDay;

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.Setting"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.Setting"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Setting"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Setting"/> was occupying.</remarks>
        public void Dispose()
        {

            BeforeMealMinute.Clear();
            AfterMealMinute.Clear();
            BeforeSleepMinute.Clear();
            RealarmMinute.Clear();

            BeforeMealMinute = null;
            AfterMealMinute = null;
            BeforeSleepMinute = null;
            RealarmMinute = null;

            _Model.Dispose();
            _Model = null;

        }

        /// <summary>
        /// 開始時間変更による終了時間の調整
        /// </summary>
        /// <returns>調整後の終了時間</returns>
        /// <param name="startTime">開始時間</param>
        /// <param name="finishTime">終了時間</param>
        private TimeSpan CompareToStartTime(TimeSpan startTime, TimeSpan finishTime)
        {

            if (startTime >= finishTime)
            {

                if (startTime >= new TimeSpan(23, 55, 0))
                {
                    return new TimeSpan(23, 59, 0);
                }
                else
                {
                    return startTime + new TimeSpan(0, 5, 0);
                }

            }
            else
            {
                return finishTime;
            }

        }

        /// <summary>
        /// 終了時間変更による開始時間の調整
        /// </summary>
        /// <returns>調整後の開始時間</returns>
        /// <param name="startTime">開始時間</param>
        /// <param name="finishTime">終了時間</param>
        private TimeSpan CompareToFinishTime(TimeSpan startTime, TimeSpan finishTime)
        {

            if (startTime >= finishTime)
            {

                if (finishTime <= new TimeSpan(0, 5, 0))
                {
                    return new TimeSpan(0, 0, 0);
                }
                else
                {
                    return finishTime - new TimeSpan(0, 5, 0);
                }

            }
            else
            {
                return startTime;
            }

        }

        /// <summary>
        /// 前回アラーム日時の更新
        /// </summary>
        public void UpdateBeforeAlarmDateTime()
        {

            DateTime beforeAlarm = _Model.GetBeforeAlarm();

            if (!_SelectBeforeAlarmDate.Equals(beforeAlarm)
                || !_SelectBeforeAlarmTime.Equals(beforeAlarm.TimeOfDay))
            {

                BeforeAlarmDate = beforeAlarm;
                BeforeAlarmTime = beforeAlarm.TimeOfDay;

            }

        }

    }
}
