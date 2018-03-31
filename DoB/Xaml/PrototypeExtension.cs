using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Xaml
{
	public class PrototypeExtension : MarkupExtension
	{
		public string Name { get; set; }

		public PrototypeExtension()
		{

		}

		public PrototypeExtension(string name)
		{
			Name = name;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return Prototypes.All == null ? null : Prototypes.All[Name].Clone();
		}
	}
}
