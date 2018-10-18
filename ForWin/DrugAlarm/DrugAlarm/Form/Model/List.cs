using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace DrugAlarm.Form.Model
{
    class List : IDisposable
    {

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

        }

        /// <summary>
        /// 薬一覧
        /// </summary>
        public List<DrugParameter> DrugList;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        /// <summary>
        /// ListBox.SelectedIndex
        /// </summary>
        public Int32 SelectedIndex = -1;

        /// <summary>
        /// 登録している薬の総数プロパティ
        /// </summary>
        public Int32 GetDrugCount
        {
            get
            {
                return _Parameter.DrugList.Count;
            }
        }

        /// <summary>
        /// 薬パラメータIndexプロパティ
        /// </summary>
        public Int32 DrugIndex
        {
            private get;
            set;
        }

        /// <summary>
        /// 薬名プロパティ
        /// </summary>
        public string DrugName
        {
            get
            {
                return _Parameter.DrugList[DrugIndex].Name;
            }
        }

        /// <summary>
        /// 服用タイミングプロパティ
        /// </summary>
        public string DrugTiming
        {
            get
            {
                return _Parameter.DrugList[DrugIndex].DrugTiming;
            }
        }

        /// <summary>
        /// 薬説明プロパティ
        /// </summary>
        public string DrugRemarks
        {
            get
            {
                return _Parameter.DrugList[DrugIndex].Remarks;
            }

        }

        /// <summary>
        /// 薬切れアラーム有無プロパティ
        /// </summary>
        public bool DrugIsPrescriptionAlarm
        {
            get
            {
                return _Parameter.DrugList[DrugIndex].IsPrescriptionAlarm;
            }
        }

        /// <summary>
        /// 設定コマンド
        /// </summary>
        public Common.DelegateCommand SettingCommand;

        /// <summary>
        /// 新規追加コマンド
        /// </summary>
        public Common.DelegateCommand AddDrugCommand;

        /// <summary>
        /// 編集コマンド
        /// </summary>
        public Common.DelegateCommand EditCommand;

        /// <summary>
        /// 削除コマンド
        /// </summary>
        public Common.DelegateCommand DeleteCommand;

        /// <summary>
        /// 服用コマンド
        /// </summary>
        public Common.DelegateCommand DrugMedicineCommand;

        /// <summary>
        /// 100msタイマ
        /// </summary>
        public DispatcherTimer Timer;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;
            DrugList = new List<DrugParameter>();

            /*
#if DEBUG
            _Parameter.DebugTest_AddDrug("AAA");
            _Parameter.DebugTest_AddDrug("BBB");
            _Parameter.DebugTest_AddDrug("CCC");
#endif
             */

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            DrugList.Clear();
            DrugList = null;

        }

        /// <summary>
        /// 薬一覧に追加
        /// </summary>
        /// <returns>薬パラメータIndex</returns>
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
        /// 選択している薬の削除
        /// </summary>
        public void DeleteDrug()
        {

            if (-1 < SelectedIndex && SelectedIndex < _Parameter.DrugList.Count)
            {
                _Parameter.DrugList.RemoveAt(SelectedIndex);
                _Parameter.Save();
                SelectedIndex = -1;
            }

        }

        /// <summary>
        /// 選択している薬の服用
        /// </summary>
        public void DrugMedicine()
        {

            if (-1 < SelectedIndex && SelectedIndex < _Parameter.DrugList.Count)
            {

                Class.Parameter.DrugParameter Drug = _Parameter.DrugList[SelectedIndex];

                if (Drug.ToBeTaken.IsDrug)
                {
                    Drug.TotalVolume -= Drug.ToBeTaken.Volume;
                    _Parameter.Save();
                }

            }

        }

    }
}
