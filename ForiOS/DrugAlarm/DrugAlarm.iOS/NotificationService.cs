using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using DrugAlarm.Common;

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

        return; //後回し

    }

    /// <summary>
    /// 通知する
    /// </summary>
    /// <param name="Title">タイトル</param>
    /// <param name="SubTitle">サブタイトル</param>
    /// <param name="Body">内容</param>
    public void On(string Title, string SubTitle, string Body)
    {
        return; //後回し
    }

    /// <summary>
    /// 通知解除
    /// </summary>
    public void off()
    {
        return; //後回し
    }

}
