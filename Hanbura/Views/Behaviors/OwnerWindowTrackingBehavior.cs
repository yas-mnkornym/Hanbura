using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	internal class OwnerWindowTrackingBehavior : Behavior<Window>
	{
		public bool IsEnabled
		{
			get { return (bool)GetValue(IsEnabledProperty); }
			set { SetValue(IsEnabledProperty, value); }
		}

		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.Register("IsEnabled", typeof(bool), typeof(OwnerWindowTrackingBehavior), new PropertyMetadata(true));

		public Window Owner
		{
			get { return (Window)GetValue(OwnerProperty); }
			set { SetValue(OwnerProperty, value); }
		}

		public static readonly DependencyProperty OwnerProperty =
			DependencyProperty.Register("Owner", typeof(Window), typeof(OwnerWindowTrackingBehavior),
			new PropertyMetadata(null, (d, e) => {
				var behavior = d as OwnerWindowTrackingBehavior;
				if (behavior == null) { return; }
				var oldOwner = e.OldValue as Window;
				var owner = e.NewValue as Window;

				if(oldOwner != null){
					oldOwner.LocationChanged -= behavior.owner_LocationChanged;
					oldOwner.SizeChanged -= behavior.owner_SizeChanged;
					oldOwner.StateChanged -= behavior.owner_StateChanged;
				}
				if (owner != null) {
					owner.LocationChanged += behavior.owner_LocationChanged;
					owner.SizeChanged += behavior.owner_SizeChanged;
					owner.StateChanged += behavior.owner_StateChanged;
					behavior.lastOwnerLeft_ = owner.Left;
					behavior.lastOwnerTop_ = owner.Top;
					behavior.lastState_ = owner.WindowState;
				}
			}));

		double lastOwnerLeft_ = double.NaN, lastOwnerTop_ = double.NaN;

		void owner_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (!IsEnabled) { return; }
			if (AssociatedObject == null) { return; }
			if (Owner == null) { return; }
			if (Owner.WindowState != WindowState.Normal) { return; }

			if (e.WidthChanged && (Owner.Left + e.PreviousSize.Width / 2.0 < AssociatedObject.Left)){
				AssociatedObject.Left += (e.NewSize.Width - e.PreviousSize.Width);
			}
			if (e.HeightChanged && (Owner.Top + e.PreviousSize.Height / 2.0 < AssociatedObject.Top)) {
				AssociatedObject.Top += (e.NewSize.Height - e.PreviousSize.Height);
			}
		}

		void owner_LocationChanged(object sender, EventArgs e)
		{
			if (!IsEnabled) { return; }
			if (AssociatedObject == null) { return; }
			if(Owner == null){ return; }
			if (Owner.WindowState != WindowState.Normal) { return; }

			if (!double.IsNaN(lastOwnerLeft_)) {
				var xDiff = Owner.Left - lastOwnerLeft_;
				if (xDiff != 0) { AssociatedObject.Left += xDiff; }
			}

			if (!double.IsNaN(lastOwnerTop_)) {
				var yDiff = Owner.Top - lastOwnerTop_;
				if (yDiff != 0) { AssociatedObject.Top += yDiff; }
			}

			lastOwnerLeft_ = Owner.Left;
			lastOwnerTop_ = Owner.Top;
		}

		double restoreLeft_, restoreTop_;
		WindowState lastState_;
		void owner_StateChanged(object sender, EventArgs e)
		{
			if (!IsEnabled) { return; }
			if (AssociatedObject == null) { return; }
			if (Owner == null) { return; }

			if(Owner.WindowState == WindowState.Maximized){
				var bounds = Owner.RestoreBounds;
				if (!double.IsNaN(lastOwnerLeft_)) {
					var xDiff = bounds.Left - lastOwnerLeft_;
					if (xDiff != 0) { AssociatedObject.Left += xDiff; }
				}

				if (!double.IsNaN(lastOwnerTop_)) {
					var yDiff = bounds.Top - lastOwnerTop_;
					if (yDiff != 0) { AssociatedObject.Top += yDiff; }
				}
				restoreLeft_ = AssociatedObject.Left;
				restoreTop_ = AssociatedObject.Top;
			}
			else if (Owner.WindowState == WindowState.Normal && lastState_ == WindowState.Maximized) {
				AssociatedObject.Left = restoreLeft_;
				AssociatedObject.Top = restoreTop_;
			}
			lastState_ = Owner.WindowState;
		}
	}
}
