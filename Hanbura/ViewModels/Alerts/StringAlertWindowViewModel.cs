using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Studiotaiha.Hanbura.Properties;
using Studiotaiha.Hanbura.Views.Alerts;

namespace Studiotaiha.Hanbura.ViewModels.Alerts
{
	internal sealed class StringAlertWindowViewModel : AlertWindowViewModelBase
	{
		public StringAlertWindowViewModel(
			string caption, // not null
			string message, // not null
			EAlertType alertType,
			IEnumerable<string> selections, // not null, not empty
			Exception exception = null,
			IDispatcher dispatcher = null)
			: base(dispatcher)
		{
			if (caption == null) { throw new ArgumentNullException("caption"); }
			if (selections == null) { throw new ArgumentNullException("selection"); }
			if (!selections.Any()) { throw new ArgumentException("selection is empty"); }

			Caption = caption;
			Message = message;
			AlertType = alertType;
			Exception = exception;
			Selections = new System.Collections.ObjectModel.ObservableCollection<string>(selections);
			Content = new StringAlertControl {
				DataContext = this
			};
		}

		/// <summary>
		/// 例外情報を文字列に変換する
		/// </summary>
		/// <param name="exception">例外情報</param>
		/// <param name="stringSelector">例外情報を文字列に変換する関数</param>
		/// <param name="indent">インデント</param>
		/// <returns>指定された関数で文字列に変換された例外情報</returns>
		public string ExceptionToString(
			Exception exception,
			Func<Exception, string> stringSelector,
			int indent = 0)
		{
			if (exception == null) { throw new ArgumentNullException("exception"); }

			StringBuilder sb = new StringBuilder();
			var level = 0;
			for (var ex = exception; ex != null; ex = ex.InnerException, level++) {
				for (int i = 0; i < level + indent; i++) {
					sb.Append("--");
				}
				if (level + indent != 0) {
					sb.Append(">");
				}

				// 例外情報を文字列に変換して追記
				sb.AppendLine(stringSelector(ex));

				// AggregateExceptionだったら中の全て文字列に変換して追記
				if (ex is AggregateException) {
					foreach (var innerEx in ((AggregateException)ex).InnerExceptions) {
						sb.AppendLine(ExceptionToString(innerEx, stringSelector, level + indent + 1));
					}
				}
			}

			return sb.ToString();
		}

		protected override bool IsCopyable
		{
			get
			{
				return true;
			}
		}

		[STAThread]
		protected override async void Copy()
		{
			try {
				var str = await Task.Run(() => {
					StringBuilder sb = new StringBuilder();

					// キャプションを追加
					sb.AppendFormat("【{0}】", Caption);
					sb.AppendLine();

					// メッセージを追加
					sb.AppendLine(Message);

					// 例外情報を追加
					if (Exception != null) {
						sb.AppendLine();
						sb.AppendLine("*** 例外メッセージ ***");
						sb.AppendLine(ExceptionToString(Exception, ex => ex.Message));

						sb.AppendLine();
						sb.AppendLine("*** 例外詳細 ***");
						sb.AppendLine(Exception.ToString());
					}
					return sb.ToString();
				});

				// WinFormsのだとオープン失敗した時も自動でリトライしてくれる。優しい。すき。
				System.Windows.Forms.Clipboard.SetText(str, System.Windows.Forms.TextDataFormat.Text);
			}
			catch (Exception ex) {
				MessageBox.Show(
					string.Format("メッセージのコピーに失敗しました。\n\n【例外情報】\n{0}", ex.ToString()),
					"エラー", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
		}

		#region Bindings
		#region Message
		string message_;
		public string Message
		{
			get
			{
				return message_;
			}
			set
			{
				SetValue(ref message_, value);
			}
		}
		#endregion

		#region Exception
		Exception exception_;
		public Exception Exception
		{
			get
			{
				return exception_;
			}
			set
			{
				SetValue(ref exception_, value);
			}
		}
		#endregion

		#endregion // Bindings

	}
}
