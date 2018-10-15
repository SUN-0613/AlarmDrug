using System;
using System.Windows.Input;

namespace DrugAlarm.Common
{
    /// <summary>
    /// Delegateを受け取るICommandの実装
    /// </summary>
    class DelegateCommand : ICommand
    {

        /// <summary>
        /// CanExecute変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged
        {

            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }

        }

        /// <summary>
        /// 実行メソッド
        /// </summary>
        private Action _Execute;

        /// <summary>
        /// 実行メソッド処理許可
        /// </summary>
        private Func<bool> _CanExecute;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        public DelegateCommand(Action execute)
        {
            _Execute = execute ?? throw new ArgumentNullException("Execute");
            _CanExecute = null;
        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        /// <param name="canExecute">実行メソッド処理許可</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _Execute = execute ?? throw new ArgumentNullException("Execute");
            _CanExecute = canExecute ?? throw new ArgumentNullException("CanExecute");
        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        public void Execute()
        {
            _Execute();
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        public bool CanExecute()
        {
            return _CanExecute == null ? true : _CanExecute();
        }

        /// <summary>
        /// ICommand.CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>メソッド実行許可</returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute();
        }

        /// <summary>
        /// ICommand.Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _Execute();
        }
    }
}
