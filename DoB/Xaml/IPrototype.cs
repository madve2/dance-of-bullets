using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Xaml
{
	public interface IPrototype
	{
		IPrototype Clone();
	}
}
