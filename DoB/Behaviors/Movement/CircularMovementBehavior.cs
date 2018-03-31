using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using Microsoft.Xna.Framework;
using DoB.Utility;
using System.Diagnostics;

namespace DoB.Behaviors
{
	public class CircularMovementBehavior : Behavior
	{
		public double Vdeg { get; set; }
		public double? Vper { get; set; }
		public double Adeg { get; set; }
		public double Aper { get; set; }

		public CircularMovementBehavior()
		{
		}

		public override void UpdateOverride(GameTime gameTime, GameObject g)
		{
			base.UpdateOverride(gameTime, g);
			double ms = gameTime.ElapsedGameTime.TotalMilliseconds;

			if( Vper.HasValue )
			{
				Vper += GameSpeedManager.ApplySpeed( Aper, ms );
			}
			else
			{
				Vdeg += GameSpeedManager.ApplySpeed( Adeg, ms );
			}

			var currPos = new Vector2((float)g.X, (float)g.Y);
			var oriPos = new Vector2((float)g.Ox, (float)g.Oy);
			double r = Vector2.Distance(currPos, oriPos);
			
			if (r == 0)
				return;

			var rot = Vper.HasValue ? GameSpeedManager.ApplySpeed( Vper.Value, ms ) / r : GameSpeedManager.ApplySpeed( Vdeg, ms );

			var vec1 = Vector2.Subtract(currPos, oriPos);
			var vec2 = new Vector2( (float)( vec1.X * Math.Cos( rot ) - vec1.Y * Math.Sin( rot ) ), (float)( vec1.X * Math.Sin( rot ) + vec1.Y * Math.Cos( rot ) ) );
			var vec3 = Vector2.Add(vec2, oriPos);
			g.MoveX += vec3.X - g.X;
			g.MoveY += vec3.Y - g.Y;
		}
	}
}
