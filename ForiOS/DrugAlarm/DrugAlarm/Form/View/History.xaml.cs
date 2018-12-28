using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// History.View
    /// </summary>
    public partial class History : ContentPage, IDisposable
    {

        /// <summary>
        /// The view model.History
        /// </summary>
        private ViewModel.History _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public History()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.History();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.History"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.History"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.History"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.History"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }


        /// <summary>
        /// Page表示イベント
        /// </summary>
        private void OnAppearing(object sender, EventArgs e)
        {
            if (_ViewModel != null)
            {
                _ViewModel.ShowList();
            }
        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallReturn":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    break;

                default:
                    break;

            }

        }

    }
}
