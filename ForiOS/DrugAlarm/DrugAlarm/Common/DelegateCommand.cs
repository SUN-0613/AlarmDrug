using System;
using System.Windows.Input;

namespace DrugAlarm.Common
{

    /// <summary>
    /// Delegateを受け取るICommandの実装
    /// </summary>
    public class DelegateCommand : ICommand
    {

        /// <summary>
        /// CanExecute変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged;

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
        /// <param name="Execute">実行メソッド</param>
        public DelegateCommand(Action Execute)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = null;
        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="Execute">実行メソッド</param>
        /// <param name="canExecute">実行メソッド処理許可</param>
        public DelegateCommand(Action Execute, Func<bool> CanExecute)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute ?? throw new ArgumentNullException(nameof(CanExecute));
        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        public void Execute()
        {
            _Execute();
        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        public void Execute(object Parameter)
        {
            _Execute();
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        public bool CanExecute()
        {
            return _CanExecute == null ? true : _CanExecute();
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        public bool CanExecute(object Parameter)
        {
            return _CanExecute();
        }

    }
}
