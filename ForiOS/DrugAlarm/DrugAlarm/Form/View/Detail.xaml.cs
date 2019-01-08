using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Detail.Codebehind
    /// </summary>
    public partial class Detail : ContentPage, IDisposable
    {

        /// <summary>
        /// Detail.ViewModel
        /// </summary>
        private ViewModel.Detail _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Index">DrugParameter.DrugList.Index</param>
        public Detail(Int32 DrugIndex = -1)
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Detail(DrugIndex);
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

            _ViewModel.EditStart = true;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.Detail"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.Detail"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.Detail"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.Detail"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// ViewModel.プロパティ変更通知イベント
        /// </summary>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool AlertAnswer;

            switch (e.PropertyName)
            {

                case "CallCancel":  

                    if (_ViewModel.IsEdited)
                    {

                        AlertAnswer = await DisplayAlert(Resx.Resources.Detail_Title, Resx.Resources.Detail_CancelMessage, Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

                        if (AlertAnswer)
                        {
                            _ViewModel.Initialize();
                            await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                        }

                    }
                    else
                    {
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    }

                    break;

                case "CallSave":

                    _ViewModel.Save();
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);

                    /*
                    AlertAnswer = await DisplayAlert(Resx.Resources.Detail_Title, _ViewModel.MakeSaveMessage(), Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

                    if (AlertAnswer)
                    {
                        _ViewModel.Save();
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    }
                    */

                    break;

                case "CallTotalVolume":
                    await DisplayAlert(Resx.Resources.Detail_Title, _ViewModel.MakeTotalVolumeOverMessage(), Resx.Resources.DisplayAlert_OK);
                    break;

                case "CallAlarmVolume":
                    await DisplayAlert(Resx.Resources.Detail_Title, _ViewModel.MakeAlarmVolumeOverMessage(), Resx.Resources.DisplayAlert_OK);
                    break;

                default:
                    break;

            }

        }

    }
}
