using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Xaml;
using System.IO;
using System.Diagnostics;

namespace DoB.Xaml
{
	public class Prototypes : CustomDictionary<string, IPrototype>
	{
		public static Prototypes All { get; set; }

		static Prototypes()
		{
			All = new Prototypes();
		}

		public static Prototypes LoadFrom( string path )
		{
			return (Prototypes)XamlServices.Parse( File.ReadAllText( path ) );
		}

		protected override void OnKeyAssigned( string key )
		{
			base.OnKeyAssigned( key );

			if( this != All )
				All.AsDictionary().Add( key, this[key] );
		}

		private IDictionary<string, IPrototype> AsDictionary()
		{
			return (IDictionary<string, IPrototype>)this;
		}
	}
}
