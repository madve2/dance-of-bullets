using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Xaml
{
	public abstract class ConstantBase : IPrototype
	{
		public abstract object GetValue();

		public IPrototype Clone()
		{
			return (IPrototype)this.MemberwiseClone();
		}
	}
}
