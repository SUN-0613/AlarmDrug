using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using UserNotifications;

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
    private const string _RequestID = "notifyKey";

    /// <summary>
    /// The notify value.
    /// </summary>
    private const string _RequestValue = "notifyValue";

    /// <summary>
    /// 通知許可の申請
    /// </summary>
    public void Allow()
    {

        // iOSのバージョンチェック
        if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
        {

            // 許可が欲しい通知タイプの選択
            // 通知タイプは「テキスト」「アイコンバッチ」「サウンド」
            // 追加するときは"|"で区切る
            UNAuthorizationOptions Types = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;

            // 許可申請
            UNUserNotificationCenter.Current.RequestAuthorization(Types, (Granted, Err) => 
            { 
                // エラー発生チェック
                if (Err != null)    
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(Err.LocalizedFailureReason + "\n" + Err.LocalizedDescription);
#endif
                }
            });

        }

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

        // メインスレッドにて処理
        UIApplication.SharedApplication.InvokeOnMainThread(delegate 
        {

            var Content = new UNMutableNotificationContent();                               // 表示内容
            var Trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(sec, IsRepeat);   // 表示条件

            // 表示テキスト
            Content.Title = Title;
            Content.Subtitle = SubTitle;
            Content.Body = Message;

            // 表示サウンド
            Content.Sound = UNNotificationSound.Default;

            // 解除する際のキー設定
            Content.UserInfo = NSDictionary.FromObjectAndKey(new NSString(_RequestValue), new NSString(_RequestID));

            // ローカル通知の予約
            var Request = UNNotificationRequest.FromIdentifier(_RequestID, Content, Trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(Request, (Err) => 
            {
                // エラー発生チェック
                if (Err != null)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(Err.LocalizedFailureReason + "\n" + Err.LocalizedDescription);
#endif
                }
            });

            // アイコン上に表示されるバッジ数値更新
            if (IsUseBadge)
                UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1;

        });

    }

    /// <summary>
    /// 通知解除
    /// </summary>
    public void Release()
    {
        UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { _RequestID });
        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
    }

}
