using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using DrugAlarm.Common;
using UserNotifications;

/// <summary>
/// ローカル通知
/// </summary>
[assembly: Dependency(typeof(NotificationService))]
public class NotificationService : INotificationService
{

    /// <summary>
    /// iOS用の登録
    /// </summary>
    public void Regist()
    {

        UNAuthorizationOptions types = UNAuthorizationOptions.Alert;

        UNUserNotificationCenter.Current.RequestAuthorization(types, (granted, err) => 
        {

            if (err != null)
            {
                System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + Environment.NewLine + err.LocalizedDescription);
            }

            if (granted)
            {

            }

        });

    }

    /// <summary>
    /// 通知する
    /// </summary>
    /// <param name="Title">タイトル</param>
    /// <param name="SubTitle">サブタイトル</param>
    /// <param name="Body">内容</param>
    public void On(string Title, string SubTitle, string Body)
    {
        UIApplication.SharedApplication.InvokeOnMainThread(delegate
        {

            var content = new UNMutableNotificationContent();
            content.Title = Title;
            content.Subtitle = SubTitle;
            content.Body = Body;
            content.Sound = UNNotificationSound.Default;

            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

            var requestID = "notifyKey";
            content.UserInfo = NSDictionary.FromObjectAndKey(new NSString("notifyValue"), new NSString("notifyKey"));
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            //UNUserNotificationCenter.Current.Delegate = new UILocalNotificationCenterDelegate();

            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => 
            {
                if (err != null)
                {
                    System.Diagnostics.Debug.WriteLine(err.LocalizedFailureReason + Environment.NewLine + err.LocalizedDescription);
                }
            });

            UIApplication.SharedApplication.ApplicationIconBadgeNumber += 1;

        });

    }

    /// <summary>
    /// 通知解除
    /// </summary>
    public void off()
    {
        UIApplication.SharedApplication.InvokeOnMainThread(delegate
        {
            UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { "notifyKey" });
        });
    }

}
