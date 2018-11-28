using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Realarm.ViewModel
    /// </summary>
    class Realarm : Common.ViewModelBase, IDisposable
    {

        #region プロパティ

        /// <summary>
        /// 分Listプロパティ
        /// </summary>
        public ObservableCollection<string> AlarmMinute { get; set; }

        /// <summary>
        /// 分設定プロパティ
        /// </summary>
        public Int32 AlarmMinuteIndex
        {
            get { return _Model.AlarmMinuteIndex; }
            set
            {
                if (!_Model.AlarmMinuteIndex.Equals(value))
                {
                    _Model.AlarmMinuteIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        #endregion

        #region コマンド

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
                        () => {
                            _Model.Save();
                            CallPropertyChanged("CallSave");
                        },
                        () => true);
                }

                return _Model.SaveCommand;

            }
        }

        #endregion

        /// <summary>
        /// Realarm.Model
        /// </summary>
        private Model.Realarm _Model;

        /// <summary>
        /// new
        /// </summary>
        public Realarm()
        {

            _Model = new Model.Realarm();

            AlarmMinute = new ObservableCollection<string>();

            _Model.GetAlarmMinuteList().ForEach(Minute => 
            {
                AlarmMinute.Add(Minute.ToString("00"));
            });

            AlarmMinuteIndex = _Model.GetAlarmMinuteIndex();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            AlarmMinute.Clear();
            AlarmMinute = null;

            _Model.Dispose();
            _Model = null;

        }

    }
}
