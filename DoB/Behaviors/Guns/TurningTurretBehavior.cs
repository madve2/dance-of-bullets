using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;

namespace DoB.Behaviors
{
	public class TurningTurretBehavior : TurretBehaviorBase
	{
		public double TurnAfterReload { get; set; }
		public double TurnAfterShoot { get; set; }

		public double Alpha { get; set; }

		bool wasShooting = false;
		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			var isShooting = Capacity == 0 || remainingBullets > 0;
			if (isShooting && !wasShooting)
				Alpha += TurnAfterReload;

			wasShooting = isShooting;

			base.UpdateOverride(gameTime, gameObject);
		}

		protected override Bullet Fire(GameObject gameObject)
		{
			var bullet = base.Fire(gameObject);
			bullet.GeneralDirection = Alpha;
			Alpha += TurnAfterShoot;
			return bullet;
		}
	}
}
