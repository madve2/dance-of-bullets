using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Extensions;

namespace DoB.Drawers
{
	public class HealthbarDrawer : RangeDrawer
	{
		Cooldown delay = new Cooldown();

		private double _DelayMs;
		public double DelayMs
		{
			get { return _DelayMs; }
			set
			{
				_DelayMs = value;
				delay.Reset( value );
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
		{
			delay.Update( gameTime.ElapsedMs() );
			if( !delay.IsElapsed )
				return;

			var rate = (((IHealth)gameObject).Health.Amount / (double)((IHealth)gameObject).Health.OriginalAmount);
			DrawInner(spriteBatch, rate);
		}
	}
}
