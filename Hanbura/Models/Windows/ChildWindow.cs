using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Studiotaiha.Hanbura.Windows;

namespace Studiotaiha.Hanbura.Models.Windows
{
	internal sealed class ChildWindow : BindableBase, IWindow
	{
		IWindow owner_;
		Window ownerWindow_;
		Window window_;
		WindowConfig config_;
		bool shouldntDenyClosing_ = true;
		bool isContentInitialized_ = false;

		public ChildWindow(
			WindowConfig config,
			WindowSettings windowSettings,
			IDispatcher dispatcher,
			Action<ChildWindow> beforeCreateWindow = null)
		{
			if(config == null){ throw new ArgumentNullException("config");}
			if (windowSettings == null) { throw new ArgumentNullException("windowSettings"); }
			if(dispatcher == null){ throw new ArgumentNullException("dispatcher");}
			config_ = config;
			WindowSettings = windowSettings;

			HideOnClose = config.HidOnClose;
			CanBeMinimized = config.CanBeMinimized;
			CanBeMaximized = config.CanBeMaximized;
			CanBeIndependentFromOwner = config.CanBeIndependentFromOwner;
			Caption = config.Caption;

			// ここまでの処理はUIスレッドに移られると逆に困る
			Dispatcher = dispatcher;

			// どーせ中でUIスレッドに逝っちゃうんだから外からまとめて送っちゃう
			Dispatch(() => {
				if (config.TopMost.HasValue) { windowSettings.AlwaysOnTop = config.TopMost.Value; }
				WindowSettings.RestorePosition = config.RestorePosition;
				WindowSettings.RestoreSize = config.RestoreSize;
				if (config.Opacity.HasValue) { Opacity = config.Opacity.Value; }
				if (!CanBeIndependentFromOwner) { WindowSettings.IsIndependentFromOwner = false; }

				// ウィンドウも作成しちゃう
				if(beforeCreateWindow != null){
					beforeCreateWindow(this);
				}
				CreateWindow();
			});

			WindowSettings.PropertyChanged += WindowSettings_PropertyChanged;
		}

		#region ChildWindowの関数
		void CreateWindow()
		{
			// ウィンドウを生成
			window_ = new Views.ChildWindow {
				Left = config_.DefaultLeft,
				Top = config_.DefaultTop,
				ResizeMode = config_.IsResizable ? ResizeMode.CanResize : ResizeMode.NoResize,
				Owner = WindowSettings.IsIndependentFromOwner ? null : ownerWindow_,
				DataContext = this,
			};

			if (config_.AllowsTransparency) {
				window_.WindowStyle = WindowStyle.None;
				window_.AllowsTransparency = true;
			}

			// リサイズモードに応じて設定変更
			if (config_.IsResizable) {
				window_.Width = config_.DefaultWidth;
				window_.Height = config_.DefaultHeight;
			}
			else {
				window_.SizeToContent = SizeToContent.WidthAndHeight;
				WindowSettings.RestoreSize = false;
				CanBeMaximized = false;
			}

			window_.Closed += (_, __) => {
				if (Closed != null) { Closed(this, new EventArgs()); }
				WindowSettings.PropertyChanged -= WindowSettings_PropertyChanged;
			};
		}

		void InitializeContents()
		{
			if (!isContentInitialized_) {
				// コントロールとか生成させる。
				var creatingArgs = new WindowShowingEventArgs(Dispatcher);
				if (Creating != null) {
					Creating(this, creatingArgs);
				}
				Content = creatingArgs.Content;
				SettingsContent = creatingArgs.SettingsContent;
				isContentInitialized_ = true;
			}
		}

