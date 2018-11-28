using System;
using System.Globalization;

namespace DrugAlarm.Common
{

    /// <summary>
    /// 多言語化
    /// </summary>
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocal(CultureInfo cl);
    }

    /// <summary>
    /// 言語選択クラス
    /// </summary>
    public class PlatformCulture
    {

        /// <summary>
        /// 言語
        /// </summary>
        /// <value>The platform string.</value>
        public string PlatformString { get; private set; }

        /// <summary>
        /// 言語コード
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode { get; private set; }

        /// <summary>
        /// ローカルコード
        /// </summary>
        /// <value>The local code.</value>
        public string LocalCode { get; private set; }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="PlatformCultureString">Platform culture string.</param>
        public PlatformCulture(string PlatformCultureString)
        {

            if (String.IsNullOrEmpty(PlatformCultureString))
            {
                throw new ArgumentNullException(nameof(PlatformCultureString));
            }

            PlatformString = PlatformCultureString.Replace("_", "-");
            Int32 Index = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (Index > 0)
            {
                string[] Values = PlatformString.Split('-');
                LanguageCode = Values[0];
                LocalCode = Values[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocalCode = "";
            }

        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:DrugAlarm.Common.PlatformCulture"/>.</returns>
        public override string ToString()
        {
            return PlatformString;
        }

    }

}
