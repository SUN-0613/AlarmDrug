using System;
using System.Globalization;
using System.Threading;
using Foundation;
using DrugAlarm.Common;

[assembly: Xamarin.Forms.Dependency(typeof(DrugAlarm.iOS.Localize))]

namespace DrugAlarm.iOS
{

    /// <summary>
    /// 言語選択
    /// </summary>
    public class Localize : ILocalize
    {

        /// <summary>
        /// 言語設定
        /// </summary>
        /// <param name="CInfo">選択言語</param>
        public void SetLocal(CultureInfo CInfo)
        {
            Thread.CurrentThread.CurrentCulture = CInfo;
            Thread.CurrentThread.CurrentUICulture = CInfo;
        }

        /// <summary>
        /// 選択言語の取得
        /// </summary>
        /// <returns>The current culture info.</returns>
        public CultureInfo GetCurrentCultureInfo()
        {

            var NetLang = "ja";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var PLang = NSLocale.PreferredLanguages[0];
                NetLang = iOSToDotNetLanguage(PLang);
            }

            System.Globalization.CultureInfo CInfo = null;

            try
            {
                CInfo = new System.Globalization.CultureInfo(NetLang);
            }
            catch (CultureNotFoundException ex1)
            {

#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex1.Message);
#endif
                try
                {
                    string FallBack = ToDotNetFallbackLanguage(new PlatformCulture(NetLang));
                    CInfo = new System.Globalization.CultureInfo(NetLang);
                }
                catch (CultureNotFoundException ex2)
                {

#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex2.Message);
#endif

                    CInfo = new System.Globalization.CultureInfo("ja");
                }
            }

            return CInfo;

        }

        /// <summary>
        /// iOS言語を.Net言語に変換
        /// </summary>
        /// <returns>The OST o dot net language.</returns>
        /// <param name="iOSLang">I OSL ang.</param>
        private string iOSToDotNetLanguage(string iOSLang)
        {

            string NetLang = iOSLang;

            switch (iOSLang)
            {
                case "ms-MY":
                case "ms-SG":
                    NetLang = "ms";
                    break;

                case "gsw-CH":
                    NetLang = "de-CH";
                    break;
            }

            return NetLang;

        }

        /// <summary>
        /// .Net言語から戻す
        /// </summary>
        /// <returns>The dot net fallback language.</returns>
        /// <param name="PlatCulture">Plat culture.</param>
        private string ToDotNetFallbackLanguage(PlatformCulture PlatCulture)
        {

            string NetLang = PlatCulture.LanguageCode;

            switch (PlatCulture.LanguageCode)
            {
                case "pt":
                    NetLang = "pt-PT";
                    break;

                case "gsw":
                    NetLang = "de-CH";
                    break;
            }

            return NetLang;

        }

    }

}
