﻿using System;
using System.Collections.Generic;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Information.View
    /// </summary>
    public class Information : IDisposable
    {

        #region 基底

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        #endregion

        #region List

        /// <summary>
        /// 表示薬名称一覧
        /// </summary>
        private List<Class.UserControl.MedicineInfo> _DrugList;

        /// <summary>
        /// 表示薬一覧取得
        /// </summary>
        /// <returns>The drug list.</returns>
        public List<Class.UserControl.MedicineInfo> GetDrugList()
        {

            if (_DrugList == null)
                _DrugList = new List<Class.UserControl.MedicineInfo>();

            _DrugList.Clear();

            _Parameter.PrescriptionList.ForEach(index =>
            {
                _DrugList.Add(new Class.UserControl.MedicineInfo(_Parameter.DrugList[index].Name, _Parameter.DrugList[index].TotalVolume, -1));
            });

            return _DrugList;

        }

        #endregion

        #region Command

        /// <summary>
        /// OKコマンド
        /// </summary>
        public Common.DelegateCommand OkCommand;

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {
            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter; 
        }

        /// <summary>
        /// 処理終了
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.Model.Information"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.Model.Information"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.Model.Information"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.Model.Information"/> was occupying.</remarks>
        public void Dispose()
        {

            if (_DrugList != null)
            {
                _DrugList.Clear();
                _DrugList = null;
            }

        }

    }
}
