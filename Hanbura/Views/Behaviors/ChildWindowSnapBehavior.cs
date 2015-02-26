using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Studiotaiha.Hanbura.Views.Behaviors
{
	/// <summary>
	/// 子ウィンドウ用のスナップビヘイビア
	/// </summary>
	internal sealed class ChildWindowSnapBehavior : Behavior<Window>
	{
		ILogger logger_;
		ILogger Logger{
			get{
				return logger_ ?? (logger_ = LoggingService.Current.GetLogger(this));
			}
		}

		public Type[] SnapTargets { get; set; }

		public ChildWindowSnapBehavior()
		{
			SnapTargets = new Type[]{
				typeof(MainWindow),
				typeof(ChildWindow)
			};
		}

		#region SnapDistance 依存関係プロパティ
		public double SnapDistance
		{
			get { return (double)GetValue(SnapDistanceProperty); }
			set { SetValue(SnapDistanceProperty, value); }
		}
		public static readonly DependencyProperty SnapDistanceProperty =
			DependencyProperty.Register("SnapDistance", typeof(double), typeof(ChildWindowSnapBehavior), new PropertyMetadata(7.0));
		#endregion // SnapDistance 依存関係プロパティ

		#region EnableSnap 依存関係プロパティ
		public bool EnableSnap
		{
			get { return (bool)GetValue(EnableSnapProperty); }
			set { SetValue(EnableSnapProperty, value); }
		}
		public static readonly DependencyProperty EnableSnapProperty =
			DependencyProperty.Register("EnableSnap", typeof(bool), typeof(ChildWindowSnapBehavior), new PropertyMetadata(true));
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

				// デバイスピクセルにスケール済みのウィンドウ位置・サイズを取得
				var compositionTarget = PresentationSource.FromVisual(window).CompositionTarget;
				var mat = compositionTarget.TransformToDevice;
				var scaledTopLeft = mat.Transform(new Point(window.Left, window.Top));
				var scaledBottomRight = mat.Transform(new Point(window.Left + window.ActualWidth, window.Top + window.ActualHeight));
				var scaledSnapDisatance = mat.Transform(new Point(SnapDistance, SnapDistance));
				var scaledSize = scaledBottomRight - scaledTopLeft;

				// スクリーンの作業領域を取得
				var screen = System.Windows.Forms.Screen.FromPoint(new System.Drawing.Point((int)scaledTopLeft.X, (int)scaledTopLeft.Y))
					.WorkingArea;

				// ほかのウィンドウの領域を取得
				var rects = App.Current.Windows
					.Cast<Window>()
					.Where(x => SnapTargets.Contains(x.GetType()))
					.Select(x => {
						var leftTop = mat.Transform(new Point(x.Left, x.Top));
						var size = mat.Transform(new Point(x.ActualWidth, x.ActualHeight));
						return new System.Drawing.RectangleF((float)leftTop.X, (float)leftTop.Y, (float)size.X, (float)size.Y);
					})
					// スクリーンの作業領域も同じ尺度にして追加
					.Concat(new System.Drawing.RectangleF[]{
						new System.Drawing.RectangleF(screen.Right, screen.Bottom, screen.X - screen.Right, screen.Y - screen.Bottom)
					});

				// 四辺それぞれのターゲット座標を計算
				if (!rects.Any()) { return; }
				var leftTarget = rects.Select(x => x.Right).OrderBy(x => Math.Abs(scaledTopLeft.X - x)).FirstOrDefault();
				var topTarget = rects.Select(x => x.Bottom).OrderBy(x => Math.Abs(scaledTopLeft.Y - x)).FirstOrDefault();
				var rightTarget = rects.Select(x => x.Left).OrderBy(x => Math.Abs(scaledBottomRight.X - x)).FirstOrDefault();
				var bottomTarget = rects.Select(x => x.Top).OrderBy(x => Math.Abs(scaledBottomRight.Y - x)).FirstOrDefault();

				// ターゲットの座標
				var newTop = scaledTopLeft.Y;
				var newLeft = scaledTopLeft.X;

				// 横方向の調整
				if (Math.Abs(leftTarget - scaledTopLeft.X) <= scaledSnapDisatance.X) {
					newLeft = leftTarget;
				}
				else if (Math.Abs(rightTarget - scaledBottomRight.X) <= scaledSnapDisatance.X) {
					newLeft = rightTarget - scaledSize.X;
				}

				// 縦方向の調整
				if (Math.Abs(topTarget - scaledTopLeft.Y) <= scaledSnapDisatance.Y) {
					newTop = topTarget;
				}
				else if (Math.Abs(bottomTarget - scaledBottomRight.Y) <= scaledSnapDisatance.Y) {
					newTop = bottomTarget - scaledSize.Y;
				}


				mat = compositionTarget.TransformFromDevice;
				var reScaledTopLeft = mat.Transform(new Point(newLeft, newTop));
				window.Left = reScaledTopLeft.X;
				window.Top = reScaledTopLeft.Y;
			}
			catch (Exception ex) {
				Logger.Error("ウィンドウのスナップ処理に失敗しました。", ex);
			}
		}
	}
}
