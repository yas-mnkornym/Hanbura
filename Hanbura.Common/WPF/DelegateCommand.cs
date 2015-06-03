using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Studiotaiha.Hanbura
{
	public class DelegateCommand : Dispatchable, ICommand
	{
		public DelegateCommand(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		/// <summary>
		/// Execute()の処理を行うActionを取得・設定する
		/// </summary>
		public Action<object> ExecuteHandler { get; set; }

		/// <summary>
		/// canExecute()の処理を行うFuncを取得・設定する
		/// </summary>
		public Func<object, bool> CanExecuteHandler { get; set; }

		public bool CanExecute(object parameter)
		{
			var d = CanExecuteHandler;
			return d == null ? true : d(parameter);
		}

		public void Execute(object parameter)
		{
			var d = ExecuteHandler;
			if (d != null) {
				d(parameter);
			}
		}

		public void RaiseCanExecuteChanged()
		{
			Dispatch(() => {
				var d = CanExecuteChanged;
				if (d != null) {
					d(this, null);
				}
			});
		}

		public void PostCanExecuteChanged()
		{
			BeginDispatch(() => {
				var d = CanExecuteChanged;
				if (d != null) {
					d(this, null);
				}
			});
		}

		public event EventHandler CanExecuteChanged;
	}
}
