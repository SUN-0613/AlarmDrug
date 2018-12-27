using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Detail.ViewModel
    /// </summary>
    public class Detail : Common.ViewModelBase, IDisposable
    {

        #region 基底

        /// <summary>
        /// The model.Detail
        /// </summary>
        private Model.Detail _Model;

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
                if (!_Model.Drug.Name.Equals(value))
                {
                    _Model.Drug.Name = value;
                    CallPropertyChanged();
                }
            }
        }

        #region 朝食

        /// <summary>
        /// 服用FLGプロパティ
        /// 朝食
        /// </summary>
        /// <value><c>true</c> if is breakfast; otherwise, <c>false</c>.</value>
        public bool IsBreakfast
        {
            get { return _Model.Drug.Breakfast.IsDrug; }
            set
            {
                if (!_Model.Drug.Breakfast.IsDrug.Equals(value))
                {
                    _Model.Drug.Breakfast.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 朝食
        /// </summary>
        /// <value>The breakfast volume.</value>
        public ObservableCollection<string> BreakfastVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 朝食
        /// </summary>
        /// <value>The index of the breakfast volume.</value>
        public Int32 BreakfastVolumeIndex
        {
            get { return _Model.BreakfastVolumeIndex; }
            set
            {
                if (!_Model.BreakfastVolumeIndex.Equals(value))
                {
                    _Model.BreakfastVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用タイミングプロパティ
        /// 朝食
        /// </summary>
        /// <value>The meal timing.</value>
        public ObservableCollection<string> BreakfastTiming { get; set; }

        /// <summary>
        /// 服用タイミングIndexプロパティ
        /// 朝食
        /// </summary>
        /// <value>The meal timing.</value>
        public int BreakfastTimingIndex
        {
            get { return (int)_Model.Drug.Breakfast.Kind - 1; }
            set
            {
                value += 1;
                if (!((int)_Model.Drug.Breakfast.Kind).Equals(value))
                {
                    _Model.Drug.Breakfast.Kind = (Class.UserControl.KindTiming)value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 昼食

        /// <summary>
        /// 服用FLGプロパティ
        /// 昼食
        /// </summary>
        /// <value><c>true</c> if is lunch; otherwise, <c>false</c>.</value>
        public bool IsLunch
        {
            get { return _Model.Drug.Lunch.IsDrug; }
            set 
            {
                if (!_Model.Drug.Lunch.IsDrug.Equals(value))
                {
                    _Model.Drug.Lunch.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 昼食
        /// </summary>
        /// <value>The lunch volume.</value>
        public ObservableCollection<string> LunchVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 昼食
        /// </summary>
        /// <value>The index of the lunch volume.</value>
        public Int32 LunchVolumeIndex
        {
            get { return _Model.LunchVolumeIndex; }
            set
            {
                if (!_Model.LunchVolumeIndex.Equals(value))
                {
                    _Model.LunchVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用タイミングプロパティ
        /// 昼食
        /// </summary>
        /// <value>The meal timing.</value>
        public ObservableCollection<string> LunchTiming { get; set; }

        /// <summary>
        /// 服用タイミングIndexプロパティ
        /// 昼食
        /// </summary>
        /// <value>The meal timing.</value>
        public int LunchTimingIndex
        {
            get { return (int)_Model.Drug.Lunch.Kind - 1; }
            set
            {
                value += 1;
                if (!((int)_Model.Drug.Lunch.Kind).Equals(value))
                {
                    _Model.Drug.Lunch.Kind = (Class.UserControl.KindTiming)value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 夕食

        /// <summary>
        /// 服用FLGプロパティ
        /// 夕食
        /// </summary>
        /// <value><c>true</c> if is dinner; otherwise, <c>false</c>.</value>
        public bool IsDinner
        {
            get { return _Model.Drug.Dinner.IsDrug; }
            set 
            {
                if (!_Model.Drug.Dinner.IsDrug.Equals(value))
                {
                    _Model.Drug.Dinner.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 夕食
        /// </summary>
        /// <value>The dinner volume.</value>
        public ObservableCollection<string> DinnerVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 夕食
        /// </summary>
        /// <value>The index of the dinner volume.</value>
        public Int32 DinnerVolumeIndex
        {
            get { return _Model.DinnerVolumeIndex; }
            set
            {
                if (!_Model.DinnerVolumeIndex.Equals(value))
                {
                    _Model.DinnerVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用タイミングプロパティ
        /// 夕食
        /// </summary>
        /// <value>The meal timing.</value>
        public ObservableCollection<string> DinnerTiming { get; set; }

        /// <summary>
        /// 服用タイミングIndexプロパティ
        /// 夕食
        /// </summary>
        /// <value>The meal timing.</value>
        public int DinnerTimingIndex
        {
            get { return (int)_Model.Drug.Dinner.Kind - 1; }
            set
            {
                value += 1;
                if (!((int)_Model.Drug.Dinner.Kind).Equals(value))
                {
                    _Model.Drug.Dinner.Kind = (Class.UserControl.KindTiming)value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 服用FLGプロパティ
        /// 就寝前
        /// </summary>
        /// <value><c>true</c> if is sleep; otherwise, <c>false</c>.</value>
        public bool IsSleep
        {
            get { return _Model.Drug.Sleep.IsDrug; }
            set
            {
                if (!_Model.Drug.Sleep.IsDrug.Equals(value))
                {
                    _Model.Drug.Sleep.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 就寝前
        /// </summary>
        /// <value>The sleep volume.</value>
        public ObservableCollection<string> SleepVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 就寝前
        /// </summary>
        /// <value>The index of the sleep volume.</value>
        public Int32 SleepVolumeIndex
        {
            get { return _Model.SleepVolumeIndex; }
            set
            {
                if (!_Model.SleepVolumeIndex.Equals(value))
                {
                    _Model.SleepVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 頓服

        /// <summary>
        /// 服用FLGプロパティ
        /// 頓服
        /// </summary>
        /// <value><c>true</c> if is to be taken; otherwise, <c>false</c>.</value>
        public bool IsToBeTaken
        {
            get { return _Model.Drug.ToBeTaken.IsDrug; }
            set
            {
                if (!_Model.Drug.ToBeTaken.IsDrug.Equals(value))
                {
                    _Model.Drug.ToBeTaken.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量
        /// 頓服
        /// </summary>
        /// <value>To be taken volume.</value>
        public ObservableCollection<string> ToBeTakenVolume { get; set; }

        /// <summary>
        /// 服用量Index
        /// 頓服
        /// </summary>
        /// <value>The index of the to be taken volume.</value>
        public Int32 ToBeTakenVolumeIndex
        {
            get { return _Model.ToBeTakenVolumeIndex; }
            set
            {
                if (!_Model.ToBeTakenVolumeIndex.Equals(value))
                {
                    _Model.ToBeTakenVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 指定時間

        /// <summary>
        /// 服用FLGプロパティ
        /// 指定時間
        /// </summary>
        /// <value><c>true</c> if is appoint; otherwise, <c>false</c>.</value>
        public bool IsAppoint
        {
            get { return _Model.Drug.Appoint.IsDrug; }
            set
            {
                if (!_Model.Drug.Appoint.IsDrug.Equals(value))
                {
                    _Model.Drug.Appoint.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint volume.</value>
        public ObservableCollection<string> AppointVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 指定時間
        /// </summary>
        /// <value>The index of the appoint volume.</value>
        public Int32 AppointVolumeIndex
        {
            get { return _Model.AppointVolumeIndex; }
            set
            {
                if (!_Model.AppointVolumeIndex.Equals(value))
                {
                    _Model.AppointVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 指定日時：最小日付プロパティ
        /// </summary>
        /// <value>The appoint minimum date.</value>
        public DateTime AppointMinDate
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 指定日時：最大日付プロパティ
        /// </summary>
        /// <value>The appoint max date.</value>
        public DateTime AppointMaxDate
        {
            get { return DateTime.Now.AddYears(1); }
        }

        /// <summary>
        /// 指定日時：日付プロパティ
        /// </summary>
        /// <value>The appoint date.</value>
        public DateTime AppointDate
        {
            get { return _Model.Drug.AppointTime; }
            set
            {
                if (!(_Model.Drug.AppointTime.Year.Equals(value.Year)
                    && _Model.Drug.AppointTime.Month.Equals(value.Month)
                    && _Model.Drug.AppointTime.Day.Equals(value.Day)))
                {

                    _SelectDate = value;
                    SetAppointTime();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 指定日時：時間プロパティ
        /// </summary>
        /// <value>The appoint time.</value>
        public TimeSpan AppointTime
        {
            get { return _Model.Drug.AppointTime.TimeOfDay; }
            set
            {
                if (!(_Model.Drug.AppointTime.Hour.Equals(value.Hours)
                    && _Model.Drug.AppointTime.Minute.Equals(value.Minutes)))
                {
                    _SelectTime = value;
                    SetAppointTime();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 現在の指定日時：日付
        /// </summary>
        private DateTime _SelectDate;

        /// <summary>
        /// 現在の指定日時：時間
        /// </summary>
        private TimeSpan _SelectTime;

        /// <summary>
        /// 指定日時の設定
        /// </summary>
        private void SetAppointTime()
        {

            _Model.Drug.AppointTime = new Class.Method().ConvertToDateTime(
                                                                _SelectDate.Year
                                                                , _SelectDate.Month
                                                                , _SelectDate.Day
                                                                , _SelectTime.Hours
                                                                , _SelectTime.Minutes
                                                                , _Model.Drug.AppointTime);

        }

        /// <summary>
        /// 指定日時プロパティ
        /// 日毎
        /// </summary>
        /// <value>The hour each time.</value>
        public ObservableCollection<string> AppointDayEach { get; set; }

        /// <summary>
        /// 指定日時Indexプロパティ
        /// 日毎
        /// </summary>
        /// <value>The index of the hour each time.</value>
        public Int32 AppointDayEachIndex
        {
            get { return _Model.AppointDayEachIndex; }
            set
            {
                if (!_Model.AppointDayEachIndex.Equals(value))
                {
                    _Model.AppointDayEachIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region 時間毎

        /// <summary>
        /// 服用FLGプロパティ
        /// 時間毎
        /// </summary>
        /// <value><c>true</c> if is hour each; otherwise, <c>false</c>.</value>
        public bool IsHourEach
        {
            get { return _Model.Drug.HourEach.IsDrug; }
            set
            {
                if (!_Model.Drug.HourEach.IsDrug.Equals(value))
                {
                    _Model.Drug.HourEach.IsDrug = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 服用量プロパティ
        /// 時間毎
        /// </summary>
        /// <value>The hour each volume.</value>
        public ObservableCollection<string> HourEachVolume { get; set; }

        /// <summary>
        /// 服用量Indexプロパティ
        /// 時間毎
        /// </summary>
        /// <value>The index of the hour each volume.</value>
        public Int32 HourEachVolumeIndex
        {
            get { return _Model.HourEachVolumeIndex; }
            set
            {
                if (!_Model.HourEachVolumeIndex.Equals(value))
                {
                    _Model.HourEachVolumeIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 設定時間プロパティ
        /// 時間毎
        /// </summary>
        /// <value>The hour each time.</value>
        public ObservableCollection<string> HourEachTime { get; set; }

        /// <summary>
        /// 設定時間Indexプロパティ
        /// 時間毎
        /// </summary>
        /// <value>The index of the hour each time.</value>
        public Int32 HourEachTimeIndex
        {
            get { return _Model.HourEachTimeIndex; }
            set
            {
                if (!_Model.HourEachTimeIndex.Equals(value))
                {
                    _Model.HourEachTimeIndex = value;
                    CallPropertyChanged();
                }
            }
        }


        /// <summary>
        /// 時間毎：最小日付プロパティ
        /// </summary>
        /// <value>The appoint minimum date.</value>
        public DateTime HourEachMinDate
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 時間毎：最大日付プロパティ
        /// </summary>
        /// <value>The appoint max date.</value>
        public DateTime HourEachMaxDate
        {
            get { return DateTime.Now.AddYears(1); }
        }

        /// <summary>
        /// 時間毎：日付プロパティ
        /// </summary>
        /// <value>The appoint date.</value>
        public DateTime HourEachStartDate
        {
            get { return _Model.Drug.HourEachNextTime; }
            set
            {
                if (!(_Model.Drug.HourEachNextTime.Year.Equals(value.Year)
                    && _Model.Drug.HourEachNextTime.Month.Equals(value.Month)
                    && _Model.Drug.HourEachNextTime.Day.Equals(value.Day)))
                {

                    _SelectHourEachDate = value;
                    SetHourEachNextTime();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 時間毎：時間プロパティ
        /// </summary>
        /// <value>The appoint time.</value>
        public TimeSpan HourEachStartTime
        {
            get { return _Model.Drug.HourEachNextTime.TimeOfDay; }
            set
            {
                if (!(_Model.Drug.HourEachNextTime.Hour.Equals(value.Hours)
                    && _Model.Drug.HourEachNextTime.Minute.Equals(value.Minutes)))
                {
                    _SelectHourEachTime = value;
                    SetHourEachNextTime();
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 現在の指定日時：日付
        /// </summary>
        private DateTime _SelectHourEachDate;

        /// <summary>
        /// 現在の指定日時：時間
        /// </summary>
        private TimeSpan _SelectHourEachTime;

        /// <summary>
        /// 指定日時の設定
        /// </summary>
        private void SetHourEachNextTime()
        {

            _Model.Drug.HourEachNextTime = new Class.Method().ConvertToDateTime(
                                                                _SelectHourEachDate.Year
                                                                , _SelectHourEachDate.Month
                                                                , _SelectHourEachDate.Day
                                                                , _SelectHourEachTime.Hours
                                                                , _SelectHourEachTime.Minutes
                                                                , _Model.Drug.HourEachNextTime);

        }


        #endregion

        /// <summary>
        /// 処方量プロパティ
        /// </summary>
        /// <value>The total volume.</value>
        public string TotalVolume
        {
            get { return _Model.Drug.TotalVolume.ToString(); }
            set
            {

                if (value.Length.Equals(0))
                    value = "0";

                if (Int32.TryParse(value, out Int32 Number))
                {

                    if (Number < 0 || MaxTotalVolume < Number)
                    {
                        //メッセージ表示後、元値に戻す
                        CallPropertyChanged("CallTotalVolume");
                        CallPropertyChanged();
                    }
                    else if (!_Model.Drug.TotalVolume.Equals(Number))
                    {
                        _Model.Drug.TotalVolume = Number;
                        CallPropertyChanged();
                    }

                }
                else
                {
                    //数値でない場合、元値に戻す
                    CallPropertyChanged();
                }

            }
        }

        /// <summary>
        /// 薬残量お知らせ錠数プロパティ
        /// </summary>
        /// <value>The alarm volume.</value>
        public string AlarmVolume
        {
            get { return _Model.Drug.PrescriptionAlarmVolume.ToString(); }
            set
            {

                if (value.Length.Equals(0))
                    value = "0";

                if (Int32.TryParse(value, out Int32 Number))
                {

                    if (Number < 0 || MaxAlarmVolume < Number)
                    {
                        //メッセージ表示後、元値に戻す
                        CallPropertyChanged("CallAlarmVolume");
                        CallPropertyChanged();
                    }
                    else if (!_Model.Drug.PrescriptionAlarmVolume.Equals(Number))
                    {
                        _Model.Drug.PrescriptionAlarmVolume = Number;
                        CallPropertyChanged();
                    }

                }
                else
                {
                    //数値でない場合、元値に戻す
                    CallPropertyChanged();
                }

            }
        }

        /// <summary>
        /// 備考欄プロパティ
        /// </summary>
        /// <value>The remarks.</value>
        public string Remarks
        {
            get { return _Model.Drug.Remarks; }
            set
            {

                //=は全角に置換
                value = value.Replace("=", "＝");

                if (!_Model.Drug.Remarks.Equals(value))
                {
                    _Model.Drug.Remarks = value;
                    CallPropertyChanged();
                }

            }
        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Detail(Int32 Index)
        {

            _Model = new Model.Detail(Index);

            BreakfastVolume = new ObservableCollection<string>();
            LunchVolume = new ObservableCollection<string>();
            DinnerVolume = new ObservableCollection<string>();
            SleepVolume = new ObservableCollection<string>();
            ToBeTakenVolume = new ObservableCollection<string>();
            AppointVolume = new ObservableCollection<string>();
            HourEachVolume = new ObservableCollection<string>();

            _Model.GetVolumeList().ForEach(Volume =>
            {
                string Value = Volume.ToString("00");

                BreakfastVolume.Add(Value);
                LunchVolume.Add(Value);
                DinnerVolume.Add(Value);
                SleepVolume.Add(Value);
                ToBeTakenVolume.Add(Value);
                AppointVolume.Add(Value);
                HourEachVolume.Add(Value);

            });

            BreakfastVolumeIndex = _Model.GetVolumeBreakfastIndex();
            LunchVolumeIndex = _Model.GetVolumeLunchIndex();
            DinnerVolumeIndex = _Model.GetVolumeDinnerIndex();
            SleepVolumeIndex = _Model.GetVolumeSleepIndex();
            ToBeTakenVolumeIndex = _Model.GetVolumeToBeTakenIndex();
            AppointVolumeIndex = _Model.GetVolumeAppointIndex();
            HourEachVolumeIndex = _Model.GetVolumeHourEachIndex();

            HourEachTime = new ObservableCollection<string>();

            _SelectDate = _Model.Drug.AppointTime;
            _SelectTime = _Model.Drug.AppointTime.TimeOfDay;

            _SelectHourEachDate = _Model.Drug.HourEachNextTime;
            _SelectHourEachTime = _Model.Drug.HourEachNextTime.TimeOfDay;

            AppointDayEach = new ObservableCollection<string>();
            _Model.GetDayEachList().ForEach(Day => 
            { 
                if (Day.Equals(0))
                {
                    AppointDayEach.Add("  ");
                }
                else
                {
                    AppointDayEach.Add(Day.ToString("00"));
                }
            });
            AppointDayEachIndex = _Model.GetAppointDayEachIndex();

            _Model.GetHourEachList().ForEach(Time => { HourEachTime.Add(Time.ToString("00")); });
            HourEachTimeIndex = _Model.GetHourEachIndex();

            BreakfastTiming = new ObservableCollection<string>();
            LunchTiming = new ObservableCollection<string>();
            DinnerTiming = new ObservableCollection<string>();
            _Model.GetMealTimingList().ForEach(Timing => 
            { 
                BreakfastTiming.Add(Timing);
                LunchTiming.Add(Timing);
                DinnerTiming.Add(Timing);
            });
            BreakfastTimingIndex = _Model.GetBreakfastTimingIndex();
            LunchTimingIndex = _Model.GetLunchTimingIndex();
            DinnerTimingIndex = _Model.GetDinnerTimingIndex();

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.Detail"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.Detail"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Detail"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Detail"/> was occupying.</remarks>
        public void Dispose()
        {

            _Model.Dispose();
            _Model = null;

            if (BreakfastVolume != null)
            {
                BreakfastVolume.Clear();
                BreakfastVolume = null;
            }

            if (LunchVolume != null)
            {
                LunchVolume.Clear();
                LunchVolume = null;
            }

            if (DinnerVolume != null)
            {
                DinnerVolume.Clear();
                DinnerVolume = null;
            }

            if (BreakfastTiming != null)
            {
                BreakfastTiming.Clear();
                BreakfastTiming = null;
            }

            if (LunchTiming != null)
            {
                LunchTiming.Clear();
                LunchTiming = null;
            }

            if (DinnerTiming != null)
            {
                DinnerTiming.Clear();
                DinnerTiming = null;
            }

            if (SleepVolume != null)
            {
                SleepVolume.Clear();
                SleepVolume = null;
            }

            if (ToBeTakenVolume != null)
            {
                ToBeTakenVolume.Clear();
                ToBeTakenVolume = null;
            }

            if (AppointVolume != null)
            {
                AppointVolume.Clear();
                AppointVolume = null;
            }

            if (AppointDayEach != null)
            {
                AppointDayEach.Clear();
                AppointDayEach = null;
            }

            if (HourEachVolume != null)
            {
                HourEachVolume.Clear();
                HourEachVolume = null;
            }

            if (HourEachTime != null)
            {
                HourEachTime.Clear();
                HourEachTime = null;
            }

        }

    }
}
