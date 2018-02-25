using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Xaml
{
    public class ConstantExtension : MarkupExtension
    {
        public string Name { get; set; }

        public ConstantExtension()
        {

        }

        public ConstantExtension(string name)
        {
            Name = name;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
			return ( (ConstantBase)Prototypes.All[Name] ).GetValue();
        }
    }
}
