using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DrugAlarm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //言語選択
            if (Device.RuntimePlatform.Equals(Device.iOS) || Device.RuntimePlatform.Equals(Device.Android))
            {
                var CultureInfo = DependencyService.Get<Common.ILocalize>().GetCurrentCultureInfo();
                Resx.Resources.Culture = CultureInfo;
                DependencyService.Get<Common.ILocalize>().SetLocal(CultureInfo);
            }

            MainPage = new DrugAlarm.Form.View.List();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
