using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura.Windows
{

	/// <summary>
	/// Windowに関連する例外
	/// </summary>
	[Serializable]
	public class WindowException : Exception
	{
		public WindowException() { }
		public WindowException(string message) : base(message) { }
		public WindowException(string message, Exception inner) : base(message, inner) { }
		protected WindowException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}

	/// <summary>
	/// 既に同じタグのウィンドウが存在することを表す例外
	/// </summary>
	[Serializable]
	public class WindowTagAlreadyExistsException : WindowException
	{
		public WindowTagAlreadyExistsException()
			: base("既に同じタグのウィンドウが存在します。")
		{ }

		public WindowTagAlreadyExistsException(string tag)
			: base("既に同じタグ(" + tag + ")のウィンドウが存在します。")
		{ }

		protected WindowTagAlreadyExistsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
