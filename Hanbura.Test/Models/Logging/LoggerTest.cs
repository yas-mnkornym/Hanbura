using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Hanbura.Models.Common.Logging;

namespace Studiotaiha.Hanbura.Test.Models.Logging
{
	[TestClass]
	public class LoggerTest
	{
		[TestMethod]
		public void LogTest()
		{
			var tag = "tag";
			var msg = "msg";
			var ex = new Exception("unko");
			var level = ELogLevel.Error;
			// タグのテスト
			var logger = new Logger(tag);
			Assert.AreEqual(tag, logger.Tag);

			// イベントのテスト
			logger.Logged += (_, e) => {
				var data = e.LogData;
				Assert.AreEqual(tag, data.Tag);
				Assert.AreEqual(msg, data.Message);
				Assert.AreEqual(ex, data.Exception);
				Assert.AreEqual(level, data.Level);
			};
			logger.Log(msg, level, ex);
		}

		[TestMethod]
		public void ChildLogTest()
		{
			var N = 10;
			var msg = "msg";
			var ex = new Exception("unko");
			var level = ELogLevel.Error;
			List<ILogger> loggers = new List<ILogger>();
			ILogger root = new Logger("0");
			root.Logged += (_, e) => {
				var data = e.LogData;
				Assert.AreEqual(msg, data.Message);
				Assert.AreEqual(ex, data.Exception);
				Assert.AreEqual(level, data.Level);
			};

			var parent = root;
			for (int j = 1; j < N; j++) {
				var tag = j.ToString();
				var child = parent.CreateChild(tag);
				Assert.AreEqual(parent, child.Parent);

				child.Logged += (_, e) => {
					var data = e.LogData;
					Assert.AreEqual(msg, data.Message);
					Assert.AreEqual(ex, data.Exception);
					Assert.AreEqual(level, data.Level);
					for (int i = 0; i < j-1; i++) {
						Assert.AreEqual(i.ToString(), data.ParentTags[i]);
					}
				};

				loggers.Add(child);
				parent = child;
			}

			var bottom = loggers.Last();
			bottom.Log(msg, level, ex);
		}
	}
}
