using Xamarin.Forms;

namespace DrugAlarm.Common
{

    /// <summary>
    /// View cell custom.
    /// </summary>
    public class ViewCellCustom : ViewCell
    {

        /// <summary>
        /// 選択ハイライト背景色プロパティ
        /// </summary>
        public static readonly BindableProperty SelectedBackgroundColorProperty 
            = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(ViewCellCustom), Color.Default);

        /// <summary>
        /// 選択ハイライト背景色
        /// </summary>
        /// <value>The color of the selected background.</value>
        public Color SelectedBackgroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }

        /// <summary>
        /// new
        /// </summary>
        public ViewCellCustom()
        {
        }

    }

}
