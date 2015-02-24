using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Models.Common.Logging
{
	internal class LogExpoter : IDisposable
	{
		static readonly string StartTag = "*Start";
		static readonly string EndTag = "*End";
		CompositeDisposable disposables_ = new CompositeDisposable();
		StreamWriter writer_;

		public LogExpoter(
			ILogger logger,
			string fileName)
		{
			if (logger == null) { throw new ArgumentNullException("logger"); }
			if (fileName == null) { throw new ArgumentNullException("fileName"); }

			// ライター初期化
			writer_ = new StreamWriter(fileName, true, Encoding.UTF8, 2048);
			writer_.AutoFlush = true;
			writer_.WriteLine(StartTag);

			// ログイベントをサブスクライブ
			disposables_.Add(logger.LogSubject.Subscribe(OnLogged));
		}

		void OnLogged(LogData data)
		{
			var time = DateTimeOffset.Now;
			var tokens = new string[]{
				time.ToString("yyyy/MM/dd HH:mm:ss.fff zzz"),
				data.Level.ToString(),
				data.Tag,
				data.ParentTags != null ? 
					string.Join(",", data.ParentTags.Select(x => {
						if(x.Contains(',')){
							return string.Format(@"""{0}""", x.Replace(@"""", @"\"""));
						}
						else{
							return x;
						}
					})) : "",
				data.Message,
				data.Exception == null ? "" : data.Exception.ToString(),
				data.FileName,
				data.LineNumber.ToString(),
				data.MemberName
			};

			var str = string.Join(",", tokens.Select(x => {
					if(x.Contains(',')){
						return string.Format(@"""{0}""", x.Replace(@"""", @"\"""));
					}
					else{
						return x;
					}
			}));

			writer_.WriteLine(str);
		}

		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				if(writer_ != null){
					writer_.WriteLine(EndTag);
					writer_.Dispose();
				}

				disposables_.Dispose();
				disposables_ = null;
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);				
		}
	}
}
