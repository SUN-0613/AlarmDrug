﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Information.View
    /// </summary>
    public partial class Information : ContentPage, IDisposable
    {

        /// <summary>
        /// Information.ViewModel
        /// </summary>
        private ViewModel.Information _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Information()
        {

            InitializeComponent();

            Class.UserControl.CallInformationCounter += 1;

            _ViewModel = new ViewModel.Information();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.Information"/>.
        /// The <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.Information"/> in an unusable
        /// state. After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.Information"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.Information"/> was occupying.</remarks>
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

            switch (e.PropertyName)
            {

                case "CallOK":

                    // 複数回Alarm.xamlとInformation.xamlが呼び出された時の対応
                    for (Int32 iLoop = 0; iLoop < Class.UserControl.CallInformationCounter * 2 - 1; iLoop++)
                    {
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(false);
                    }
                    Class.UserControl.CallInformationCounter = 0;

                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);

                    break;

                default:
                    break;

            }

        }

    }
}
