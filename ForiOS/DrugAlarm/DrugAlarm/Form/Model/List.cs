using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// List.Model
    /// </summary>
    public class List : IDisposable
    {

        #region パラメータ

        /// <summary>
        /// DrugParameterより一部抜粋
        /// </summary>
        public class DrugParameter
        {

            /// <summary>
            /// 名称
            /// </summary>
            public string Name;

            /// <summary>
            /// :
            /// </summary>
            public string Colon;

            /// <summary>
            /// 服用タイミング
            /// </summary>
            public string DrugTiming;

            /// <summary>
            /// 備考
            /// </summary>
            public string Remarks;

            /// <summary>
            /// 薬切れFLG
            /// </summary>
            public bool IsPrescriptionAlarm;

            /// <summary>
            /// 頓服服用
            /// </summary>
            public bool IsToBeTaken;

            /// <summary>
            /// new
            /// </summary>
            public DrugParameter(string Name = "", string Colon = ":", string DrugTiming = "", string Remarks = "", bool IsPrescriptionAlarm = false, bool IsToBeTaken = false)
            {
                this.Name = Name;
                this.Colon = Colon;
                this.DrugTiming = DrugTiming;
                this.Remarks = Remarks;
                this.IsPrescriptionAlarm = IsPrescriptionAlarm;
                this.IsToBeTaken = IsToBeTaken;
            }

        }

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        /// <summary>
        /// List.SelectedIndex
        /// </summary>
        public DrugParameter SelectedItem;

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.List"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.List"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.List"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.List"/> was occupying.</remarks>
        public void Dispose()
        {

        }

    }
}
