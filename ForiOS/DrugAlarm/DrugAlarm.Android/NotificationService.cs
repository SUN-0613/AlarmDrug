using Android.App;
using Android.Content;
using Android.Media;
using System;
using Xamarin.Forms;
using DrugAlarm.Common;

[assembly: Dependency(typeof(NotificationService))]
public class NotificationService : INotificationService
{

    /// <summary>
    /// 通知ID
    /// </summary>
    private Int32 _id = 0;

    /// <summary>
    /// 登録
    /// </summary>
    public void Regist()
    {
        return; //Androidでは何もしない
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

        /*
        Context Cont = Forms.Context;
        Intent Int = new Intent(Cont, typeof(MainActivity));
        */
    }

    /// <summary>
    /// 通知解除
    /// </summary>
    public void off()
    {
        return; //後回し
    }


}
