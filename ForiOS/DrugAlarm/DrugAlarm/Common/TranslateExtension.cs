using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DrugAlarm.Common
{

    /// <summary>
    /// XAMLから言語設定呼出
    /// </summary>
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {

        /// <summary>
        /// リソースファイル
        /// </summary>
        private const string ResourceID = "DrugAlarm.Resx.Resources";

        /// <summary>
        /// 言語情報
        /// </summary>
        readonly CultureInfo CInfo;

        /// <summary>
        /// テキストプロパティ
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// new
        /// </summary>
        public TranslateExtension()
        {

            if (Device.RuntimePlatform.Equals(Device.iOS) || Device.RuntimePlatform.Equals(Device.Android))
            {
                CInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider SProvider)
        {

            if (Text == null) return "";

            ResourceManager ResManager = new ResourceManager(ResourceID, typeof(TranslateExtension).GetTypeInfo().Assembly);
            var Translation = ResManager.GetString(Text, CInfo);

            if (Translation == null)
            {

#if DEBUG
                throw new ArgumentException(String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'", Text, ResourceID, CInfo.Name), nameof(Text));
#else
                Translation = Text;
#endif
            }

            return Translation;

        }

    }
}
