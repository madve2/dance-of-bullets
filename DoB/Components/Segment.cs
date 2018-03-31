using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Windows.Markup;

namespace DoB.Components
{
	[ContentProperty( "Components" )]
	public class Segment : ComponentBase
	{
		public List<IComponent> Components { get; set; }

		public Segment()
		{
			Components = new List<IComponent>();
		}

		protected override void UpdateOverride( GameTime gameTime )
		{
			for (int i = 0; i < Components.Count; i++)
			{
				Components[i].Update(gameTime);
			}
		}
	}
}
