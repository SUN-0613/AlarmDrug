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
        private bool _IsRunTimer = true;

        /// <summary>
        /// タイマスキップFLG
        /// </summary>
        private bool _IsSkipTimer = false;

        /// <summary>
        /// ローカル通知済FLG
        /// </summary>
        private bool _IsLocalAlarm = false;

        /// <summary>
        /// アラーム画面表示FLG
        /// </summary>
        private bool _IsShowAlarm = false;

        /// <summary>
        /// アラーム予定時刻
        /// </summary>
        private DateTime _SetTimer = DateTime.MaxValue;

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

            _IsRunTimer = true;

            //タイマ処理
            Device.StartTimer(new TimeSpan(0, 0, 0, 1, 0),
            () =>
            {

                CheckTimer();

                return _IsRunTimer;

            });


        }

        /// <summary>
        /// タイマー処理中止
        /// </summary>
        public void Stop()
        {
            _IsRunTimer = false;
        }

        /// <summary>
        /// 薬服用時間チェック
        /// </summary>
        public void CheckTimer()
        {

            if (!_IsSkipTimer)
            {

                _IsSkipTimer = true;

                try
                {
#if DEBUG
                    if (!_Parameter.NextAlarm.Timer.Equals(DateTime.MaxValue))
                        System.Diagnostics.Debug.WriteLine("Run " + DateTime.Now.ToString(UserControl.TimeSecFormat) 
                                                            + " :Next " + _Parameter.NextAlarm.Timer.ToString(UserControl.TimeFormat));
                    else
                        System.Diagnostics.Debug.WriteLine("Run " + DateTime.Now.ToString(UserControl.TimeSecFormat));
#endif

                    //アラーム時間が変更されたらFLGリセット
                    if (!_SetTimer.Equals(_Parameter.NextAlarm.Timer))
                    {
                        _IsLocalAlarm = false;
                        _IsShowAlarm = false;
                        _SetTimer = _Parameter.NextAlarm.Timer;

                        //再帰
                        _IsSkipTimer = false;
                        CheckTimer();
                        return;

                    }

                    if (_Parameter.NextAlarm.Timer <= DateTime.Now)
                    {

                        //バックグラウンドで起動していないか
                        if (!(Xamarin.Forms.Application.Current as App).IsBackground)
                        {
                            //アラーム表示
                            if (!_IsShowAlarm)
                            {

                                (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Alarm());
                                _IsShowAlarm = true;

                                if (_IsLocalAlarm)
                                {
                                    DependencyService.Get<Common.INotificationService>().Release();
                                }

                            }

                        }
                        else
                        {
                            //ローカル通知
                            if (!_IsLocalAlarm)
                            {
                                DependencyService.Get<Common.INotificationService>().Show(Resx.Resources.Timer_Title, Resx.Resources.Timer_Subtitle, Resx.Resources.Timer_Body);
                                _IsLocalAlarm = true;
                            }
                        }

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
                    _IsSkipTimer = false;
                }

            }

        }

    }
}
