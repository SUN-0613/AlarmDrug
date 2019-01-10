using DrugAlarm.Common;
using DrugAlarm.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ViewCellCustom), typeof(ViewCellCustomRenderer))]

namespace DrugAlarm.iOS.Renderers
{

    /// <summary>
    /// View Cell Custom Renderer
    /// </summary>
    public class ViewCellCustomRenderer : ViewCellRenderer
    {

        /// <summary>
        /// セル取得
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="item">Item.</param>
        /// <param name="reusableCell">Reusable cell.</param>
        /// <param name="tv">Tv.</param>
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {

            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = ((ViewCellCustom)item).SelectedBackgroundColor.ToUIColor()
            };

            return cell;

        }

    }
}
