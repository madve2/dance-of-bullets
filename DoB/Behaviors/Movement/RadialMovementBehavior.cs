using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using System.Diagnostics;
using DoB.Utility;

namespace DoB.Behaviors
{
	public class RadialMovementBehavior : Behavior
	{
		public double Vr { get; set; }

		public double? DirectionOverride { get; set; }

		public override void UpdateOverride(GameTime gameTime, GameObject g)
		{
			base.UpdateOverride(gameTime, g);
			double ms = gameTime.ElapsedGameTime.TotalMilliseconds;
			double moveStep = GameSpeedManager.ApplySpeed(Vr, ms);

			var dir = DirectionOverride ?? g.GeneralDirection;
			g.MoveX += Math.Cos(dir) * moveStep;
			g.MoveY += Math.Sin(dir) * moveStep;
		}
	}
}
