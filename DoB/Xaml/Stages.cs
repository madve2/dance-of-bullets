using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DoB.Components;

namespace DoB.Xaml
{
	public class Stages : List<Stage>
	{
		public static Stages Current { get; set; }

		public List<Prototypes> PrototypePacks { get; set; }

		public Stages()
		{
			Current = this;
			PrototypePacks = new List<Prototypes>();
		}
	}
}
