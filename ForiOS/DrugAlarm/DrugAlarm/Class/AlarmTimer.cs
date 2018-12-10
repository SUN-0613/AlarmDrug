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
                    if (_Parameter.NextAlarm.Count > 0)
                        System.Diagnostics.Debug.WriteLine("Run " + DateTime.Now.ToString("HH:mm:ss") + " :Next " + _Parameter.NextAlarm[0].Timer.ToString("HH:mm"));
                    else
                        System.Diagnostics.Debug.WriteLine("Run " + DateTime.Now.ToString("HH:mm:ss"));
#endif

                    //次回アラーム時刻を超過していればアラーム表示
                    for (Int32 iLoop = 0; iLoop < _Parameter.NextAlarm.Count; iLoop++)
                    {
                        if (_Parameter.NextAlarm[iLoop].Timer <= DateTime.Now)
                            _Parameter.NextAlarmIndex = iLoop;
                        else
                            break;
                    }

                    if (!_Parameter.NextAlarmIndex.Equals(-1))
                    {

                        //バックグラウンドで起動していないか
                        if (!(Xamarin.Forms.Application.Current as App).IsBackground)
                        {
                            //アラーム表示
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
                            //ローカル通知
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
