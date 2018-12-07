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
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;

        /// <summary>
        /// タイマ続行FLG
        /// </summary>
        private bool IsRunTimer = true;

        /// <summary>
        /// タイマスキップFLG
        /// </summary>
        private bool IsSkipTimer = false;

        /// <summary>
        /// ローカル通知済FLG
        /// </summary>
        private bool IsLocalAlarm = false;

        /// <summary>
        /// アラーム画面表示FLG
        /// </summary>
        private bool IsShowAlarm = false;

        /// <summary>
        /// new
        /// </summary>
        public AlarmTimer()
        {

            //ローカル通知許可
            DependencyService.Get<Common.INotificationService>().Allow();

        }

        /// <summary>
        /// タイマー処理開始
        /// </summary>
        public void Start()
        {

            IsRunTimer = true;

            //タイマ処理
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0),
            () =>
            {

                CheckTimer();

                return IsRunTimer;

            });


        }

        /// <summary>
        /// タイマー処理中止
        /// </summary>
        public void Stop()
        {
            IsRunTimer = false;
        }

        /// <summary>
        /// 薬服用時間チェック
        /// </summary>
        public void CheckTimer()
        {

            if (!IsSkipTimer)
            {

                IsSkipTimer = true;

                try
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine("Run " + DateTime.Now.ToString("HH:mm:ss") + " :Next " + _Parameter.NextAlarm.Timer.ToString("HH:mm"));
#endif

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
                                DependencyService.Get<Common.INotificationService>().Show(Resx.Resources.Timer_Title, Resx.Resources.Timer_Subtitle, Resx.Resources.Timer_Body);
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
                    IsSkipTimer = false;
                }

            }

        }

    }
}
