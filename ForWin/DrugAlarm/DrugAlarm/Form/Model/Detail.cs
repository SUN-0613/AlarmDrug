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

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited;

        /// <summary>
        /// 対象薬
        /// </summary>
        public Class.Parameter.DrugParameter Drug;

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
        /// 最小錠数
        /// </summary>
        private const Int32 MinVolume = 0;

        /// <summary>
        /// 最大錠数
        /// </summary>
        private const Int32 MaxVolume = 30;

        /// <summary>
        /// 服用量Index：朝食
        /// </summary>
        private Int32 _BreakfastVolumeIndex;

        /// <summary>
        /// 服用量Index：昼食
        /// </summary>
        private Int32 _LunchVolumeIndex;

        /// <summary>
        /// 服用量Index：夕食
        /// </summary>
        private Int32 _DinnerVolumeIndex;

        /// <summary>
        /// 服用量Index：就寝前
        /// </summary>
        private Int32 _SleepVolumeIndex;

        /// <summary>
        /// 服用量Index：頓服
        /// </summary>
        private Int32 _ToBeTakenVolumeIndex;

        /// <summary>
        /// 服用量Index：指定時間
        /// </summary>
        private Int32 _AppointVolumeIndex;

        /// <summary>
        /// 服用量Index：時間毎
        /// </summary>
        private Int32 _HourEachVolumeIndex;

        /// <summary>
        /// 薬パラメータIndex
        /// -1は新規登録
        /// </summary>
        private Int32 _SelectedIndex;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

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
        /// new
        /// </summary>
        public Detail(Int32 Index)
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;
            _SelectedIndex = Index;

            if (-1 < Index && Index < _Parameter.DrugList.Count)
            {
                Drug = _Parameter.DrugList[Index];
            }
            else
            {
                Drug = new Class.Parameter.DrugParameter();
            }

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Volume == null)
            {
                _Volume.Clear();
                _Volume = null;
            }

            if (_Year == null)
            {
                _Year.Clear();
                _Year = null;
            }

            if (_Month == null)
            {
                _Month.Clear();
                _Month = null;
            }

            if (_Day == null)
            {
                _Day.Clear();
                _Day = null;
            }

            if (_Hour == null)
            {
                _Hour.Clear();
                _Hour = null;
            }

            if (_Minute == null)
            {
                _Minute.Clear();
                _Minute = null;
            }

        }

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
        /// 指定日時を作成
        /// </summary>
        public void SetAppointDateTime()
        {

            if (!AppointYearIndex.Equals(-1)
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
                DateTime Return;

                Str.Append(Year.ToString()).Append("/").Append(Month.ToString()).Append("/").Append(Day.ToString())
                    .Append(" ").Append(Hour.ToString()).Append(":").Append(Minute.ToString());

                if (DateTime.TryParse(Str.ToString(), out Return))
                {
                    Drug.AppointTime = Return;
                }

                Str.Clear();
                Str = null;

            }

        }

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

            _Parameter.Save();

        }

    }
}
