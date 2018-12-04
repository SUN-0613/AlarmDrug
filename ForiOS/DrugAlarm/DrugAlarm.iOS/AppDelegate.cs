using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace DrugAlarm.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        /// <summary>
        /// バックグラウンド時のタスクID
        /// </summary>
        private nint _TaskId = 0;

        /// <summary>
        /// バックグラウンド処理開始
        /// </summary>
        /// <param name="uiApplication">User interface application.</param>
        public override void DidEnterBackground(UIApplication uiApplication)
        {

            base.DidEnterBackground(uiApplication);

#if DEBUG
            System.Diagnostics.Debug.WriteLine(nameof(DidEnterBackground));
#endif 

            try
            {

                if (!_TaskId.Equals(0))
                    return;

                _TaskId = UIApplication.SharedApplication.BeginBackgroundTask(() => { });
                (Xamarin.Forms.Application.Current as App).IsBackground = true;

            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif 
            }

        }

        /// <summary>
        /// フォアグラウンド処理開始
        /// </summary>
        /// <param name="uiApplication">User interface application.</param>
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);

#if DEBUG
            System.Diagnostics.Debug.WriteLine(nameof(OnActivated));
#endif 

            try
            {

                if (_TaskId.Equals(0))
                    return;

                UIApplication.SharedApplication.EndBackgroundTask(_TaskId);
                _TaskId = 0;
                (Xamarin.Forms.Application.Current as App).IsBackground = false;

            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif 
            }

        }

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
