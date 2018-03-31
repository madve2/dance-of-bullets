using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using DoB.Utility;
using System.Diagnostics;
using DoB.Xaml;

namespace DoB.Behaviors
{
	[Obsolete("Use TurningTurretBehavior instead.")]
	public class SpinningTurretBehavior : TurretBehaviorBase
	{
		public double VdegReloading { get; set; }
		public double VdegShooting { get; set; }
		public double Vdeg
		{
			set
			{
				VdegShooting = value;
				VdegReloading = value;
			}
		}

		public double Alpha { get; set; }

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			var isShooting = Capacity == 0 || remainingBullets > 0;
			var vdeg = isShooting ? VdegShooting : VdegReloading;
			Alpha += GameSpeedManager.ApplySpeed(vdeg, gameTime.ElapsedGameTime.TotalMilliseconds);

			base.UpdateOverride(gameTime, gameObject);
		}

		protected override Bullet Fire(GameObject gameObject)
		{
		   var bullet = base.Fire(gameObject);
			bullet.GeneralDirection = Alpha;
			return bullet;
		}
	}
}
