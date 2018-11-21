using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Realarm.ViewModel
    /// </summary>
    public class Realarm : Common.ViewModelBase, IDisposable
    {

        #region 基底

        /// <summary>
        /// Realarm.Model
        /// </summary>
        private Model.Realarm _Model;

        #endregion

        #region BindingProperty

        /// <summary>
        /// 分Listプロパティ
        /// </summary>
        /// <value>The alarm minute.</value>
        public ObservableCollection<string> AlarmMinute { get; set; }

        /// <summary>
        /// 分Indexプロパティ
        /// </summary>
        /// <value>The index of the alarm minute.</value>
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

        #region CommandProperty

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
                        () => 
                            { 
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
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.Realarm"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.Realarm"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Realarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.Realarm"/> was occupying.</remarks>
        public void Dispose()
        {

            if (AlarmMinute != null)
            {
                AlarmMinute.Clear();
                AlarmMinute = null;
            }

            _Model.Dispose();
            _Model = null;

        }

    }
}
