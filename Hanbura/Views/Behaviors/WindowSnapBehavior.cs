using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	internal class WindowSnapBehavior : Behavior<Window>
	{
		ILogger logger_;
		ILogger Logger{
			get{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger(this));
			}
		}

		#region EnableSnap 依存関係プロパティ
		public double SnapDistance
		{
			get { return (double)GetValue(SnapDistanceProperty); }
			set { SetValue(SnapDistanceProperty, value); }
		}
		public static readonly DependencyProperty SnapDistanceProperty =
			DependencyProperty.Register("SnapDistance", typeof(double), typeof(WindowSnapBehavior), new PropertyMetadata(7.0));
		#endregion // EnableSnap 依存関係プロパティ


		#region EnableSnap 依存関係プロパティ
		public bool EnableSnap
		{
			get { return (bool)GetValue(EnableSnapProperty); }
			set { SetValue(EnableSnapProperty, value); }
		}
		public static readonly DependencyProperty EnableSnapProperty =
			DependencyProperty.Register("EnableSnap", typeof(bool), typeof(WindowSnapBehavior), new PropertyMetadata(true));
		#endregion // EnableSnap 依存関係プロパティ


		protected override void OnAttached()
		{
			AssociatedObject.LocationChanged += AssociatedObject_LocationChanged;
		}

		protected override void OnDetaching()
		{
			AssociatedObject.LocationChanged -= AssociatedObject_LocationChanged;
		}

		void AssociatedObject_LocationChanged(object sender, EventArgs e)
		{
			if (!EnableSnap) { return; }

			try {
				var window = AssociatedObject;
				if (window.WindowState != WindowState.Normal) { return; }

				var mat = PresentationSource.FromVisual(window).CompositionTarget.TransformToDevice;
				var scaledTopLeft = mat.Transform(new Point(window.Left, window.Top));
				var scaledBottomRight = mat.Transform(new Point(window.Left + window.ActualWidth, window.Top + window.ActualHeight));
				var scaledSnapDisatance = mat.Transform(new Point(SnapDistance, SnapDistance));
				var scaledSize = scaledBottomRight - scaledTopLeft;

				var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)scaledTopLeft.X, (int)scaledTopLeft.Y));
				var bounds = screen.WorkingArea;
				var newTop = scaledTopLeft.Y;
				var newLeft = scaledTopLeft.X;

				// 横方向の調整
				if (Math.Abs(bounds.Left - scaledTopLeft.X) <= scaledSnapDisatance.X) {
					newLeft = bounds.Left;
				}
				else if (Math.Abs(bounds.Right - scaledBottomRight.X) <= scaledSnapDisatance.X) {
					newLeft = bounds.Right - scaledSize.X;
				}

				// 縦方向の調整
				if (Math.Abs(bounds.Top - scaledTopLeft.Y) <= scaledSnapDisatance.Y) {
					newTop = bounds.Top;
				}
				else if (Math.Abs(bounds.Bottom - scaledBottomRight.Y) <= scaledSnapDisatance.Y) {
					newTop = bounds.Bottom - scaledSize.Y;
				}

				window.Left = newLeft;
				window.Top = newTop;
			}
			catch (Exception ex) {
				Logger.Error("ウィンドウのスナップ処理に失敗しました。", ex);
			}
		}
	}
}