		void WindowSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == GetMemberName(() => WindowSettings.IsIndependentFromOwner)) {
				if (WindowSettings.IsIndependentFromOwner) {
					window_.Owner = null;
				}
				else {
					window_.Owner = ownerWindow_;
				}
			}
		}

		public void SetOwner(ChildWindow owner)
		{
			if (owner == null) { throw new ArgumentNullException("owner"); }
			ownerWindow_ = owner.window_;
		}

		public void SetOwner(Window window)
		{
			ownerWindow_ = window;
		}

		#endregion

		#region ウィンドウ操作関数

		public void Show()
		{
			InitializeContents();
			window_.Show();
		}

		public bool? ShowDialog()
		{
			InitializeContents();
			return window_.ShowDialog();
		}

		public void Hide()
		{
			window_.Hide();
		}

		public void Close()
		{
			shouldntDenyClosing_ = false;
			window_.Close();
		}


		public void Maximize()
		{
			if (CanBeMaximized) {
				window_.WindowState = System.Windows.WindowState.Maximized;
			}
		}

		public void Minimize()
		{
			if (CanBeMinimized) {
				window_.WindowState = System.Windows.WindowState.Minimized;
			}
		}

		public void Restore()
		{
			window_.WindowState = System.Windows.WindowState.Normal;
		}
		#endregion

		#region 普通のプロパティ
		public IWindow Owner
		{
			get
			{
				return owner_;
			}
			set
			{
				if (SetValue(ref owner_, value)) {
					var childWindow = value as ChildWindow;
					if (childWindow != null) {
						ownerWindow_ = childWindow.ownerWindow_;
						if (!WindowSettings.IsIndependentFromOwner) {
							window_.Owner = ownerWindow_;
						}
					}
				}
			}
		}

		public string Tag { get; private set; }

		public bool TopMost
		{
			get
			{
				return WindowSettings.AlwaysOnTop;
			}
			set
			{
				WindowSettings.AlwaysOnTop = value;
			}
		}

		public bool HideOnClose
		{
			get;
			set;
		}

		public double Width
		{
			get
			{
				return window_.Width;
			}
			set
			{
				Dispatch(() => window_.Width = value);
			}
		}

		public double Height
		{
			get
			{
				return window_.Height;
			}
			set
			{
				Dispatch(() => window_.Height = value);
			}
		}

		public double Left
		{
			get
			{
				return window_.Left;
			}
			set
			{
				Dispatch(() => window_.Left = value);
			}
		}

		public double Top
		{
			get
			{
				return window_.Top;
			}
			set
			{
				Dispatch(() => window_.Top = value);
			}
		}

		public bool IsVisible
		{
			get
			{
				return window_.IsVisible;
			}
			set
			{
				if (value) {
					Show();
				}
				else {
					Hide();
				}
			}
		}
		#endregion

		#region 変更通知プロパティ
		#region WindowSettings
		WindowSettings windowSettings_;
		public WindowSettings WindowSettings
		{
			get
			{
				return windowSettings_;
			}
			set
			{
				SetValue(ref windowSettings_, value);
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

		#region SettingsContent
		object settingsContent_;

		public object SettingsContent
		{
			get
			{
				return settingsContent_;
			}
			set
			{
				SetValue(ref settingsContent_, value);
			}
		}
		#endregion

		#region Opacity
		double opacity_ = 1.0;
		public double Opacity
		{
			get
			{
				return opacity_;
			}
			set
			{
				SetValue(ref opacity_, value);
			}
		}
		#endregion

		#region TrackOwner
		bool trackOwner_ = true;
		public bool TrackOwner
		{
			get
			{
				return trackOwner_;
			}
			set
			{
				SetValue(ref trackOwner_, value);
			}
		}
		#endregion

		#region CanBeIndependentFromOwner
		bool canBeIndependentFromOwner_ = true;
		public bool CanBeIndependentFromOwner
		{
			get
			{
				return canBeIndependentFromOwner_;
			}
			set
			{
				SetValue(ref canBeIndependentFromOwner_, value);
			}
		}
		#endregion

		#region CanBeMinimized
		bool canBeMinimized_ = true;
		public bool CanBeMinimized
		{
			get
			{
				return canBeMinimized_;
			}
			set
			{
				SetValue(ref canBeMinimized_, value);
			}
		}
		#endregion

		#region CanBeMaximized
		bool canBeMaximized_ = true;
		public bool CanBeMaximized
		{
			get
			{
				return canBeMaximized_;
			}
			set
			{
				if (value && !IsResizable) { return; }
				SetValue(ref canBeMaximized_, value);
			}
		}
		#endregion

		#region ShowCloseButton
		bool showCloseButton = true;
		public bool ShowCloseButton
		{
			get
			{
				return showCloseButton;
			}
			set
			{
				SetValue(ref showCloseButton, value);
			}
		}
		#endregion

		#region EnableSettings
		bool enableSettings_ = true;
		public bool EnableSettings
		{
			get
			{
				return enableSettings_;
			}
			set
			{
				SetValue(ref enableSettings_, value);
			}
		}
		#endregion

		#region IsResizable
		bool isResizable_ = true;
		public bool IsResizable
		{
			get
			{
				return isResizable_;
			}
			set
			{
				if (SetValue(ref isResizable_, value) && window_ != null) {
					if (value) {
						window_.ResizeMode = ResizeMode.CanResize;
						window_.SizeToContent = SizeToContent.Manual;
						if (config_.CanBeMaximized) {
							CanBeMaximized = true;
						}
					}
					else {
						window_.ResizeMode = ResizeMode.NoResize;
						window_.SizeToContent = SizeToContent.WidthAndHeight;
						CanBeMaximized = false;
					}
				}
			}
		}
		#endregion

		public bool IsResizeable
		{
			get
			{
				return (window_.ResizeMode != System.Windows.ResizeMode.NoResize);
			}
			set
			{
				window_.ResizeMode = value ? System.Windows.ResizeMode.CanResize : System.Windows.ResizeMode.NoResize;
			}
		}
		#endregion

		#region Commands
		#region ClosingCommand
		DelegateCommand closingCommand_ = null;
		public DelegateCommand ClosingCommand
		{
			get
			{
				return closingCommand_ ?? (closingCommand_ = new DelegateCommand {
					ExecuteHandler = param => {
						var e = param as CancelEventArgs;
						if (e == null) { return; }
						if (!shouldntDenyClosing_ && HideOnClose) {
							e.Cancel = true;
						}
					}
				});
			}
		}
		#endregion 
		#endregion // Commands

		public event EventHandler<WindowResizedEventArgs> Resized;

		public event EventHandler<WindowMovedEventArgs> Moved;

		public event EventHandler<WindowStateChangedEventArgs> StateChanged;

		public event EventHandler Showed;

		public event EventHandler Hidden;

		public event EventHandler Closed;

		public event EventHandler<WindowShowingEventArgs> Creating;
	}
}
