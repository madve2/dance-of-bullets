using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.Utility;
using DoB.Xaml;
using DoB.GameObjects;

namespace DoB.Components
{
	public class EnemySpawner : ComponentBase
	{
		Cooldown cooldown = new Cooldown();

		public double CooldownMs { get; set; }
		public int Count { get; set; }

		public string ReferenceName { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
		public double XIncrement { get; set; }
		public double YIncrement { get; set; }
		public double RIncrement { get; set; }
		private double totalRIncrement = 0;

		protected override void UpdateOverride( GameTime gameTime )
		{
			if( Count < 1 )
				return;

			cooldown.Update( gameTime.ElapsedGameTime.TotalMilliseconds );
			if( cooldown.IsElapsed )
			{
				var c = (GameObject)Prototypes.All[ReferenceName].Clone();
				GameObject.Game.Objects.Add( c );
				c.X = X;
				c.Y = Y;
				c.R += totalRIncrement;
				X += XIncrement;
				Y += YIncrement;
				totalRIncrement += RIncrement;
				Count--;

				cooldown.Reset( CooldownMs );
			}
		}
	}
}
