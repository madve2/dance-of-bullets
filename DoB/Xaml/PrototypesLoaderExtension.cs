using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace DoB.Xaml
{
	public class PrototypesLoaderExtension: MarkupExtension
	{
		public string FileNames { get; set; }

		public PrototypesLoaderExtension()
		{

		}

		public PrototypesLoaderExtension(string fileNames)
		{
			FileNames = fileNames;
		}

		public override object ProvideValue( IServiceProvider serviceProvider )
		{
			var result = new List<Prototypes>();
			foreach( var fileName in FileNames.Split(';') )
			{
				result.Add(Prototypes.LoadFrom( "StageData\\" + fileName + ".xaml" ));
			}
			return result;
		}
	}
}
