using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.ViewModels.Alerts
{
	internal sealed class ContentAlertWindowViewModel : AlertWindowViewModelBase
	{
		IDialogCallback callback_;

		public ContentAlertWindowViewModel(
			string caption,
			object content,
			EAlertType alertType,
			IEnumerable<string> selections,
			IDialogCallback callback,
			IDispatcher dispatcher = null)
			: base(dispatcher)
		{
			if (caption == null) { throw new ArgumentNullException("caption"); }
			if (content == null) { throw new ArgumentNullException("content"); }
			if (selections == null) { throw new ArgumentNullException("selection"); }
			if (!selections.Any()) { throw new ArgumentNullException("selection is empty"); }

			Caption = caption;
			Content = content;
			AlertType = alertType;
			Selections = new System.Collections.ObjectModel.ObservableCollection<string>(selections);
			callback_ = callback;
		}

		protected override void Copy()
		{
			if (callback_ != null) {
				callback_.OnCopy();
			}
		}

		protected override bool IsCopyable
		{
			get
			{
				if (callback_ != null) {
					return callback_.SupportsCopy;
				}
				else {
					return false;
				}
			}
		}
	}
}
