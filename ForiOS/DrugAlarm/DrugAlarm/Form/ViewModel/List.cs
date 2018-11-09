using System;
using DrugAlarm.Common;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// List.ViewModel
    /// </summary>
    public class List : ViewModelBase, IDisposable
    {

        /// <summary>
        /// Model
        /// </summary>
        private Model.List _Model;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            _Model = new Model.List();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.ViewModel.List"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.ViewModel.List"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.ViewModel.List"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.ViewModel.List"/> was occupying.</remarks>
        public void Dispose()
        {
            _Model.Dispose();
            _Model = null;
        }

    }
}
