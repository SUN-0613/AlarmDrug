using System;

namespace DrugAlarm.Common
{

    /// <summary>
    /// ローカル通知
    /// DependencyServiceにて利用
    /// </summary>
    public interface INotificationService
    {

        /// <summary>
        /// iOS用の登録
        /// </summary>
        void Regist();

        /// <summary>
        /// 通知する
        /// </summary>
        /// <param name="Title">タイトル</param>
        /// <param name="SubTitle">サブタイトル</param>
        /// <param name="Body">内容</param>
        void On(string Title, string SubTitle, string Body);

        /// <summary>
        /// 通知解除
        /// </summary>
        void off();

    }

}
