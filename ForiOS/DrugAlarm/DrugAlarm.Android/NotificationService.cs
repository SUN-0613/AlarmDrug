using Android.App;
using Android.Content;
using Android.Media;
using System;
using Xamarin.Forms;
using DrugAlarm.Droid;
using Android.Service;

[assembly: Dependency(typeof(NotificationService))]

/// <summary>
/// ローカル通知
/// </summary>
/// <remarks>
/// 参考URL
/// https://itblogdsi.blog.fc2.com/blog-entry-185.html
/// </remarks>
public class NotificationService : DrugAlarm.Common.INotificationService
{

    /// <summary>
    /// The notify key.
    /// </summary>
    private const Int32 _RequestID = 0;

    /// <summary>
    /// 通知許可の申請
    /// </summary>
    public void Allow()
    {
        return; // Androidでは必要なし
    }

    /// <summary>
    /// 通知表示
    /// </summary>
    /// <param name="Title">主題</param>
    /// <param name="SubTitle">副題</param>
    /// <param name="Message">表示内容</param>
    /// <param name="sec">指定秒数後に通知表示</param>
    /// <param name="IsRepeat">通知表示を繰り返すか</param>
    /// <param name="IsUseBadge">バッジの数字更新</param>
    public void Show(string Title, string SubTitle, string Message, Int32 sec = 0, bool IsRepeat = false, bool IsUseBadge = true)
    {

        Context context = Forms.Context;
        Intent intents = new Intent(context, typeof(MainActivity));
        PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, intents, 0);

        Android.Net.Uri uri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

        Notification notification = new Notification.Builder(context)
                                                    .SetContentTitle(Title)
                                                    .SetContentText(Message)
                                                    .SetOngoing(false)
                                                    .SetContentIntent(pendingIntent)
                                                    .SetSound(uri)
                                                    .Build();

        var manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
        manager.Notify(_RequestID, notification);


    }

    /// <summary>
    /// 通知解除
    /// </summary>
    public void Release()
    {
        Context context = Forms.Context;
        NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
        manager.Cancel(_RequestID);
    }

}
