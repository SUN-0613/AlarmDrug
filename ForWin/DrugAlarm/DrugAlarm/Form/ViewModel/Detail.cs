using System;

namespace DrugAlarm.Form.ViewModel
{

    /// <summary>
    /// Detail.xamlのViewModel
    /// </summary>
    class Detail : Common.ViewModelBase, IDisposable
    {

        /// <summary>
        /// Detail.Model
        /// </summary>
        private Model.Detail _Model;

        /// <summary>
        /// 編集したか
        /// </summary>
        public bool IsEdited
        {
            get
            {
                return _Model.IsEdited;
            }
            set
            {
                _Model.IsEdited = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 朝食服用FLGプロパティ
        /// </summary>
        public bool IsBreakfast
        {
            get
            {
                return _Model.Drug.Breakfast.IsDrug;
            }
            set
            {
                _Model.Drug.Breakfast.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 昼食服用FLGプロパティ
        /// </summary>
        public bool IsLunch
        {
            get
            {
                return _Model.Drug.Lunch.IsDrug;
            }
            set
            {
                _Model.Drug.Lunch.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 夕食服用FLGプロパティ
        /// </summary>
        public bool IsDinner
        {
            get
            {
                return _Model.Drug.Dinner.IsDrug;
            }
            set
            {
                _Model.Drug.Dinner.IsDrug = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 食事時の服用タイミング
        /// </summary>
        /// <remarks>朝昼夕ともに同設定のため、朝のみで判断</remarks>
        public Class.Parameter.DrugParameter.KindTiming MealTiming
        {
            get
            {
                return _Model.Drug.Breakfast.Kind;
            }
            set
            {
                _Model.Drug.Breakfast.Kind = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Index">Parameter.DrugList[Index]</param>
        public Detail(Int32 Index)
        {
            _Model = new Model.Detail(Index);
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _Model.Dispose();
            _Model = null;
        }

        /// <summary>
        /// キャンセルコマンドプロパティ
        /// </summary>
        public Common.DelegateCommand CancelCommand
        {
            get
            {

                if (_Model.CancelCommand == null)
                {
                    _Model.CancelCommand = new Common.DelegateCommand(
                        () => { CallPropertyChanged("CallCancel"); },
                        () => true);
                }

                return _Model.CancelCommand;

            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            _Model.Initialize();
        }

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
                        () => { CallPropertyChanged("CallSave"); },
                        () => true);
                }

                return _Model.SaveCommand;

            }
        }

        /// <summary>
        /// 保存確認メッセージ作成
        /// </summary>
        /// <returns>保存確認メッセージ</returns>
        public string MakeSaveMessage()
        {
            return DrugAlarm.Properties.Resources.Detail_SaveMessage.Replace("_DRUG_", _Model.Drug.Name);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            _Model.Save();
        }

    }

}
