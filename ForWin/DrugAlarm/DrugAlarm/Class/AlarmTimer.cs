using System;
using System.Windows.Threading;

namespace DrugAlarm.Class
{

    /// <summary>
    /// アラーム処理クラス
    /// </summary>
    public class AlarmTimer : IDisposable
    {

        /// <summary>
        /// パラメータ
        /// </summary>
        private Parameter _Parameter = (System.Windows.Application.Current as App).Parameter;

        /// <summary>
        /// 100msタイマ
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// new
        /// </summary>
        public AlarmTimer()
        {

            //タイマ処理
            _Timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.IsEnabled = true;
            _Timer.Start();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            //タイマ終了
            if (_Timer.IsEnabled)
            {
                _Timer.IsEnabled = false;
                _Timer.Stop();
            }
            _Timer.Tick -= new EventHandler(Timer_Tick);

        }

        /// <summary>
        /// タイマ処理
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {

            //タイマ一時停止
            _Timer.Stop();
            _Timer.IsEnabled = false;

            try
            {

                //次回アラーム時刻を超過していればアラーム表示
                if (DateTime.Now >= _Parameter.NextAlarm.Timer)
                {

                    //アラーム表示
                    var Alarm = new Form.View.Alarm();
                    Alarm.ShowDialog();

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
                //タイマ再開
                _Timer.IsEnabled = true;
                _Timer.Start();
            }

        }

    }

}
