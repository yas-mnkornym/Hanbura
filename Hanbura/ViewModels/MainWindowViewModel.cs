using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.ViewModels
{
	public class MainWindowViewModel : BindableBase
	{
		public MainWindowViewModel(IDispatcher dispatcher)
			: base(dispatcher)
		{ }
		
		#region Bindings
		#region Caption Messages
		#region NotificationMessage
		string captionMessage_;
		public string CaptionMessage
		{
			get
			{
				return captionMessage_;
			}
			set
			{
				SetValue(ref captionMessage_, value);
			}
		}
		#endregion

		#region CaptionMessageKind
		CaptionMessageKind captionMessageKind_ = CaptionMessageKind.None;
		public CaptionMessageKind CaptionMessageKind
		{
			get
			{
				return captionMessageKind_;
			}
			set
			{
				SetValue(ref captionMessageKind_, value);
			}
		}
		#endregion
		#endregion // Caption Messages
		#endregion // Bindings
	}
}
