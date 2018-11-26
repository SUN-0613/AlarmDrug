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

        #endregion

        /// <summary>
        /// 食事時の服用タイミングプロパティ
        /// </summary>
        /// <value>The meal timing.</value>
        public Class.UserControl.KindTiming MealTiming
        {
            get { return _Model.Drug.Breakfast.Kind; }
            set 
            {
                if (!_Model.Drug.Breakfast.Kind.Equals(value))
                {
                    _Model.Drug.Breakfast.Kind = value;
                    CallPropertyChanged();
                }
            }
        }

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
        /// 年プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint year.</value>
        public ObservableCollection<string> AppointYear { get; set; }

        /// <summary>
        /// 年Indexプロパティ
        /// 指定時間
        /// </summary>
        /// <value>The index of the appoint year.</value>
        public Int32 AppointYearIndex
        {
            get { return _Model.AppointYearIndex; }
            set
            {
                if (!_Model.AppointYearIndex.Equals(value))
                {
                    _Model.AppointYearIndex = value;

                    //2月が選択中なら、うるう年を考慮して日リストを再取得
                    if ((_Model.AppointMonthIndex + 1).Equals(2))
                    {
                        RemakeDayList();
                    }

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();
                }
            }
        }

        /// <summary>
        /// 月プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint month.</value>
        public ObservableCollection<string> AppointMonth { get; set; }

        /// <summary>
        /// 月Indexプロパティ
        /// 指定時間
        /// </summary>
        /// <value>The index of the appoint month.</value>
        public Int32 AppointMonthIndex
        {
            get { return _Model.AppointMonthIndex; }
            set
            {
                if (!_Model.AppointMonthIndex.Equals(value))
                {

                    _Model.AppointMonthIndex = value;
                    RemakeDayList();

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();

                }
            }
        }

        /// <summary>
        /// 日プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint day.</value>
        public ObservableCollection<string> AppointDay { get; set; }

        /// <summary>
        /// 日Indexプロパティ
        /// 指定時間
        /// </summary>
        /// <value>The index of the appoint day.</value>
        public Int32 AppointDayIndex
        {
            get { return _Model.AppointDayIndex; }
            set
            {
                if (!_Model.AppointDayIndex.Equals(value))
                {

                    _Model.AppointDayIndex = value;

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();

                }
            }
        }

        /// <summary>
        /// 時プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint hour.</value>
        public ObservableCollection<string> AppointHour { get; set; }

        /// <summary>
        /// 時Indexプロパティ
        /// </summary>
        /// <value>The index of the appoint hour.</value>
        public Int32 AppointHourIndex
        {
            get { return _Model.AppointHourIndex; }
            set
            {
                if (!_Model.AppointHourIndex.Equals(value))
                {

                    _Model.AppointHourIndex = value;

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();

                }
            }
        }

        /// <summary>
        /// 分プロパティ
        /// 指定時間
        /// </summary>
        /// <value>The appoint minute.</value>
        public ObservableCollection<string> AppointMinute { get; set; }

        /// <summary>
        /// 分Indexプロパティ
        /// 指定時間
        /// </summary>
        /// <value>The index of the appoint minute.</value>
        public Int32 AppointMinuteIndex
        {
            get { return _Model.AppointMinuteIndex; }
            set
            {
                if (!_Model.AppointMinuteIndex.Equals(value))
                {

                    _Model.AppointMinuteIndex = value;

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();

                }
            }
        }

        /// <summary>
        /// 日リスト再作成
        /// </summary>
        private void RemakeDayList()
        {

            if (!AppointYearIndex.Equals(-1) && !AppointMonthIndex.Equals(-1))
            {

                Int32 Tmp = _Model.AppointDayIndex;
                _Model.AppointDayIndex = -1;

                AppointDay.Clear();
                _Model.GetDayList().ForEach(Data => { AppointDay.Add(Data.ToString("00")); });

                if (AppointDay.Count <= Tmp)
                {
                    Tmp = AppointDay.Count - 1;
                }

                AppointDayIndex = Tmp;

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

            AppointYear = new ObservableCollection<string>();
            AppointMonth = new ObservableCollection<string>();
            AppointDay = new ObservableCollection<string>();
            AppointHour = new ObservableCollection<string>();
            AppointMinute = new ObservableCollection<string>();
            HourEachTime = new ObservableCollection<string>();

            _Model.GetYearList().ForEach(Year => { AppointYear.Add(Year.ToString("0000")); });
            AppointYearIndex = _Model.GetAppointYearIndex();

            _Model.GetMonthList().ForEach(Month => { AppointMonth.Add(Month.ToString("00")); });
            AppointMonthIndex = _Model.GetAppointMonthIndex();

            //AppointMonthIndexプロパティ作成時に日リストは作成済み
            AppointDayIndex = _Model.GetAppointDayIndex();

            _Model.GetHourList().ForEach(Hour => { AppointHour.Add(Hour.ToString("00")); });
            AppointHourIndex = _Model.GetAppointHourIndex();

            _Model.GetMinuteList().ForEach(Minute => { AppointMinute.Add(Minute.ToString("00")); });
            AppointMinuteIndex = _Model.GetAppointMinuteIndex();

            _Model.GetHourEachList().ForEach(Time => { HourEachTime.Add(Time.ToString("00")); });
            HourEachTimeIndex = _Model.GetHourEachIndex();

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

            if (HourEachVolume != null)
            {
                HourEachVolume.Clear();
                HourEachVolume = null;
            }

            if (AppointYear != null)
            {
                AppointYear.Clear();
                AppointYear = null;
            }

            if (AppointMonth != null)
            {
                AppointMonth.Clear();
                AppointMonth = null;
            }

            if (AppointDay != null)
            {
                AppointDay.Clear();
                AppointDay = null;
            }

            if (AppointHour != null)
            {
                AppointHour.Clear();
                AppointHour = null;
            }

            if (AppointMinute != null)
            {
                AppointMinute.Clear();
                AppointMinute = null;
            }

            if (HourEachTime != null)
            {
                HourEachTime.Clear();
                HourEachTime = null;
            }

        }

    }
}
