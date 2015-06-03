using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

		/// <summary>
		/// 式からメンバ名を取得する。
		/// </summary>
		/// <typeparam name="MemberType">メンバの型</typeparam>
		/// <param name="expression">式</param>
		/// <returns>メンバ名</returns>
		public string GetMemberName<MemberType>(Expression<Func<MemberType>> expression)
		{
			return ((MemberExpression)expression.Body).Member.Name;
		}


		public event PropertyChangedEventHandler PropertyChanged;

		public event PropertyChangingEventHandler PropertyChanging;
	}
}
