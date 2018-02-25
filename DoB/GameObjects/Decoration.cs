using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Utility;
using Microsoft.Xna.Framework;
using DoB.Extensions;
using System.Diagnostics;

namespace DoB.GameObjects
{
	public class Decoration : GameObject
	{
		public double LengthMs { get; set; }
		public double LengthVariance { get; set; }
		private Cooldown lengthCooldown;
		public Decoration()
		{
			LengthMs = double.PositiveInfinity;
		}

		static Random rnd = new Random();
		public override void OnFirstUpdate( GameTime gameTime )
		{
			base.OnFirstUpdate( gameTime );
			var finalLength = LengthMs + LengthVariance * 2 * (rnd.NextDouble() - 0.5) ;
			lengthCooldown = new Cooldown(finalLength );
		}

		public override void Update( GameTime gameTime )
		{
			base.Update( gameTime );
			lengthCooldown.Update( gameTime.ElapsedMs() );
			if( lengthCooldown.IsElapsed )
				this.RemoveSelf();
		}
	}
}
