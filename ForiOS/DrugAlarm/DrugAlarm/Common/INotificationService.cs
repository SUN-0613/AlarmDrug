using System;

namespace DrugAlarm.Common
{

    /// <summary>
    /// ローカル通知
    /// DependencyServiceにて利用
    /// </summary>
    public interface INotificationService
    {

        /// <summary>
        /// 通知許可の申請
        /// iOS用
        /// </summary>
        void Allow();

        /// <summary>
        /// 通知表示
        /// </summary>
        /// <param name="Title">主題</param>
        /// <param name="SubTitle">副題</param>
        /// <param name="Message">表示内容</param>
        /// <param name="sec">指定秒数後に通知表示</param>
        /// <param name="IsRepeat">通知表示を繰り返すか</param>
        /// <param name="IsUseBadge">バッジの数字更新</param>
        void Show(string Title, string SubTitle, string Message, Int32 sec = 1, bool IsRepeat = false, bool IsUseBadge = true);

        /// <summary>
        /// 通知解除
        /// </summary>
        void Release();

    }

}
