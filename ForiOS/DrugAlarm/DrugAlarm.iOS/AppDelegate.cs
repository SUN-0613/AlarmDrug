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

                (Xamarin.Forms.Application.Current as App).IsBackground = true;
                (Xamarin.Forms.Application.Current as App).TimerStop();

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

                (Xamarin.Forms.Application.Current as App).IsBackground = false;
                (Xamarin.Forms.Application.Current as App).TImerStart();

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
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        /// <summary>
        /// バックグラウンドフェッチでの処理
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="completionHandler">Completion handler.</param>
        [Export("application:performFetchWithCompletionHandler:")]
        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {

            try
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(nameof(PerformFetch));
#endif 

                (Xamarin.Forms.Application.Current as App).TimerCheck();

            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif 
            }
            finally
            {
                completionHandler(UIBackgroundFetchResult.NewData);
            }

        }

    }
}
