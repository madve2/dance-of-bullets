using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Xaml
{
	[ContentProperty("Value")]
	public class Constant<T> : ConstantBase
	{
		public T Value { get; set; }

		public override object GetValue()
		{
			return Value;
		}
	}
}
