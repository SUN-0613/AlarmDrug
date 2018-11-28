using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Listコードビハインド
    /// </summary>
    public partial class List : ContentPage, IDisposable
    {

        /// <summary>
        /// ViewModel
        /// </summary>
        private ViewModel.List _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public List()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.List();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.List"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.List"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.List"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.List"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool AlertAnswer;

            switch (e.PropertyName)
            {

                case "CallSetting":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Setting());
                    break;

                case "CallAddDrug":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Detail(-1));
                    break;

                case "CallEditDrug":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Detail(_ViewModel.SelectedIndex));
                    break;

                case "CallDeleteDrug":

                    AlertAnswer = await DisplayAlert(Resx.Resources.List_Title, _ViewModel.MakeDeleteDrugMessage(), Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

                    if (AlertAnswer)
                    {
                        _ViewModel.DeleteDrug();
                    }
                    break;

                case "CallDrugMedicine":
                    if (_ViewModel.DrugMedicine())
                    {
                        await DisplayAlert(Resx.Resources.List_Title, _ViewModel.MakeDrugMedicineMessage(), Resx.Resources.DisplayAlert_OK);
                    }
                    break;

                default:
                    break;

            }

        }

    }

}
