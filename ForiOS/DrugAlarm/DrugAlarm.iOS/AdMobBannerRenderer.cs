using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Google.MobileAds;
using UIKit;
using CoreGraphics;
using DrugAlarm;
using DrugAlarm.AdMob;
using DrugAlarm.iOS;

[assembly: ExportRenderer(typeof(Banner), typeof(AdMobBannerRenderer))]
namespace DrugAlarm.iOS
{

    /// <summary>
    /// Ad mob banner renderer.
    /// </summary>
    public class AdMobBannerRenderer : ViewRenderer
    {

        /// <summary>
        /// AdMob.広告ユニットID
        /// </summary>
        /// <remarks>
        /// テスト用:ca-app-pub-3940256099942544/2934735716
        /// 本番用:ca-app-pub-5716768910200232/5019034379
        /// </remarks>
        private const string adUnitID = "ca-app-pub-5716768910200232/5019034379";

        /// <summary>
        /// ViewControl
        /// </summary>
        private UIViewController _ViewControl = null;

        /// <summary>
        /// AdMob.Banner
        /// </summary>
        private BannerView _AdMobBanner;

        /// <summary>
        /// The view on screen.
        /// </summary>
        private bool _ViewOnScreen;

        /// <summary>
        /// Ons the element changed.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {

            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            if (e.OldElement == null)
            {

                foreach (UIWindow uiWindow in UIApplication.SharedApplication.Windows)
                {

                    if (uiWindow.RootViewController != null)
                    {
                        _ViewControl = uiWindow.RootViewController;
                    }

                }

                _AdMobBanner = new BannerView(AdSizeCons.Banner, new CGPoint(-10, 0))
                {
                    AdUnitID = adUnitID,
                    RootViewController = _ViewControl
                };

                _AdMobBanner.AdReceived += (sender, args) =>
                {

                    if (!_ViewOnScreen)
                    {
                        AddSubview(_AdMobBanner);
                    }

                    _ViewOnScreen = true;

                };

                var request = Request.GetDefaultRequest();

                _AdMobBanner.LoadRequest(request);
                SetNativeControl(_AdMobBanner);

            }


        }

        public AdMobBannerRenderer()
        {
        }
    }
}
