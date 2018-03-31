using DoB.GameObjects;
using DoB.Utility;
using Microsoft.Xna.Framework;
using DoB.Xaml;

namespace DoB.Behaviors
{
	public abstract class TurretBehaviorBase : GunBehaviorBase
	{
		public double ReloadDurationMs { get; set; }
		public int Capacity { get; set; }
		public double GunCooldownMs { get; set; }
		
		public bool NeedsReloadFirst { get; set; }
		public bool NeedsCooldownAfterReload { get; set; }
		
		protected Cooldown gunCooldown = null;
		protected Cooldown reloadCooldown = null;
		
		protected int remainingBullets = 0;

		public override void ResetTimers()
		{
			base.ResetTimers();
			gunCooldown = null;
			reloadCooldown = null;
		}

		public override IPrototype Clone()
		{
			var c = (TurretBehaviorBase)base.Clone();
			c.gunCooldown = null;
			c.reloadCooldown = null;
			return c;
		}

		public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
		{
			base.OnFirstUpdate(gameTime, gameObject);
			gunCooldown = new Cooldown(NeedsCooldownAfterReload ? GunCooldownMs : 0);
			reloadCooldown = new Cooldown(ReloadDurationMs);
			if (!NeedsReloadFirst)
				remainingBullets = Capacity;
		}
		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			base.UpdateOverride(gameTime, gameObject);
			var isShooting = Capacity == 0 || remainingBullets > 0;

			if (!isShooting)
			{
				reloadCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

				if (reloadCooldown.IsElapsed)
				{
					gunCooldown.Reset(NeedsCooldownAfterReload ? GunCooldownMs : 0);
					remainingBullets = Capacity;
				}

				return;
			}
			
			gunCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
			if (isShooting && gunCooldown.IsElapsed)
			{
				Fire(gameObject);
				remainingBullets--;
				gunCooldown.Reset(GunCooldownMs);

				if (remainingBullets < 1)
					reloadCooldown.Reset(ReloadDurationMs);
			}
		}

		protected virtual Bullet Fire(GameObject gameObject)
		{
			var bullet = (Bullet)BulletPrototype.Clone();
			GameObject.Game.Objects.Add( bullet );
			bullet.X = gameObject.X;
			bullet.Y = gameObject.Y;
			if (BulletTintOverride != null)
				bullet.Tint = BulletTintOverride.Value;
			if (BulletTextureOverride != null)
				bullet.BaseTexture = BulletTextureOverride;
			return bullet;
		}
	}
}
