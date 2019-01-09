using System;
using System.Collections.Generic;

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
        public Int32 SelectedIndex;

        #endregion

        #region 薬プロパティ

        /// <summary>
        /// 薬一覧
        /// </summary>
        public List<DrugParameter> DrugList;

        /// <summary>
        /// 登録している薬の総数プロパティ
        /// </summary>
        /// <value>DrugList.Count</value>
        public Int32 GetDrugCount { get { return _Parameter.DrugList.Count; }}

        /// <summary>
        /// 薬パラメータIndexプロパティ
        /// </summary>
        /// <value>The index of the drug.</value>
        public Int32 DrugIndex { private get; set; }

        /// <summary>
        /// 薬名プロパティ
        /// </summary>
        /// <value>The name of the drug.</value>
        public string DrugName { get { return _Parameter.DrugList[DrugIndex].Name; }}

        /// <summary>
        /// 服用タイミングプロパティ
        /// </summary>
        /// <value>The drug timing.</value>
        public string DrugTiming { get { return _Parameter.DrugList[DrugIndex].DrugTiming; }}

        /// <summary>
        /// 薬説明プロパティ
        /// </summary>
        /// <value>The drug remarks.</value>
        public string DrugRemarks { get { return _Parameter.DrugList[DrugIndex].Remarks; }}

        /// <summary>
        /// 薬切れアラーム有無プロパティ
        /// </summary>
        /// <value><c>true</c> if drug is prescription alarm; otherwise, <c>false</c>.</value>
        public bool DrugIsPrescriptionAlarm { get { return _Parameter.DrugList[DrugIndex].IsPrescriptionAlarm; }}

        /// <summary>
        /// 頓服服用プロパティ
        /// </summary>
        /// <value><c>true</c> if drug is to be taken; otherwise, <c>false</c>.</value>
        public bool DrugIsToBeTaken { get { return _Parameter.DrugList[DrugIndex].ToBeTaken.IsDrug; }}

        /// <summary>
        /// 薬一覧に追加
        /// </summary>
        /// <returns>DrugList.MaxIndex</returns>
        public Int32 AddDrugList()
        {
            DrugList.Add(new DrugParameter());
            return DrugList.Count - 1;
        }

        /// <summary>
        /// 薬一覧クリア
        /// </summary>
        public void ClearDrugList()
        {
            DrugList.Clear();
        }

        /// <summary>
        /// SelectedIndexの整合性チェック
        /// </summary>
        /// <returns><c>true</c>OK<c>false</c>NG</returns>
        public bool IsOkSelectedIndex()
        {
            return (-1 < SelectedIndex && SelectedIndex < _Parameter.DrugList.Count);
        }

        /// <summary>
        /// 選択している薬の削除
        /// </summary>
        public void DeleteDrug()
        {

            if (IsOkSelectedIndex())
            {

                _Parameter.DeleteDrug(SelectedIndex);
                SelectedIndex = -1;

            }

        }

        /// <summary>
        /// 選択している薬の服用
        /// </summary>
        /// <returns><c>true</c>正常終了<c>false</c>異常終了</returns>
        public bool DrugMedicine()
        {

            bool Return = false;

            if (IsOkSelectedIndex())
            {

                Class.Parameter.DrugParameter Drug = _Parameter.DrugList[SelectedIndex];

                if (Drug.ToBeTaken.IsDrug)
                {

                    Drug.TotalVolume -= Drug.ToBeTaken.Volume;
                    _Parameter.Save(false);

                    Return = true;
                }

            }

            return Return;

        }

        #endregion

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
            DrugList = new List<DrugParameter>();

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

            DrugList.Clear();
            DrugList = null;

        }

        /// <summary>
        /// 次回アラームの日時取得
        /// </summary>
        /// <returns>The alarm.</returns>
        public string GetNextAlarmMessage()
        {
            if (!_Parameter.NextAlarm.Timer.Equals(DateTime.MaxValue))
            {
                return Resx.Resources.List_NextAlarmMessage + _Parameter.NextAlarm.Timer.ToString(Class.UserControl.DateTimeFormat);
            }
            else
            {
                return "";
            }
        }

    }
}
