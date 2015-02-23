using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.ViewModels.Alerts
{
	internal abstract class AlertWindowViewModelBase : BindableBase
	{
		public AlertWindowViewModelBase(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		#region Abstract Methods
		/// <summary>
		/// コピーコマンドの実装を提供する
		/// </summary>
		abstract protected void Copy();

		/// <summary>
		/// コピーに対応しているかどうかを示すフラグを取得する
		/// </summary>
		abstract protected bool IsCopyable { get; }
		#endregion // Abstract Methods

		#region Bindings
		#region Content
		object content_;
		public object Content
		{
			get
			{
				return content_;
			}
			set
			{
				SetValue(ref content_, value);
			}
		}
		#endregion

		#region Selections
		ObservableCollection<string> selections_ = new ObservableCollection<string>();
		public ObservableCollection<string> Selections
		{
			get
			{
				return selections_;
			}
			set
			{
				SetValue(ref selections_, value);
			}
		}
		#endregion

		#region Caption
		string caption_;
		public string Caption
		{
			get
			{
				return caption_;
			}
			set
			{
				SetValue(ref caption_, value);
			}
		}
		#endregion

		#region AlertType
		EAlertType alertType_;
		public EAlertType AlertType
		{
			get
			{
				return alertType_;
			}
			set
			{
				SetValue(ref alertType_, value);
			}
		}
		#endregion
		#endregion // Bindings

		#region Commands
		#region CopyCommand
		DelegateCommand copyCommand_ = null;
		public DelegateCommand CopyCommand
		{
			get
			{
				return copyCommand_ ?? (copyCommand_ = new DelegateCommand {
					ExecuteHandler = param => {
						Copy();
					},
					CanExecuteHandler = param => {
						return IsCopyable;
					}
				});
			}
		}
		#endregion 

		#region SelectedCommand
		DelegateCommand selectedCommand_ = null;
		public DelegateCommand SelectedCommand
		{
			get
			{
				return selectedCommand_ ?? (selectedCommand_ = new DelegateCommand {
					ExecuteHandler = param => {
						var selection = param as string;
						if (selection == null) { return; }

						if (Selected != null) {
							Selected(this, selection);
						}
					}
				});
			}
		}
		#endregion 
		#endregion // Commands

		#region Events
		/// <summary>
		/// ユーザが選択肢から一つ選択した時に呼び出される。
		/// </summary>
		public event EventHandler<string> Selected;
		#endregion
	}
}
