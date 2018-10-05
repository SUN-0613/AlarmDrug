using System;
using System.Collections.ObjectModel;
using System.Windows;
using DrugAlarm.Class;
using DrugAlarm.Base;

namespace DrugAlarm.Form
{
    class ListViewModel : ViewModelBase
    {

        /// <summary>
        /// 薬一覧
        /// </summary>
        public ObservableCollection<Parameter.DrugParameter> List { get; set; }

        /// <summary>
        /// ListBox.SelectedIndex
        /// </summary>
        private Int32 _SelectedIndex = -1;

        /// <summary>
        /// ListBox.SelectedIndexプロパティ
        /// </summary>
        public Int32 SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (!_SelectedIndex.Equals(value))
                {
                    _SelectedIndex = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter Parameter;

        /// <summary>
        /// new
        /// </summary>
        public ListViewModel()
        {

            Parameter = (System.Windows.Application.Current as App).Parameter;
            List = Parameter.DrugList;

#if DEBUG
            Parameter.DebugTest_AddDrug("TEST");
#endif

        }

        /// <summary>
        /// 設定画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallSetting(List View)
        {

            var form = new Setting
            {
                Owner = View
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 新規追加画面呼出
        /// </summary>
        /// <param name="View">呼出元画面</param>
        public void CallDetailForm(List View, bool IsNewDrug)
        {

            Int32 Index;

            if (IsNewDrug)
            {
                Index = -1;
            }
            else if (-1 < _SelectedIndex && _SelectedIndex < List.Count)
            {
                Index = _SelectedIndex;
            }
            else
            {
                return;
            }

            var form = new Detail(Index)
            {
                Owner = View
            };
            form.ShowDialog();

        }

        /// <summary>
        /// 薬削除
        /// </summary>
        public void DeleteDrug()
        {

            if (-1 < _SelectedIndex && _SelectedIndex < List.Count)
            {

                string AppName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                string Message = DrugAlarm.Properties.Resources.List_DeleteMessage.Replace("_DRUG_", List[_SelectedIndex].Name);

                if (MessageBox.Show(Message, AppName, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    List.RemoveAt(_SelectedIndex);
                    Parameter.Save();
                    SelectedIndex = -1;
                }

            }

        }

        /// <summary>
        /// 頓服服用
        /// </summary>
        public void DrugMedicine()
        {

            Parameter.DrugParameter Drug = List[_SelectedIndex];

            if (-1 < _SelectedIndex && _SelectedIndex < List.Count)
            {
                if (Drug.ToBeTaken.IsDrug)
                {
                    Drug.TotalVolume -= Drug.ToBeTaken.Volume;
                    Parameter.Save();
                }
            }

        }

    }
}
