using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DrugAlarm.Class
{

    /// <summary>
    /// アラーム処理クラス
    /// </summary>
    public class AlarmTimer : IDisposable
    {

        /// <summary>
        /// 100msタイマ
        /// </summary>
        private DispatcherTimer _Timer;

        /// <summary>
        /// new
        /// </summary>
        public AlarmTimer()
        {

            _Timer = new DispatcherTimer();
            _Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _Timer.Tick += new EventHandler(Timer_Tick);
            _Timer.Start();

            

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            _Timer.Tick -= new EventHandler(Timer_Tick);
        }

        /// <summary>
        /// タイマ処理
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {

        }


    }

}
