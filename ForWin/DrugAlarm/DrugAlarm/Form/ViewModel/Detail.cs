using System;
using DrugAlarm.Common;
using DrugAlarm.Class;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Detail.xamlのViewModel
    /// </summary>
    class Detail : ViewModelBase, IDisposable
    {

        /// <summary>
        /// 新規追加FLG
        /// </summary>
        private bool IsNewDrug;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter;

        /// <summary>
        /// 対象薬
        /// </summary>
        private Parameter.DrugParameter _Drug;

        /// <summary>
        /// バインディングプロパティ一覧
        /// </summary>
        public BindProperty Bind;

        /// <summary>
        /// 薬名称
        /// </summary>
        public class BindProperty : ViewModelBase, IDisposable
        {

            /// <summary>
            /// 薬パラメータ
            /// </summary>
            private Parameter.DrugParameter _Drug;

            /// <summary>
            /// new
            /// </summary>
            /// <param name="Drug">薬パラメータ</param>
            public BindProperty(Parameter.DrugParameter Drug)
            {
                _Drug = Drug;
            }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name
            {
                get { return _Drug.Name; }
                set
                {
                    _Drug.Name = value;
                    CallPropertyChanged();
                }

            }

            /// <summary>
            /// 服用タイミング：朝食
            /// </summary>
            public bool IsBreakfast
            {
                get { return _Drug.Breakfast.IsDrug; }
                set
                {
                    _Drug.Breakfast.IsDrug = value;
                    CallPropertyChanged();
                }
            }

            /// <summary>
            /// 終了処理
            /// </summary>
            public void Dispose()
            {
                
            }

        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Index">Parameter.DrugList[Index]</param>
        public Detail(Int32 Index)
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;

            if (-1 < Index && Index < _Parameter.DrugList.Count)
            {
                _Drug = _Parameter.DrugList[Index];
                IsNewDrug = false;
            }
            else
            {
                _Drug = new Parameter.DrugParameter();
                IsNewDrug = true;
            }

            Bind = new BindProperty(_Drug);


        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            Bind.Dispose();
            Bind = null;
        }

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

            if (IsNewDrug)
            {
                _Parameter.DrugList.Add(_Drug);
            }

            _Parameter.Save();

        }

    }

}
