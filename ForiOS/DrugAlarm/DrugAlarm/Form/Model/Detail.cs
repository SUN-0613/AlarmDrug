using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Detail.Model
    /// </summary>
    public class Detail : IDisposable
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
        /// キャンセルコマンド
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンド
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

            if (_SelectedIndex.Equals(-1))
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
        /// 服用量Index
        /// 朝食プロパティ
        /// </summary>
        /// <value>The index of the breakfast volume.</value>
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
        /// 服用量Index
        /// 朝食
        /// </summary>
        private Int32 _BreakfastVolumeIndex;

        /// <summary>
        /// 朝食
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume breakfast index.</returns>
        public Int32 GetVolumeBreakfastIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.Breakfast.Volume);
        }

        #endregion

        #region 昼食

        /// <summary>
        /// 服用量Index
        /// 昼食プロパティ
        /// </summary>
        /// <value>The index of the Lunch volume.</value>
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
        /// 服用量Index
        /// 昼食
        /// </summary>
        private Int32 _LunchVolumeIndex;

        /// <summary>
        /// 昼食
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume Lunch index.</returns>
        public Int32 GetVolumeLunchIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.Lunch.Volume);
        }

        #endregion

        #region 夕食

        /// <summary>
        /// 服用量Index
        /// 夕食プロパティ
        /// </summary>
        /// <value>The index of the Dinner volume.</value>
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
        /// 服用量Index
        /// 夕食
        /// </summary>
        private Int32 _DinnerVolumeIndex;

        /// <summary>
        /// 夕食
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume Lunch index.</returns>
        public Int32 GetVolumeDinnerIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.Dinner.Volume);
        }

        #endregion

        #region 就寝前

        /// <summary>
        /// 服用量Index
        /// 就寝前プロパティ
        /// </summary>
        /// <value>The index of the Dinner volume.</value>
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
        /// 服用量Index
        /// 就寝前
        /// </summary>
        private Int32 _SleepVolumeIndex;

        /// <summary>
        /// 就寝前
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume Lunch index.</returns>
        public Int32 GetVolumeSleepIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.Sleep.Volume);
        }

        #endregion

        #region 頓服

        /// <summary>
        /// 服用量Index
        /// 頓服プロパティ
        /// </summary>
        /// <value>The index of the ToBeTaken volume.</value>
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
        /// 服用量Index
        /// 頓服
        /// </summary>
        private Int32 _ToBeTakenVolumeIndex;

        /// <summary>
        /// 頓服
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume ToBeTaken index.</returns>
        public Int32 GetVolumeToBeTakenIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.ToBeTaken.Volume);
        }

        #endregion

        #region 指定時間

        /// <summary>
        /// 服用量Index
        /// 指定時間プロパティ
        /// </summary>
        /// <value>The index of the Appoint volume.</value>
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
        /// 服用量Index
        /// 指定時間
        /// </summary>
        private Int32 _AppointVolumeIndex;

        /// <summary>
        /// 年Index
        /// 指定時間
        /// </summary>
        public Int32 AppointYearIndex;

        /// <summary>
        /// 月Index
        /// 指定時間
        /// </summary>
        public Int32 AppointMonthIndex;

        /// <summary>
        /// 日Index
        /// 指定時間
        /// </summary>
        public Int32 AppointDayIndex;

        /// <summary>
        /// 時Index
        /// 指定時間
        /// </summary>
        public Int32 AppointHourIndex;

        /// <summary>
        /// 分Index
        /// 指定時間
        /// </summary>
        public Int32 AppointMinuteIndex;

        /// <summary>
        /// 指定時間
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume Appoint index.</returns>
        public Int32 GetVolumeAppointIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.Appoint.Volume);
        }

        /// <summary>
        /// 指定時間
        /// 現在設定値より年Indexを取得
        /// </summary>
        /// <returns>The appoint year index.</returns>
        public Int32 GetAppointYearIndex()
        {
            return new Class.Method().GetDefaultIndex(_Year, Drug.AppointTime.Year);
        }

        /// <summary>
        /// 指定時間
        /// 現在設定値より月Indexを取得
        /// </summary>
        /// <returns>The appoint month index.</returns>
        public Int32 GetAppointMonthIndex()
        {
            return new Class.Method().GetDefaultIndex(_Month, Drug.AppointTime.Month);
        }

        /// <summary>
        /// 指定時間
        /// 現在設定値より日Indexを取得
        /// </summary>
        /// <returns>The appoint day index.</returns>
        public Int32 GetAppointDayIndex()
        {
            return new Class.Method().GetDefaultIndex(_Day, Drug.AppointTime.Day);
        }

        /// <summary>
        /// 指定時間
        /// 現在設定値より時Indexを取得
        /// </summary>
        /// <returns>The appoint hour index.</returns>
        public Int32 GetAppointHourIndex()
        {
            return new Class.Method().GetDefaultIndex(_Hour, Drug.AppointTime.Hour);
        }

        /// <summary>
        /// 指定時間
        /// 現在設定値より分Indexを取得
        /// </summary>
        /// <returns>The appoint minute index.</returns>
        public Int32 GetAppointMinuteIndex()
        {
            return new Class.Method().GetDefaultIndex(_Minute, Drug.AppointTime.Minute);
        }

        /// <summary>
        /// 指定日時を作成
        /// </summary>
        public void SetAppointDateTime()
        {

            Class.Method method = new Class.Method();

            if (method.IsOkListStatus(_Year, AppointYearIndex)
                && method.IsOkListStatus(_Month, AppointMonthIndex)
                && method.IsOkListStatus(_Day, AppointDayIndex)
                && method.IsOkListStatus(_Hour, AppointHourIndex)
                && method.IsOkListStatus(_Month, AppointMonthIndex))
            {

                Drug.AppointTime = method.ConvertToDateTime(_Year[AppointYearIndex], _Month[AppointMonthIndex], _Day[AppointDayIndex]
                                                            , _Hour[AppointHourIndex], _Minute[AppointMinuteIndex], 0, Drug.AppointTime);

            }

        }

        #endregion

        #region 時間毎

        /// <summary>
        /// 服用量Index
        /// 時間毎プロパティ
        /// </summary>
        /// <value>The index of the HourEach volume.</value>
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
        /// 服用量Index
        /// 時間毎
        /// </summary>
        private Int32 _HourEachVolumeIndex;

        /// <summary>
        /// 設定時間Index
        /// 時間毎
        /// </summary>
        /// <value>The index of the hour each time.</value>
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
        /// 設定時間Index
        /// 時間毎
        /// </summary>
        private Int32 _HourEachTimeIndex;

        /// <summary>
        /// 時間毎
        /// 現在設定値より錠数Indexを取得
        /// </summary>
        /// <returns>The volume HourEach index.</returns>
        public Int32 GetVolumeHourEachIndex()
        {
            return new Class.Method().GetDefaultIndex(_Volume, Drug.HourEach.Volume);
        }

        /// <summary>
        /// 時間毎
        /// 現在設定値より設定時間Indexを取得
        /// </summary>
        /// <returns>The hour each index.</returns>
        public Int32 GetHourEachIndex()
        {
            return new Class.Method().GetDefaultIndex(_HourEach, Drug.HourEachTime);
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
        /// 錠数Listの作成
        /// </summary>
        /// <returns>The volume list.</returns>
        public List<Int32> GetVolumeList()
        {

            if (_Volume == null)
            {

                _Volume = new List<Int32>();

                for (Int32 iLoop = MinVolume; iLoop <= MaxVolume; iLoop++)
                {
                    _Volume.Add(iLoop);
                }

            }

            return _Volume;

        }

        /// <summary>
        /// 年Listの作成
        /// </summary>
        /// <returns>The year list.</returns>
        public List<Int32> GetYearList()
        {

            if (_Year == null)
            {

                Int32 Year = DateTime.Now.Year;
                _Year = new List<Int32>();

                for (Int32 iLoop = 0; iLoop <= 1; iLoop++)
                {
                    _Year.Add(Year + iLoop);
                }

            }

            return _Year;

        }

        /// <summary>
        /// 月Listの作成
        /// </summary>
        /// <returns>The month list.</returns>
        public List<Int32> GetMonthList()
        {

            if (_Month == null)
            {

                _Month = new List<Int32>();

                for (Int32 iLoop = 1; iLoop <= 12; iLoop++)
                {
                    _Month.Add(iLoop);
                }

            }

            return _Month;

        }

        /// <summary>
        /// 日Listの作成
        /// </summary>
        /// <returns>The day list.</returns>
        public List<Int32> GetDayList()
        {

            Int32 Year = _Year[AppointYearIndex];
            Int32 Month = _Month[AppointMonthIndex];

            if (_Day == null)
            {

                _Day = new List<Int32>();
            }

            _Day.Clear();

            for (Int32 iLoop = 1; iLoop <= DateTime.DaysInMonth(Year, Month); iLoop++)
            {
                _Day.Add(iLoop);
            }

            return _Day;

        }

        /// <summary>
        /// 時Listの作成
        /// </summary>
        /// <returns>The hour list.</returns>
        public List<Int32> GetHourList()
        {

            if (_Hour == null)
            {
                _Hour = new List<Int32>();

                for (Int32 iLoop = 1; iLoop < 24; iLoop++)
                {
                    _Hour.Add(iLoop);
                }

            }

            return _Hour;

        }

        /// <summary>
        /// 分Listの作成
        /// </summary>
        /// <returns>The minute list.</returns>
        public List<Int32> GetMinuteList()
        {

            if (_Minute == null)
            {
                _Minute = new List<Int32>();

                for (Int32 iLoop = 0; iLoop < 60; iLoop += 5)
                {
                    _Minute.Add(iLoop);
                }

            }

            return _Minute;

        }

        /// <summary>
        /// 時間毎Listの取得
        /// </summary>
        /// <returns>The hour each list.</returns>
        public List<Int32> GetHourEachList()
        {

            if (_HourEach == null)
            {

                _HourEach = new List<Int32>();

                for (Int32 iLoop = 1; iLoop <= 24; iLoop++)
                {
                    _HourEach.Add(iLoop);
                }

            }

            return _HourEach;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Index">DrugParameter.DrugList.Index</param>
        public Detail(Int32 Index)
        {

            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
            _SelectedIndex = Index;

            if (_SelectedIndex.Equals(-1))
            {
                //新規追加
                Drug = new Class.Parameter.DrugParameter();
            }
            else
            {
                //修正
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
                    Remarks = _Parameter.DrugList[_SelectedIndex].Remarks,
                    DrugTiming = _Parameter.DrugList[_SelectedIndex].DrugTiming
                };

                if (Drug.AppointTime < DateTime.Now)
                {
                    Drug.AppointTime = DateTime.Now;
                }

                IsEdited = false;

            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.Detail"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.Detail"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.Detail"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.Detail"/> was occupying.</remarks>
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
