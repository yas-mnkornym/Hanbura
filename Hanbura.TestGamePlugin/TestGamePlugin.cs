//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Controls;
//using Studiotaiha.Hanbura;
//using Studiotaiha.Hanbura.Plugins;

//namespace Hanbura.TestGamePlugin
//{
//	class TestGamePlugin : IGamePlugin
//	{
//		public string GameID
//		{
//			get
//			{
//				return "TestGame";
//			}
//		}

//		public string GameUrl
//		{
//			get
//			{
//				return "http://studio-taiha.net";
//			}
//		}

//		public void Register(WebBrowser webBrowser)
//		{
//		}

//		public void Unregister()
//		{
//		}

//		public void OnRequesting(string url, string body, string method)
//		{
//		}

//		public void OnResponsed(string url, string response, string method)
//		{
//		}

//		public event EventHandler LoginStarted;

//		public event EventHandler<LoginFinishedEventArgs> LoggedFinished;

//		public event EventHandler GameStarted;

//		public event EventHandler<GameErrorEventArgs> Error;

//		Guid pluginId_ = new Guid("E0974FA0-70FE-4530-BB15-2DFB5C5AB0A5");
//		public Guid PluginId
//		{
//			get
//			{
//				return pluginId_;
//			}
//		}

//		public string PluginName
//		{
//			get
//			{
//				return "テストプラグイン";
//			}
//		}

//		public string Author
//		{
//			get
//			{
//				return "スタジオ大破";
//			}
//		}

//		public string CopyrightString
//		{
//			get
//			{
//				return @"© 2015 スタジオ大破";
//			}
//		}

//		public string Uri
//		{
//			get
//			{
//				return "http://studio-taiha.net";
//			}
//		}

//		public Version Version
//		{
//			get
//			{
//				return Assembly.GetExecutingAssembly().GetName().Version;
//			}
//		}

//		public void OnLoaded()
//		{
//		}

//		public void OnUnloading()
//		{
//		}

//		public void Initialize(IPluginInfo pluginInfo)
//		{
//			pluginInfo.AlertManager.ShowInformationMessage("Initialize");
//		}
//	}
//}
