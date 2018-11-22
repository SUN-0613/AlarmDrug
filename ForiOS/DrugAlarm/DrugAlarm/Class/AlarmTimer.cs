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

            Parameter _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;
            bool IsRun = true;      //タイマ続行FLG
            bool IsSkip = false;    //タイマスキップFLG

            /*
            Int32 Counter = 0;
            bool IsAlert = false;
            */

            //タイマ処理
            Device.StartTimer(new TimeSpan(0, 0, 0, 0, 100), 
            () =>
            {

                if (!IsSkip)
                {

                    IsSkip = true;

                    try
                    {

                        /*
                        if (++Counter == 10)
                        {
                            Counter = 0;
                            IsAlert = true;
                            (Xamarin.Forms.Application.Current as App).MainPage.DisplayAlert("Title", "Message", "OK").ContinueWith(t => { IsAlert = false; if (IsSkip) IsSkip = false; });
                            //(Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Alarm());
                        }
                        */

                        //次回アラーム時刻を超過していればアラーム表示
                        if (_Parameter.NextAlarm.Timer <= DateTime.Now)
                        {

                            //アラーム表示
                            (Xamarin.Forms.Application.Current as App).MainPage.Navigation.PushAsync(new Form.View.Alarm());

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
                        /*
                        if (!IsAlert)
                            IsSkip = false;
                        */
                        IsSkip = false;
                    }

                }

                return IsRun;                

            });

        }

    }
}
