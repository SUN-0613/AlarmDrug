using System;
using System.Collections.ObjectModel;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Setting.ViewModel
    /// </summary>
    public class Setting : Common.ViewModelBase, IDisposable
    {

        /// <summary>
        /// Setting.Model
        /// </summary>
        private Model.Setting _Model;

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            _Model = new Model.Setting();

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

            _Model.Dispose();
            _Model = null;

        }

    }
}
