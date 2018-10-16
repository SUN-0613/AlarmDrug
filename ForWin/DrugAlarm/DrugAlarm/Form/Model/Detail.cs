using System;

namespace DrugAlarm.Form.Model
{

    /// <summary>
    /// Detail.Model
    /// </summary>
    class Detail : IDisposable
    {

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand CancelCommand;

        /// <summary>
        /// 保存コマンドプロパティ
        /// </summary>
        public Common.DelegateCommand SaveCommand;

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited;

        /// <summary>
        /// 対象薬
        /// </summary>
        public Class.Parameter.DrugParameter Drug;

        /// <summary>
        /// 薬パラメータIndex
        /// -1は新規登録
        /// </summary>
        private Int32 _SelectedIndex;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Class.Parameter _Parameter;

        /// <summary>
        /// new
        /// </summary>
        public Detail(Int32 Index)
        {

            _Parameter = (System.Windows.Application.Current as App).Parameter;
            _SelectedIndex = Index;

            if (-1 < Index && Index < _Parameter.DrugList.Count)
            {
                Drug = _Parameter.DrugList[Index];
            }
            else
            {
                Drug = new Class.Parameter.DrugParameter();
            }

            IsEdited = false;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
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

            if (_SelectedIndex == -1)
            {
                _Parameter.DrugList.Add(Drug);
            }

            _Parameter.Save();

        }



    }
}
