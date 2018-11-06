using System;
using System.Threading;
using System.Globalization;
using Xamarin.Forms;
using DrugAlarm.Common;

[assembly:Xamarin.Forms.Dependency(typeof(DrugAlarm.Droid.Localize))]

namespace DrugAlarm.Droid
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
            string NetLang = "ja";
            var AndroidLocal = Java.Util.Locale.Default;

            NetLang = AndroidToDotNetLanguage(AndroidLocal.ToString().Replace("_", "-"));
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
                    CInfo = new System.Globalization.CultureInfo(FallBack);
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
        /// Android言語を.Net言語に変換
        /// </summary>
        /// <returns>The OST o dot net language.</returns>
        /// <param name="iOSLang">I OSL ang.</param>
        private string AndroidToDotNetLanguage(string AndroidLang)
        {

            string NetLang = AndroidLang;

            switch (AndroidLang)
            {
                case "ms-BN":
                case "ms-MY":
                case "ms-SG":
                    NetLang = "ms";
                    break;

                case "in-ID":
                    NetLang = "id-ID";
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
                case "gsw":
                    NetLang = "de-CH";
                    break;
            }

            return NetLang;

        }


    }

}
