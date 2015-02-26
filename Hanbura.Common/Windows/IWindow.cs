using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{
	public interface IWindow
	{
		/// <summary>
		/// ウィンドウのタグを取得する
		/// </summary>
		string Tag { get; }

		/// <summary>
		/// オーナウィンドウを取得する
		/// </summary>
		IWindow Owner { get; }

		/// <summary>
		/// ウィンドウを表示する
		/// </summary>
		void Show();

		/// <summary>
		/// ウィンドウを非表示にする
		/// </summary>
		void Hide();

		/// <summary>
		/// ウィンドウを閉じる
		/// </summary>
		/// <remarks>
		/// Close()関数を呼び出すとウィンドウ管理システムからこのウィンドウが破棄される。
		/// 二度とShow()関数でウィンドウで開けない。
		/// </remarks>
		void Close();

		/// <summary>
		/// ウィンドウのキャプションを取得・設定する
		/// </summary>
		string Caption { get; set; }

		/// <summary>
		/// ウィンドウをモーダルダイアログとして表示する
		/// </summary>
		/// <returns>DialogResultの値</returns>
		bool? ShowDialog();

		/// <summary>
		/// ウィンドウのコンテンツを取得・設定する
		/// </summary>
		object Content { get; set; }

		/// <summary>
		/// ウィンドウの設定コンテンツを取得・設定する
		/// </summary>
		object SettingsContent { get; set; }

		/// <summary>
		/// ウィンドウの幅を取得・設定する
		/// </summary>
		double Width { get; set; }

		/// <summary>
		/// ウィンドウの高さを取得・取得する
		/// </summary>
		double Height { get; set; }

		/// <summary>
		/// ウィンドウの左端座標を取得・取得する
		/// </summary>
		double Left { get; set; }

		/// <summary>
		/// ウィンドウの上端座標を取得・取得する
		/// </summary>
		double Top { get; set; }

		/// <summary>
		/// ウィンドウの不透明度を取得・設定する
		/// </summary>
		double Opacity { get; set; }

		/// <summary>
		/// ウィンドウを最前面に表示するかどうかを示す値を取得・設定する
		/// </summary>
		bool TopMost { get; set; }

		/// <summary>
		/// 親ウィンドウの移動に追従するかどうかを取得・設定する
		/// </summary>
		bool TrackOwner { get; set; }

		/// <summary>
		/// 親ウィンドウからの独立を可能にするかどうかを示す値を取得・設定する
		/// </summary>
		bool CanBeIndependentFromOwner { get; set; }

		/// <summary>
		/// ウィンドウがリサイズ可能であるかどうかを取得・設定する
		/// </summary>
		/// <remarks>この設定をFalseにすると、ホストウィンドウのSizeToContentがWidthAndHeightに設定される。</remarks>
		bool IsResizeable { get; set; }

		/// <summary>
		/// 閉じるボタンが押された時、Close()ではなくHide()を行うかどうかを示す値を取得・設定する
		/// </summary>
		/// <remarks>ShowDialog()でウィンドウが表示された場合無視される。</remarks>
		bool HideOnClose { get; set; }

		/// <summary>
		/// ウィンドウが最小化可能であるかどうかを示す値を取得・設定する
		/// </summary>
		bool CanBeMinimized { get; set; }

		/// <summary>
		/// ウィンドウが最大化可能であるかどうかを示す値を取得・設定する
		/// </summary>
		bool CanBeMaximized { get; set; }

		/// <summary>
		/// ウィンドウに閉じるボタンを表示するかどうかを示す値を取得・設定する
		/// </summary>
		bool ShowCloseButton { get; set; }

		/// <summary>
		/// ウィンドウ設定を表示するかどうかを示す値を取得・設定する
		/// </summary>
		bool EnableSettings { get; set; }

		/// <summary>
		/// ウィンドウの可視状態を取得・設定する。
		/// </summary>
		bool IsVisible { get; set; }

		/// <summary>
		/// ウィンドウを最大化する。
		/// </summary>
		void Maximize();

		/// <summary>
		/// ウィンドウを最小化する。
		/// </summary>
		void Minimize();

		/// <summary>
		/// ウィンドウ表示に戻す。
		/// </summary>
		void Restore();

		/// <summary>
		/// ウィンドウのリサイズを通知するイベント。
		/// </summary>
		event EventHandler<WindowResizedEventArgs> Resized;

		/// <summary>
		/// ウィンドウの移動を通知するイベント。
		/// </summary>
		event EventHandler<WindowMovedEventArgs> Moved;

		/// <summary>
		/// ウィンドウの状態変更を通知するイベント。
		/// </summary>
		event EventHandler<WindowStateChangedEventArgs> StateChanged;

		/// <summary>
		/// ウィンドウが表示されたことを通知するイベント。
		/// </summary>
		event EventHandler Showed;

		/// <summary>
		/// ウィンドウが非表示になったこを通知するイベント。
		/// </summary>
		event EventHandler Hidden;

		/// <summary>
		/// ウィンドウが閉じられたことを通知するイベント。
		/// </summary>
		event EventHandler Closed;

		/// <summary>
		/// ウィンドウが生成される時に一度だけ呼び出される。
		/// コンテンツの作成などを行う。
		/// </summary>
		/// <remarks>
		/// 必ずUIスレッドで呼び出される。 
		/// </remarks>
		event EventHandler<WindowShowingEventArgs> Creating;
	}
		
}
