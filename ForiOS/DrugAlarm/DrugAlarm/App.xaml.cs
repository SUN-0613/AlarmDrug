using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DrugAlarm.Class;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DrugAlarm
{

    /// <summary>
    /// Appクラス
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        /// パラメータプロパティ
        /// </summary>
        /// <value>The parameter.</value>
        public Parameter Parameter { get; set; }

        /// <summary>
        /// new
        /// </summary>
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

            Parameter = new Parameter();

            MainPage = new DrugAlarm.Form.View.List();
        }

        /// <summary>
        /// Ons the start.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Ons the sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Ons the resume.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }

    }
}
