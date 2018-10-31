using System;
using System.Collections.Generic;
using System.Text;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Detail.Model
    /// </summary>
    class Detail : IDisposable
    {

        #region プロパティ

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited;

        /// <summary>
        /// 対象薬
        /// </summary>
        public Class.Parameter.DrugParameter Drug;

        /// <summary>
        /// 薬パラメータIndex
        /// -1は新規登録
        /// </summary>
        private Int32 _SelectedIndex;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        #endregion

        #region コマンド

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 初期化
        /// 編集内容を破棄
        /// </summary>
        public void Initialize()
        {
            _Parameter.Load();
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {

            if (_SelectedIndex == -1)
            {
                _Parameter.DrugList.Add(Drug);
            }
            else
            {
                _Parameter.DrugList.RemoveAt(_SelectedIndex);
                _Parameter.DrugList.Insert(_SelectedIndex, Drug);
            }

            _Parameter.Save();
            _Parameter.Load();

        }

        #endregion

        #region 朝食

        /// <summary>
        /// 服用量Index：朝食プロパティ
        /// </summary>
        public Int32 BreakfastVolumeIndex
        {
            get { return _BreakfastVolumeIndex; }
            set
            {
                _BreakfastVolumeIndex = value;
                Drug.Breakfast.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 服用量Index：朝食
        /// </summary>
        private Int32 _BreakfastVolumeIndex;

        /// <summary>
        /// 朝食：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeBreakfastIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.Breakfast.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region 昼食

        /// <summary>
        /// 服用量Index：昼食プロパティ
        /// </summary>
        public Int32 LunchVolumeIndex
        {
            get { return _LunchVolumeIndex; }
            set
            {
                _LunchVolumeIndex = value;
                Drug.Lunch.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 服用量Index：昼食
        /// </summary>
        private Int32 _LunchVolumeIndex;

        /// <summary>
        /// 昼食：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeLunchIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.Lunch.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region 夕食

        /// <summary>
        /// 服用量Index：夕食プロパティ
        /// </summary>
        public Int32 DinnerVolumeIndex
        {
            get { return _DinnerVolumeIndex; }
            set
            {
                _DinnerVolumeIndex = value;
                Drug.Dinner.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 服用量Index：夕食
        /// </summary>
        private Int32 _DinnerVolumeIndex;

        /// <summary>
        /// 夕食：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeDinnerIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.Dinner.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 服用量Index：就寝前プロパティ
        /// </summary>
        public Int32 SleepVolumeIndex
        {
            get { return _SleepVolumeIndex; }
            set
            {
                _SleepVolumeIndex = value;
                Drug.Sleep.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 服用量Index：就寝前
        /// </summary>
        private Int32 _SleepVolumeIndex;

        /// <summary>
        /// 就寝前：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeSleepIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.Sleep.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region 頓服

        /// <summary>
        /// 服用量Index：頓服プロパティ
        /// </summary>
        public Int32 ToBeTakenVolumeIndex
        {
            get { return _ToBeTakenVolumeIndex; }
            set
            {
                _ToBeTakenVolumeIndex = value;
                Drug.ToBeTaken.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 服用量Index：頓服
        /// </summary>
        private Int32 _ToBeTakenVolumeIndex;

        /// <summary>
        /// 頓服：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeToBeTakenIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.ToBeTaken.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region 指定時間

        /// <summary>
        /// 服用量Index：指定時間プロパティ
        /// </summary>
        public Int32 AppointVolumeIndex
        {
            get { return _AppointVolumeIndex; }
            set
            {
                _AppointVolumeIndex = value;
                Drug.Appoint.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 年Index：指定日時
        /// </summary>
        public Int32 AppointYearIndex;

        /// <summary>
        /// 月Index：指定日時
        /// </summary>
        public Int32 AppointMonthIndex;

        /// <summary>
        /// 日Index：指定日時
        /// </summary>
        public Int32 AppointDayIndex;

        /// <summary>
        /// 時Index：指定日時
        /// </summary>
        public Int32 AppointHourIndex;

        /// <summary>
        /// 分Index：指定日時
        /// </summary>
        public Int32 AppointMinuteIndex;

        /// <summary>
        /// 服用量Index：指定時間
        /// </summary>
        private Int32 _AppointVolumeIndex;

        /// <summary>
        /// 指定日時：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeAppointIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.Appoint.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時：年Indexの設定
        /// </summary>
        public Int32 GetAppointYearIndex()
        {

            Int32 Return = _Year.IndexOf(Drug.AppointTime.Year);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時：月Indexの設定
        /// </summary>
        public Int32 GetAppointMonthIndex()
        {

            Int32 Return = _Month.IndexOf(Drug.AppointTime.Month);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時：日Indexの設定
        /// </summary>
        public Int32 GetAppointDayIndex()
        {

            Int32 Return = _Day.IndexOf(Drug.AppointTime.Day);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時：時Indexの設定
        /// </summary>
        public Int32 GetAppointHourIndex()
        {

            Int32 Return = _Hour.IndexOf(Drug.AppointTime.Hour);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時：分Indexの設定
        /// </summary>
        public Int32 GetAppointMinuteIndex()
        {

            Int32 Return = -1;
            for (Int32 iLoop = 0; iLoop < _Minute.Count; iLoop++)
            {
                if (Drug.AppointTime.Minute <= _Minute[iLoop])
                {
                    Return = iLoop;
                    break;
                }
            }

            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 指定日時を作成
        /// </summary>
        public void SetAppointDateTime()
        {

            if (_Year != null
                && _Month != null
                && _Day != null
                && _Hour != null
                && _Minute != null
                && !AppointYearIndex.Equals(-1)
                && !AppointMonthIndex.Equals(-1)
                && !AppointDayIndex.Equals(-1)
                && !AppointHourIndex.Equals(-1)
                && !AppointMinuteIndex.Equals(-1))
            {

                Int32 Year = _Year[AppointYearIndex];
                Int32 Month = _Month[AppointMonthIndex];
                Int32 Day = _Day[AppointDayIndex];
                Int32 Hour = _Hour[AppointHourIndex];
                Int32 Minute = _Minute[AppointMinuteIndex];
                StringBuilder Str = new StringBuilder();

                Str.Append(Year.ToString()).Append("/").Append(Month.ToString()).Append("/").Append(Day.ToString())
                    .Append(" ").Append(Hour.ToString()).Append(":").Append(Minute.ToString());

                if (DateTime.TryParse(Str.ToString(), out DateTime Return))
                {
                    Drug.AppointTime = Return;
                }

                Str.Clear();
                Str = null;

            }

        }

        #endregion

        #region 時間毎

        /// <summary>
        /// 服用量Index：時間毎プロパティ
        /// </summary>
        public Int32 HourEachVolumeIndex
        {
            get { return _HourEachVolumeIndex; }
            set
            {
                _HourEachVolumeIndex = value;
                Drug.HourEach.Volume = _Volume[value];
            }
        }

        /// <summary>
        /// 設定時間Index：時間毎プロパティ
        /// </summary>
        public Int32 HourEachTimeIndex
        {
            get { return _HourEachTimeIndex; }
            set
            {
                _HourEachTimeIndex = value;
                Drug.HourEachTime = _HourEach[value];
            }
        }

        /// <summary>
        /// 服用量Index：時間毎
        /// </summary>
        private Int32 _HourEachVolumeIndex;

        /// <summary>
        /// 設定時間Index：時間毎
        /// </summary>
        private Int32 _HourEachTimeIndex;

        /// <summary>
        /// 時間毎：錠数Indexの設定
        /// </summary>
        /// <returns></returns>
        public Int32 GetVolumeHourEachIndex()
        {

            Int32 Return = _Volume.IndexOf(Drug.HourEach.Volume);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        /// <summary>
        /// 時間毎：設定時間Indexの設定
        /// </summary>
        public Int32 GetHourEachIndex()
        {

            Int32 Return = _HourEach.IndexOf(Drug.HourEachTime);
            if (Return.Equals(-1)) Return = 0;

            return Return;

        }

        #endregion

        #region Const

        /// <summary>
        /// 最小錠数
        /// </summary>
        private const Int32 MinVolume = 0;

        /// <summary>
        /// 最大錠数
        /// </summary>
        private const Int32 MaxVolume = 30;

        #endregion

        #region List

        /// <summary>
        /// 錠数List
        /// </summary>
        private List<Int32> _Volume;

        /// <summary>
        /// 年List
        /// </summary>
        private List<Int32> _Year;

        /// <summary>
        /// 月List
        /// </summary>
        private List<Int32> _Month;

        /// <summary>
        /// 日List
        /// </summary>
        private List<Int32> _Day;

        /// <summary>
        /// 時List
        /// </summary>
        private List<Int32> _Hour;

        /// <summary>
        /// 分List
        /// </summary>
        private List<Int32> _Minute;

        /// <summary>
        /// 時間毎List
        /// </summary>
        private List<Int32> _HourEach;

        /// <summary>
        /// 錠数Listの取得
        /// </summary>
        /// <returns>錠数List</returns>
        public List<Int32> GetVolumeList()
        {

            if (_Volume == null)
            {

                _Volume = new List<Int32>();

                for (Int32 iLoop = MinVolume; iLoop <= MaxVolume; iLoop++)
                    _Volume.Add(iLoop);

            }

            return _Volume;

        }

        /// <summary>
        /// 年Listの取得
        /// </summary>
        /// <returns>年List</returns>
        public List<Int32> GetYearList()
        {

            if (_Year == null)
            {

                Int32 Year = DateTime.Now.Year;
                _Year = new List<Int32>();

                for (Int32 iLoop = 0; iLoop <= 1; iLoop++)
                    _Year.Add(Year + iLoop);

            }

            return _Year;

        }

        /// <summary>
        /// 月Listの取得
        /// </summary>
        /// <returns>月List</returns>
        public List<Int32> GetMonthList()
        {

            if (_Month == null)
            {

                _Month = new List<Int32>();

                for (Int32 iLoop = 1; iLoop <= 12; iLoop++)
                    _Month.Add(iLoop);

            }

            return _Month;

        }

        /// <summary>
        /// 日Listの取得
        /// </summary>
        /// <returns>日List</returns>
        public List<Int32> GetDayList()
        {

            Int32 Year = _Year[AppointYearIndex];
            Int32 Month = _Month[AppointMonthIndex];

            if (_Day == null)
                _Day = new List<Int32>();

            _Day.Clear();

            for (Int32 iLoop = 1; iLoop <= DateTime.DaysInMonth(Year, Month); iLoop++)
                _Day.Add(iLoop);

            return _Day;

        }

        /// <summary>
        /// 時Listの取得
        /// </summary>
        /// <returns>時List</returns>
        public List<Int32> GetHourList()
        {

            if (_Hour == null)
                _Hour = new List<Int32>();

            _Hour.Clear();

            for (Int32 iLoop = 0; iLoop <= 23; iLoop++)
                _Hour.Add(iLoop);

            return _Hour;

        }

        /// <summary>
        /// 分Listの取得
        /// </summary>
        /// <returns>分List</returns>
        public List<Int32> GetMinuteList()
        {

            if (_Minute == null)
                _Minute = new List<Int32>();

            _Minute.Clear();

            for (Int32 iLoop = 0; iLoop < 60; iLoop += 5)
                _Minute.Add(iLoop);

            return _Minute;

        }

        /// <summary>
        /// 時間毎Listの取得
        /// </summary>
        /// <returns>分List</returns>
        public List<Int32> GetHourEachList()
        {

            if (_HourEach == null)
                _HourEach = new List<Int32>();

            _HourEach.Clear();

            for (Int32 iLoop = 1; iLoop <= 24; iLoop++)
                _HourEach.Add(iLoop);

            return _HourEach;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Detail(Int32 Index)
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;
            _SelectedIndex = Index;

            if (_SelectedIndex.Equals(-1))
            {
                Drug = new Class.Parameter.DrugParameter();
            }
            else
            {

                Drug = new Class.Parameter.DrugParameter()
                {
                    Name = _Parameter.DrugList[_SelectedIndex].Name,
                    Breakfast = _Parameter.DrugList[_SelectedIndex].Breakfast,
                    Lunch = _Parameter.DrugList[_SelectedIndex].Lunch,
                    Dinner = _Parameter.DrugList[_SelectedIndex].Dinner,
                    Sleep = _Parameter.DrugList[_SelectedIndex].Sleep,
                    ToBeTaken = _Parameter.DrugList[_SelectedIndex].ToBeTaken,
                    Appoint = _Parameter.DrugList[_SelectedIndex].Appoint,
                    AppointTime = _Parameter.DrugList[_SelectedIndex].AppointTime,
                    HourEach = _Parameter.DrugList[_SelectedIndex].HourEach,
                    HourEachTime = _Parameter.DrugList[_SelectedIndex].HourEachTime,
                    TotalVolume = _Parameter.DrugList[_SelectedIndex].TotalVolume,
                    PrescriptionAlarmVolume = _Parameter.DrugList[_SelectedIndex].PrescriptionAlarmVolume,
                    IsPrescriptionAlarm = _Parameter.DrugList[_SelectedIndex].IsPrescriptionAlarm,
                    Remarks = _Parameter.DrugList[_SelectedIndex].Remarks,
                    DrugTiming = _Parameter.DrugList[_SelectedIndex].DrugTiming
                };

            }

            if (Drug.AppointTime < DateTime.Now) Drug.AppointTime = DateTime.Now;

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Volume != null)
            {
                _Volume.Clear();
                _Volume = null;
            }

            if (_Year != null)
            {
                _Year.Clear();
                _Year = null;
            }

            if (_Month != null)
            {
                _Month.Clear();
                _Month = null;
            }

            if (_Day != null)
            {
                _Day.Clear();
                _Day = null;
            }

            if (_Hour != null)
            {
                _Hour.Clear();
                _Hour = null;
            }

            if (_Minute != null)
            {
                _Minute.Clear();
                _Minute = null;
            }

            if (_HourEach != null)
            {
                _HourEach.Clear();
                _HourEach = null;
            }

        }

    }
}
