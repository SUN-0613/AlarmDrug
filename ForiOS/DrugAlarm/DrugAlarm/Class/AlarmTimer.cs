using System;
using Xamarin.Forms;

namespace DrugAlarm.Class
{

    /// <summary>
    /// アラーム処理クラス
    /// </summary>
    public class AlarmTimer
    {

        /// <summary>
        /// new
        /// </summary>
        public AlarmTimer()
        {

            Parameter _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;    //パラメータ
            DependencyService.Get<Common.INotificationService>().Allow();                   //ローカル通知

            bool IsRun = true;          //タイマ続行FLG
            bool IsSkip = false;        //タイマスキップFLG
            bool IsLocalAlarm = false;  //ローカル通知済FLG
            bool IsShowAlarm = false;   //アラーム画面表示FLG

            //タイマ処理
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), 
            () =>
            {

                if (!IsSkip)
                {

                    IsSkip = true;

                    try
                    {

                        //次回アラーム時刻を超過していればアラーム表示
                        if (_Parameter.NextAlarm.Timer <= DateTime.Now)
                        {

                            //アラーム表示
                            if (!(Xamarin.Forms.Application.Current as App).IsBackground)
                            {

                                if (!IsShowAlarm)
                                {

                                    (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Alarm());
                                    IsShowAlarm = true;

                                    if (IsLocalAlarm)
                                    {
                                        DependencyService.Get<Common.INotificationService>().Release();
                                    }

                                }

                            }
                            else 
                            {
                                if (!IsLocalAlarm)
                                {
                                    DependencyService.Get<Common.INotificationService>().Show("Title", "SubTitle", "Body");
                                    IsLocalAlarm = true;
                                }
                            }

                        }
                        else if (IsLocalAlarm || IsShowAlarm)    //FLGリセット
                        {
                            IsLocalAlarm = false;
                            IsShowAlarm = false;
                        }

                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
                    }
                    finally
                    {
                        IsSkip = false;
                    }

                }

                return IsRun;                

            });

        }

    }
}
