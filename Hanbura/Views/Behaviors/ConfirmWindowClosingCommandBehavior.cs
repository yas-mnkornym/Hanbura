using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	class ConfirmWindowClosingCommandBehavior : Behavior<Window>
	{
		public ICommand ClosingCommand
		{
			get { return (ICommand)GetValue(ClosingCommandProperty); }
			set { SetValue(ClosingCommandProperty, value); }
		}

		public static readonly DependencyProperty ClosingCommandProperty =
			DependencyProperty.Register("ClosingCommand", typeof(ICommand), typeof(ConfirmWindowClosingCommandBehavior), new PropertyMetadata(null));


		protected override void OnAttached()
		{
			AssociatedObject.Closing += AssociatedObject_Closing;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.Closing -= AssociatedObject_Closing;
		}

		void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ClosingCommand != null && ClosingCommand.CanExecute(AssociatedObject)) {
				ClosingCommand.Execute(e);
			}
		}
	}
}
