using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura
{
	public static class LoggerExt
	{
		#region Error
		public static void Error(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Error, ex);
		}

		public static void ErrorFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Error(string.Format(message, args), ex);
		}
		public static void ErrorFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.ErrorFormat((Exception)null, message, args);
		}
		#endregion

		#region Fatal
		public static void Fatal(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Fatal, ex);
		}

		public static void FatalFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Fatal(string.Format(message, args), ex);
		}
		public static void FatalFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.FatalFormat((Exception)null, message, args);
		}
		#endregion


		#region Warning
		public static void Warning(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Warning, ex);
		}

		public static void WarningFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Warning(string.Format(message, args), ex);
		}
		public static void WarningFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.WarningFormat((Exception)null, message, args);
		}
		#endregion

		#region Information
		public static void Information(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Information, ex);
		}

		public static void InformationFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Information(string.Format(message, args), ex);
		}
		public static void InformationFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.InformationFormat((Exception)null, message, args);
		}
		#endregion

		#region Debug
		public static void Debug(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Debug, ex);
		}

		public static void DebugFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Debug(string.Format(message, args), ex);
		}
		public static void DebugFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.DebugFormat((Exception)null, message, args);
		}
		#endregion

		#region Verbose
		public static void Verbose(
			this ILogger logger,
			string message,
			Exception ex = null)
		{
			logger.Log(message, ELogLevel.Verbose, ex);
		}

		public static void VerboseFormat(
			this ILogger logger,
			Exception ex,
			string message,
			params object[] args)
		{
			logger.Verbose(string.Format(message, args), ex);
		}
		public static void VerboseFormat(
			this ILogger logger,
			string message,
			params object[] args)
		{
			logger.VerboseFormat((Exception)null, message, args);
		}
		#endregion
	}
}
