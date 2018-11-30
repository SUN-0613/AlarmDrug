using System.ComponentModel;
using Xamarin.Forms;

namespace DrugAlarm.Form.View
{

    /// <summary>
    /// Alarm.View
    /// </summary>
    public partial class Alarm : ContentPage
    {

        /// <summary>
        /// Alarm.ViewModel
        /// </summary>
        private ViewModel.Alarm _ViewModel;

        /// <summary>
        /// new
        /// </summary>
        public Alarm()
        {

            InitializeComponent();

            _ViewModel = new ViewModel.Alarm();
            BindingContext = _ViewModel;

            _ViewModel.PropertyChanged += OnPropertyChanged;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:DrugAlarm.Form.View.Alarm"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="T:DrugAlarm.Form.View.Alarm"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="T:DrugAlarm.Form.View.Alarm"/> so the garbage collector can reclaim the memory that the
        /// <see cref="T:DrugAlarm.Form.View.Alarm"/> was occupying.</remarks>
        public void Dispose()
        {

            _ViewModel.PropertyChanged -= OnPropertyChanged;

            _ViewModel.Dispose();
            _ViewModel = null;

        }

        /// <summary>
        /// ViewModel変更通知イベント
        /// </summary>
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallOK":

                    //残り錠の計算、次回アラーム設定
                    if (_ViewModel.TakeMedicine())
                    {
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Information());
                    }
                    else
                    {
                        await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PopAsync(true);
                    }

                    break;

                case "CallRealarm":
                    await (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Realarm());
                    break;

                default:
                    break;

            }

        }

    }
}
