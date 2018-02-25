using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;

namespace DoB.Behaviors
{
	public class SpawnOnRemovalBehavior : Behavior
	{
		public GameObject Prototype { get; set; }
		public bool InheritSize { get; set; }

		private double x;
		private double y;
		private double r;

		public override void UpdateOverride( Microsoft.Xna.Framework.GameTime gameTime, GameObject gameObject )
		{
			base.UpdateOverride( gameTime, gameObject );
			x = gameObject.X;
			y = gameObject.Y;
			r = gameObject.R;
		}

		public override void OnRemoval( GameObject gameObject )
		{
			if( !GameObject.Game.GameplayRectangle.Contains( (int)x, (int)y ) )
				return;

			var c = (GameObject)Prototype.Clone();
			GameObject.Game.Objects.Add( c );
			c.X = x;
			c.Y = y;
			if( InheritSize )
				c.R = r;
		}
	}
}
