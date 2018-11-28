using System;
using System.ComponentModel;
using System.Diagnostics;

namespace DrugAlarm.Common
{

    /// <summary>
    /// ViewModel基幹
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {

        /// <summary>
        /// Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="PropertyName">Changedイベントを発生させたいプロパティ名</param>
        /// <param name="StackFrameIndex">呼び出し元StackFrame</param>
        protected virtual void CallPropertyChanged(string PropertyName = "", Int32 StackFrameIndex = 1)
        {

            if (PropertyChanged == null) return;

            //プロパティ名が指定されていない場合は呼び出し元メソッド名とする
            if (PropertyName.Length.Equals(0))
            {
                StackFrame Caller = new StackFrame(StackFrameIndex);        //呼び出し元メソッド情報
                string[] MethodName = Caller.GetMethod().Name.Split('_');   //呼び出し元メソッド名
                PropertyName = MethodName[MethodName.Length - 1];
            }

            //イベント発生
            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));

        }

    }
}
