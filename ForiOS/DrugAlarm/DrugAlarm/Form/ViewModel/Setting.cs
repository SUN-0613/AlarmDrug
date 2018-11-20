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
        /// 時プロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The breakfast start hour.</value>
        public ObservableCollection<string> BreakfastStartHour { get; set; }

        /// <summary>
        /// 時Indexプロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The index of the breakfast start hour.</value>
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
        /// 分プロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The breakfast start minute.</value>
        public ObservableCollection<string> BreakfastStartMinute { get; set; }

        /// <summary>
        /// 分Indexプロパティ
        /// 朝食
        /// 開始
        /// </summary>
        /// <value>The index of the breakfast start minute.</value>
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
        /// 時プロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The breakfast finish hour.</value>
        public ObservableCollection<string> BreakfastFinishHour { get; set; }

        /// <summary>
        /// 時Indexプロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The index of the breakfast finish hour.</value>
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
        /// 分プロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The breakfast finish minute.</value>
        public ObservableCollection<string> BreakfastFinishMinute { get; set; }

        /// <summary>
        /// 分Indexプロパティ
        /// 朝食
        /// 終了
        /// </summary>
        /// <value>The index of the breakfast finish minute.</value>
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

        #region 昼食

        /// <summary>         /// 時プロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The Lunch start hour.</value>         public ObservableCollection<string> LunchStartHour { get; set; }

        /// <summary>         /// 時Indexプロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The index of the Lunch start hour.</value>         public Int32 LunchStartHourIndex         {             get { return _Model.LunchStartHourIndex; }             set             {                 if (!_Model.LunchStartHourIndex.Equals(value))                 {                     _Model.LunchStartHourIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 分プロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The Lunch start minute.</value>         public ObservableCollection<string> LunchStartMinute { get; set; }

        /// <summary>         /// 分Indexプロパティ         /// 昼食         /// 開始         /// </summary>         /// <value>The index of the Lunch start minute.</value>         public Int32 LunchStartMinuteIndex         {             get { return _Model.LunchStartMinuteIndex; }             set             {                 if (!_Model.LunchStartMinuteIndex.Equals(value))                 {                     _Model.LunchStartMinuteIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 時プロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The Lunch finish hour.</value>         public ObservableCollection<string> LunchFinishHour { get; set; }

        /// <summary>         /// 時Indexプロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The index of the Lunch finish hour.</value>         public Int32 LunchFinishHourIndex         {             get { return _Model.LunchFinishHourIndex; }             set             {                 if (!_Model.LunchFinishHourIndex.Equals(value))                 {                     _Model.LunchFinishHourIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 分プロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The Lunch finish minute.</value>         public ObservableCollection<string> LunchFinishMinute { get; set; }

        /// <summary>         /// 分Indexプロパティ         /// 昼食         /// 終了         /// </summary>         /// <value>The index of the Lunch finish minute.</value>         public Int32 LunchFinishMinuteIndex         {             get { return _Model.LunchFinishMinuteIndex; }             set             {                 if (!_Model.LunchFinishMinuteIndex.Equals(value))                 {                     _Model.LunchFinishMinuteIndex = value;                     CallPropertyChanged();                 }             }         }

        #endregion

        #region 夕食

        /// <summary>         /// 時プロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The Dinner start hour.</value>         public ObservableCollection<string> DinnerStartHour { get; set; }

        /// <summary>         /// 時Indexプロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The index of the Dinner start hour.</value>         public Int32 DinnerStartHourIndex         {             get { return _Model.DinnerStartHourIndex; }             set             {                 if (!_Model.DinnerStartHourIndex.Equals(value))                 {                     _Model.DinnerStartHourIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 分プロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The Dinner start minute.</value>         public ObservableCollection<string> DinnerStartMinute { get; set; }

        /// <summary>         /// 分Indexプロパティ         /// 夕食         /// 開始         /// </summary>         /// <value>The index of the Dinner start minute.</value>         public Int32 DinnerStartMinuteIndex         {             get { return _Model.DinnerStartMinuteIndex; }             set             {                 if (!_Model.DinnerStartMinuteIndex.Equals(value))                 {                     _Model.DinnerStartMinuteIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 時プロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The Dinner finish hour.</value>         public ObservableCollection<string> DinnerFinishHour { get; set; }

        /// <summary>         /// 時Indexプロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The index of the Dinner finish hour.</value>         public Int32 DinnerFinishHourIndex         {             get { return _Model.DinnerFinishHourIndex; }             set             {                 if (!_Model.DinnerFinishHourIndex.Equals(value))                 {                     _Model.DinnerFinishHourIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 分プロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The Dinner finish minute.</value>         public ObservableCollection<string> DinnerFinishMinute { get; set; }

        /// <summary>         /// 分Indexプロパティ         /// 夕食         /// 終了         /// </summary>         /// <value>The index of the Dinner finish minute.</value>         public Int32 DinnerFinishMinuteIndex         {             get { return _Model.DinnerFinishMinuteIndex; }             set             {                 if (!_Model.DinnerFinishMinuteIndex.Equals(value))                 {                     _Model.DinnerFinishMinuteIndex = value;                     CallPropertyChanged();                 }             }         }

        #endregion

        #region 就寝前

        /// <summary>         /// 時プロパティ         /// 就寝前         /// </summary>         /// <value>The Sleep  hour.</value>         public ObservableCollection<string> SleepHour { get; set; }

        /// <summary>         /// 時Indexプロパティ         /// 就寝前         /// </summary>         /// <value>The index of the Sleep  hour.</value>         public Int32 SleepHourIndex         {             get { return _Model.SleepHourIndex; }             set             {                 if (!_Model.SleepHourIndex.Equals(value))                 {                     _Model.SleepHourIndex = value;                     CallPropertyChanged();                 }             }         }

        /// <summary>         /// 分プロパティ         /// 就寝前         /// </summary>         /// <value>The Sleep  minute.</value>         public ObservableCollection<string> SleepMinute { get; set; }

        /// <summary>         /// 分Indexプロパティ         /// 就寝前         /// </summary>         /// <value>The index of the Sleep  minute.</value>         public Int32 SleepMinuteIndex         {             get { return _Model.SleepMinuteIndex; }             set             {                 if (!_Model.SleepMinuteIndex.Equals(value))                 {                     _Model.SleepMinuteIndex = value;                     CallPropertyChanged();                 }             }         } 
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

        #endregion

        #endregion

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
            RealarmMinute = new ObservableCollection<string>();

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
                RealarmMinute.Add(Str);

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
            RealarmMinuteIndex = _Model.GetRealarmMinuteIndex();

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
            RealarmMinute.Clear();

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
            RealarmMinute = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}
