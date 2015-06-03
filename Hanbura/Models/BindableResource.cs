using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Hanbura.Properties;

namespace Studiotaiha.Hanbura.Models
{
	internal class BindableResource
	{
		static readonly BindableResource current_ = new BindableResource();
		public static BindableResource Current
		{
			get
			{
				return current_;
			}
		}

		Resources resources_ = new Resources();
		public Resources Resources
		{
			get
			{
				return resources_ ?? (resources_ = new Resources());
			}
		}
	}
}
