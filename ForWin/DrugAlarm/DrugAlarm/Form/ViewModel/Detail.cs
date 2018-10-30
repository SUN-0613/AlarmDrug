using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Detail.xamlのViewModel
    /// </summary>
    class Detail : Common.ViewModelBase, IDisposable
    {

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="PropertyName">Changedイベントを発生させたいプロパティ名</param>
        protected override void CallPropertyChanged(string PropertyName = "")
        {
            
            //プロパティ名が指定されていない場合は呼び出し元メソッド名とする
            if (PropertyName.Length.Equals(0))
            {
                StackFrame Caller = new StackFrame(1);                      //呼び出し元メソッド情報
                string[] MethodName = Caller.GetMethod().Name.Split('_');   //呼び出し元メソッド名
                PropertyName = MethodName[MethodName.Length - 1];
            }

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

        /// <summary>
        /// 薬名プロパティ
        /// </summary>
        public string Name
        {
            get { return _Model.Drug.Name; }
            set
            {
                if (!_Model.Drug.Name.Equals(value))
                {
                    _Model.Drug.Name = value;
                    CallPropertyChanged();

                    IsEdited = true;

                }
            }
        }

        /// <summary>
        /// 朝食服用FLGプロパティ
        /// </summary>
        public bool IsBreakfast
        {
            get
            {
                return _Model.Drug.Breakfast.IsDrug;
            }
            set
            {
                _Model.Drug.Breakfast.IsDrug = value;
                CallPropertyChanged();

            }
        }

        /// <summary>
        /// 服用量：朝食
        /// </summary>
        public ObservableCollection<string> BreakfastVolume { get; set; }

        /// <summary>
        /// 服用量Index：朝食
        /// </summary>
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
        /// 昼食服用FLGプロパティ
        /// </summary>
        public bool IsLunch
        {
            get
            {
                return _Model.Drug.Lunch.IsDrug;
            }
            set
            {
                _Model.Drug.Lunch.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：昼食
        /// </summary>
        public ObservableCollection<string> LunchVolume { get; set; }

        /// <summary>
        /// 服用量Index：昼食
        /// </summary>
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
        /// 夕食服用FLGプロパティ
        /// </summary>
        public bool IsDinner
        {
            get
            {
                return _Model.Drug.Dinner.IsDrug;
            }
            set
            {
                _Model.Drug.Dinner.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：夕食
        /// </summary>
        public ObservableCollection<string> DinnerVolume { get; set; }

        /// <summary>
        /// 服用量Index：夕食
        /// </summary>
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
        /// 就寝前服用FLGプロパティ
        /// </summary>
        public bool IsSleep
        {
            get
            {
                return _Model.Drug.Sleep.IsDrug;
            }
            set
            {
                _Model.Drug.Sleep.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：就寝前
        /// </summary>
        public ObservableCollection<string> SleepVolume { get; set; }

        /// <summary>
        /// 服用量Index：就寝前
        /// </summary>
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

        /// <summary>
        /// 頓服服用FLGプロパティ
        /// </summary>
        public bool IsToBeTaken
        {
            get
            {
                return _Model.Drug.ToBeTaken.IsDrug;
            }
            set
            {
                _Model.Drug.ToBeTaken.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：頓服
        /// </summary>
        public ObservableCollection<string> ToBeTakenVolume { get; set; }

        /// <summary>
        /// 服用量Index：頓服
        /// </summary>
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

        /// <summary>
        /// 時間指定服用FLGプロパティ
        /// </summary>
        public bool IsAppoint
        {
            get
            {
                return _Model.Drug.Appoint.IsDrug;
            }
            set
            {
                _Model.Drug.Appoint.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：時間指定
        /// </summary>
        public ObservableCollection<string> AppointVolume { get; set; }

        /// <summary>
        /// 服用量Index：時間指定
        /// </summary>
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
        /// 年：時間指定
        /// </summary>
        public ObservableCollection<string> AppointYear { get; set; }

        /// <summary>
        /// 年Index：時間指定
        /// </summary>
        public Int32 AppointYearIndex
        {
            get { return _Model.AppointYearIndex; }
            set
            {

                if (!_Model.AppointYearIndex.Equals(value))
                {

                    _Model.AppointYearIndex = value;

                    //2月が選択中なら、閏年を考慮して日リストを再取得
                    if ((_Model.AppointMonthIndex + 1).Equals(2))
                        RemakeDayList();

                    CallPropertyChanged();
                    _Model.SetAppointDateTime();
                }

            }
        }

        /// <summary>
        /// 月：時間指定
        /// </summary>
        public ObservableCollection<string> AppointMonth { get; set; }

        /// <summary>
        /// 月Index：時間指定
        /// </summary>
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
        /// 日：時間指定
        /// </summary>
        public ObservableCollection<string> AppointDay { get; set; }

        /// <summary>
        /// 日Index：時間指定
        /// </summary>
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
        /// 時：時間指定
        /// </summary>
        public ObservableCollection<string> AppointHour { get; set; }

        /// <summary>
        /// 時Index：時間指定
        /// </summary>
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
        /// 分：時間指定
        /// </summary>
        public ObservableCollection<string> AppointMinute { get; set; }

        /// <summary>
        /// 分Index：時間指定
        /// </summary>
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
        /// 時間毎服用FLGプロパティ
        /// </summary>
        public bool IsHourEach
        {
            get
            {
                return _Model.Drug.HourEach.IsDrug;
            }
            set
            {
                _Model.Drug.HourEach.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 服用量：時間毎
        /// </summary>
        public ObservableCollection<string> HourEachVolume { get; set; }

        /// <summary>
        /// 服用量Index：時間毎
        /// </summary>
        public Int32 HourEachVolumeIndex
        {
            get { return _Model.HourEachVolumeIndex; }
            set
            {
                _Model.HourEachVolumeIndex = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 時間毎：設定時間
        /// </summary>
        public ObservableCollection<string> HourEachTime { get; set; }

        /// <summary>
        /// 設定時間Index：時間毎
        /// </summary>
        public Int32 HourEachTimeIndex
        {
            get { return _Model.HourEachTimeIndex; }
            set
            {
                _Model.HourEachTimeIndex = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 食事時の服用タイミング
        /// </summary>
        /// <remarks>朝昼夕ともに同設定のため、朝のみで判断</remarks>
        public Class.Parameter.DrugParameter.KindTiming MealTiming
        {
            get
            {
                return _Model.Drug.Breakfast.Kind;
            }
            set
            {
                _Model.Drug.Breakfast.Kind = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 処方量
        /// </summary>
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
        /// 薬残量お知らせ錠数
        /// </summary>
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
        /// 備考欄
        /// </summary>
        public string Remarks
        {
            get { return _Model.Drug.Remarks; }
            set
            {
                if (!_Model.Drug.Remarks.Equals(value))
                {
                    _Model.Drug.Remarks = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 処方量最大値
        /// </summary>
        private const Int32 MaxTotalVolume = 3000;

        /// <summary>
        /// お知らせ錠数最大値
        /// </summary>
        private const Int32 MaxAlarmVolume = 100;

        /// <summary>
        /// Detail.Model
        /// </summary>
        private Model.Detail _Model;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Index">Parameter.DrugList[Index]</param>
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

            /* AppointMonthIndexプロパティにて作成済みのためコメントアウト
             _Model.GetDayList().ForEach(Day => { AppointDay.Add(Day.ToString("00")); });
             */
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
        public void Dispose()
        {
            _Model.Dispose();
            _Model = null;

            BreakfastVolume.Clear();
            BreakfastVolume = null;

            LunchVolume.Clear();
            LunchVolume = null;

            DinnerVolume.Clear();
            DinnerVolume = null;

            SleepVolume.Clear();
            SleepVolume = null;

            ToBeTakenVolume.Clear();
            ToBeTakenVolume = null;

            AppointVolume.Clear();
            AppointVolume = null;

            HourEachVolume.Clear();
            HourEachVolume = null;

            AppointYear.Clear();
            AppointYear = null;

            AppointMonth.Clear();
            AppointMonth = null;

            AppointDay.Clear();
            AppointDay = null;

            AppointHour.Clear();
            AppointHour = null;

            AppointMinute.Clear();
            AppointMinute = null;

            HourEachTime.Clear();
            HourEachTime = null;

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
        /// 保存確認メッセージ作成
        /// </summary>
        /// <returns>保存確認メッセージ</returns>
        public string MakeSaveMessage()
        {
            return DrugAlarm.Properties.Resources.Detail_SaveMessage.Replace("_DRUG_", _Model.Drug.Name);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Model.Save();
        }

        /// <summary>
        /// 日リスト再作成
        /// </summary>
        private void RemakeDayList()
        {

            if (!_Model.AppointYearIndex.Equals(-1) && !_Model.AppointMonthIndex.Equals(-1))
            {

                Int32 Tmp = _Model.AppointDayIndex;
                _Model.AppointDayIndex = -1;

                AppointDay.Clear();
                _Model.GetDayList().ForEach(Data => { AppointDay.Add(Data.ToString("00")); });

                if (AppointDay.Count <= Tmp)
                {
                    Tmp = AppointDay.Count - 1;
                }

                _Model.AppointDayIndex = Tmp;

            }

        }

        /// <summary>
        /// 処方量入力オーバーメッセージ作成
        /// </summary>
        /// <returns>処方量入力オーバーメッセージ</returns>
        public string MakeTotalVolumeOverMessage()
        {
            return DrugAlarm.Properties.Resources.Detail_TotalVolumeOverMessage.Replace("_TOTALVOLUME_", MaxTotalVolume.ToString());
        }

        /// <summary>
        /// 薬残量お知らせ入力オーバーメッセージ作成
        /// </summary>
        /// <returns>処方量入力オーバーメッセージ</returns>
        public string MakeAlarmVolumeOverMessage()
        {
            return DrugAlarm.Properties.Resources.Detail_AlarmVolumeOverMessage.Replace("_ALARMVOLUME_", MaxAlarmVolume.ToString());
        }

    }

}
