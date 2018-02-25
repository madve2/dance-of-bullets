using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Xaml
{
    public class DegreesExtension : MarkupExtension
    {
        public double Degrees { get; set; }
        public DegreesExtension()
        {
        }

        public DegreesExtension(double degrees)
        {
            Degrees = degrees;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return (Math.PI / 180) * Degrees;
        }
    }
}
