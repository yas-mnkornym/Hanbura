﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Hanbura
{
	public class NotificationObject : Dispatchable, INotifyPropertyChanged, INotifyPropertyChanging
	{
		public NotificationObject(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		/// <summary>
		/// プロパティの変更完了を通知する
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null) {
				Dispatch(() => {
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				});
			}
		}

		/// <summary>
		/// プロパティの変更開始を通知する
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanging([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanging != null) {
				Dispatch(() => {
					PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
				});
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;
	}
}
