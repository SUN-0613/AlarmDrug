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
        /// 表示ページ
        /// </summary>
        private Page _MainPage = (Xamarin.Forms.Application.Current as App).MainPage;

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
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {
                default:
                    break;
            }

        }

    }
}
