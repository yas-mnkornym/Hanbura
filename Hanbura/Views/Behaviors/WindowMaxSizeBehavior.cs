using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	internal class WindowMaxSizeBehavior : Behavior<Window>
	{
		#region Logger
		ILogger logger_;
		ILogger Logger
		{
			get
			{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger(this));
			}
		}
		#endregion

		#region Dependency Proeprties
		public double WidthRatio
		{
			get { return (double)GetValue(WidthRatioProperty); }
			set { SetValue(WidthRatioProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WidthRatio.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WidthRatioProperty =
			DependencyProperty.Register("WidthRatio", typeof(double), typeof(WindowMaxSizeBehavior), new PropertyMetadata(0.8));


		public double HeightRatio
		{
			get { return (double)GetValue(HeightRatioProperty); }
			set { SetValue(HeightRatioProperty, value); }
		}

		// Using a DependencyProperty as the backing store for HeightRatio.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty HeightRatioProperty =
			DependencyProperty.Register("HeightRatio", typeof(double), typeof(WindowMaxSizeBehavior), new PropertyMetadata(0.8));
		#endregion



		protected override void OnAttached()
		{
			AssociatedObject.LocationChanged += AssociatedObject_LocationChanged;
			AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
			base.OnAttached();
		}

		protected override void OnDetaching()
		{
			AssociatedObject.LocationChanged -= AssociatedObject_LocationChanged;
			AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
			base.OnDetaching();
		}

		void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			try {
				UpdateMaxSize();
			}
			catch (Exception ex) {
				Logger.Error("Failed to update max size of the window.", ex);
			}
		}

		void AssociatedObject_LocationChanged(object sender, EventArgs e)
		{
			try {
				UpdateMaxSize();
			}
			catch (Exception ex) {
				Logger.Error("Failed to update max size of the window.", ex);
			}
		}

		void UpdateMaxSize()
		{
			var ps = PresentationSource.FromVisual(AssociatedObject);
			var ct = ps.CompositionTarget;
			var tm = ct.TransformToDevice;
			var xscale = tm.M11;
			var yscale = tm.M22;

			var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)AssociatedObject.Left, (int)AssociatedObject.Top));
			var wb = screen.WorkingArea;
			var width = wb.Width;
			var height = wb.Height;

			var swidth = width / xscale;
			var sheight = height / yscale;

			AssociatedObject.MaxWidth = swidth * 0.8;
			AssociatedObject.MaxHeight = sheight * 0.8;
		}
	}
}
