using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// History.Model
    /// </summary>
    public class History : IDisposable
    {

        #region Class

        /// <summary>
        /// 薬情報
        /// </summary>
        public class Drug : Common.ViewModelBase
        {

            /// <summary>
            /// 名称
            /// </summary>
            private string _Name = "";

            /// <summary>
            /// 名称プロパティ
            /// </summary>
            /// <value>The name.</value>
            public string Name
            {
                get { return _Name; }
                set
                {
                    if (!_Name.Equals(value))
                    {
                        _Name = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 錠
            /// </summary>
            private Int32 _Volume;

            /// <summary>
            /// 錠プロパティ
            /// </summary>
            /// <value>The colon.</value>
            public string Volume
            {
                get { return _Volume.ToString(); }
                set
                {
                    if (Int32.TryParse(value, out Int32 intValue)
                    {
                        _Volume = intValue;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// new
            /// </summary>
            public Drug()
            {
                Name = "";
                Volume = "0";
            }

            /// <summary>
            /// new
            /// </summary>
            /// <param name="name">Name.</param>
            /// <param name="volume">Volume.</param>
            public Drug(string name, Int32 volume)
            {
                Name = name;
                Volume = volume.ToString();
            }

        }

        /// <summary>
        /// 履歴情報
        /// </summary>
        public class HistoryInfo : Common.ViewModelBase, IDisposable
        {

            /// <summary>
            /// 服用時間
            /// </summary>
            private DateTime _Timer = DateTime.MaxValue;

            /// <summary>
            /// 服用時間プロパティ
            /// </summary>
            /// <value>The time.</value>
            public DateTime Timer
            {
                get { return _Timer; }
                set
                {
                    if (!_Timer.Equals(value))
                    {
                        _Timer = value;
                        CallPropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 薬一覧
            /// </summary>
            /// <value>The drug list.</value>
            public ObservableCollection<Drug> DrugList { get; set; }

            /// <summary>
            /// new
            /// </summary>
            public HistoryInfo()
            {
                DrugList = new ObservableCollection<Drug>();
            }

            /// <summary>
            /// 終了処理
            /// </summary>
            /// <remarks>Call <see cref="Dispose"/> when you are finished using the
            /// <see cref="T:DrugAlarm.Form.Model.History.HistoryInfo"/>. The <see cref="Dispose"/> method leaves the
            /// <see cref="T:DrugAlarm.Form.Model.History.HistoryInfo"/> in an unusable state. After calling
            /// <see cref="Dispose"/>, you must release all references to the
            /// <see cref="T:DrugAlarm.Form.Model.History.HistoryInfo"/> so the garbage collector can reclaim the memory
            /// that the <see cref="T:DrugAlarm.Form.Model.History.HistoryInfo"/> was occupying.</remarks>
            public void Dispose()
            {
                DrugList.Clear();
                DrugList = null;
            }

        }

        #endregion

        #region Command

        /// <summary>
        /// 戻るコマンド
        /// </summary>
        public Common.DelegateCommand ReturnCommand;

        #endregion

        #region パラメータ

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        /// <summary>
        /// 履歴一覧
        /// </summary>
        private List<HistoryInfo> _HistoryList;

        /// <summary>
        /// 履歴一覧取得
        /// </summary>
        /// <returns>The history list.</returns>
        public List<HistoryInfo> GetHistoryList()
        {

            if (_HistoryList == null)
            {
                _HistoryList = new List<HistoryInfo>();
            }

            _HistoryList.ForEach(Alarm =>
            {
                Alarm.Dispose();
                Alarm = null;
            });

            _HistoryList.Clear();

            _Parameter.AlarmHistory.ForEach(Alarm => 
            {

                HistoryInfo AddData = new HistoryInfo() 
                {
                    Timer = Alarm.Timer
                };

                Alarm.DrugList.ForEach(Drug => 
                {
                    AddData.DrugList.Add(new Drug()
                    {
                        Name = _Parameter.DrugList[Drug.Index].Name,
                        Volume = Drug.Volume.ToString()
                    }); 
                });

                _HistoryList.Add(AddData);
                 
            });

            return _HistoryList;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public History()
        {

            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.History"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.History"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.History"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.History"/> was occupying.</remarks>
        public void Dispose()
        {

            if (_HistoryList != null)
            {

                _HistoryList.ForEach(Alarm =>
                {
                    Alarm.Dispose();
                    Alarm = null;
                });

                _HistoryList.Clear();
                _HistoryList = null;
            }

        }

    }
}
