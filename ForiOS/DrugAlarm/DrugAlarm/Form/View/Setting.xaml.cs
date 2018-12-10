using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Setting.Codebehind
    /// </summary>
    public partial class Setting : ContentPage, IDisposable
    {

        /// <summary>
        /// Setting.ViewModel
        /// </summary>
        private ViewModel.Setting _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Setting()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Setting();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.Setting"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.Setting"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.Setting"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.Setting"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// ViewModel.プロパティ変更通知イベント
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool AlertAnswer; 

            switch (e.PropertyName)
            {

                case "CallCancel":

                    if (_ViewModel.IsEdited)
                    {

                        AlertAnswer = await DisplayAlert(Resx.Resources.Setting_Title, Resx.Resources.Setting_CancelMessage, Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

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
                    AlertAnswer = await DisplayAlert(Resx.Resources.Setting_Title, Resx.Resources.Setting_SaveMessage, Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

                    if (AlertAnswer)
                    {
                        _ViewModel.Save();
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    }
                    */

                    break;

                default:
                    break;
            }

        }

    }
}
