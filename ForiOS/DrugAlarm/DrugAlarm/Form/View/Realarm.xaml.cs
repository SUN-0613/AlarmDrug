﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Realarm.View
    /// </summary>
    public partial class Realarm : ContentPage, IDisposable
    {

        /// <summary>
        /// Realarm.ViewModel
        /// </summary>
        private ViewModel.Realarm _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Realarm()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Realarm();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.Realarm"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.Realarm"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.Realarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.Realarm"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// ViewModel変更通知イベント
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            bool AlertAnswer;

            switch (e.PropertyName)
            {

                case "CallCancel":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    break;

                case "CallSave":
                    PagePop();
                    break;

                case "CallSkip":

                    AlertAnswer = await DisplayAlert(Resx.Resources.Detail_Title, _ViewModel.MakeSkipMessage(), Resx.Resources.DisplayAlert_Yes, Resx.Resources.DisplayAlert_No);

                    if (AlertAnswer)
                    {
                        PagePop();
                    }

                    break;

                default:
                    break;

            }

        }

        /// <summary>
        /// 表示ページを戻す
        /// </summary>
        private async void PagePop()
        {

            if (!Class.UserControl.IsWaitToRunRealarm)
            {
                await(Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(false);
            }
            else
            {
                Class.UserControl.IsWaitToRunRealarm = false;
            }
            await(Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);

        }

    }
}
