using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using Microsoft.Xna.Framework;

namespace DoB.Behaviors
{
	public class AimingTurretBehavior : TurretBehaviorBase
	{
		protected double idealDirection = 0;

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			double corr = GameObject.Game.Player.X - gameObject.X > 0 ? 0 : Math.PI;
			idealDirection = corr + Math.Atan((GameObject.Game.Player.Y - gameObject.Y) / (GameObject.Game.Player.X - gameObject.X));
			base.UpdateOverride(gameTime, gameObject);
		}


		protected override Bullet Fire(GameObject gameObject)
		{
			var bullet = base.Fire(gameObject);
			bullet.GeneralDirection = idealDirection;
			return bullet;
		}
	}
}
