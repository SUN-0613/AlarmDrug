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
        /// 表示ページ
        /// </summary>
        private Page _MainPage = (Xamarin.Forms.Application.Current as App).MainPage;

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (Xamarin.Forms.Application.Current as App).Parameter;

        /// <summary>
        /// タイマースキップ
        /// </summary>
        private bool IsSkip;

        /// <summary>
        /// タイマー続行
        /// </summary>
        private bool IsRun;

        /// <summary>
        /// new
        /// </summary>
        public AlarmTimer()
        {

            IsRun = true;
            IsSkip = false;

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
                            _MainPage.Navigation.PushAsync(new Form.View.Alarm());

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
