using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Behaviors;
using DoB.GameObjects;
using Microsoft.Xna.Framework;
using DoB.Utility;

namespace DoB.Behaviors
{
	public class XYMovementBehavior : Behavior
	{
		public double Vx { get; set; }
		public double Vy { get; set; }

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			base.UpdateOverride(gameTime, gameObject);
			double ms = gameTime.ElapsedGameTime.TotalMilliseconds;
			gameObject.MoveX += GameSpeedManager.ApplySpeed(Vx, ms);
			gameObject.MoveY += GameSpeedManager.ApplySpeed(Vy, ms);
		}
	}
}
