using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura
{
	internal sealed class LoggingService
	{
		static LoggingService current_;

		private LoggingService(ILogger logger)
		{
			if (logger == null) { throw new ArgumentNullException("logger"); }
			Logger = logger;
		}

		public static void Initialize(ILogger logger)
		{
			current_ = new LoggingService(logger);
		}

		public static LoggingService Current
		{
			get
			{
				if (current_ == null) { throw new InvalidOperationException("LoggingService is not initialized."); }
				return current_;
			}
		}

		public ILogger Logger { get; private set; }

		public ILogger GetLogger(string tag)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			return Logger.CreateChild(tag);
		}

		public ILogger GetLogger(object owner)
		{
			if (owner == null) { throw new ArgumentNullException("owner"); }
			var type = owner.GetType();
			return GetLogger(type.Name);
		}
	}
}
