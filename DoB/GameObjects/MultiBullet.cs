using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Utility;
using Microsoft.Xna.Framework;
using DoB.Xaml;

namespace DoB.GameObjects
{
	public class MultiBullet : Bullet
	{
		public double DuplicateCooldownMs { get; set; }
		public int Count { get; set; }
		public double DegIncrement { get; set; }

		private Cooldown duplicateCooldown = new Cooldown();
		private MultiBullet originalClone = null;

		public MultiBullet()
		{

		}

		public override void OnFirstUpdate(Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.OnFirstUpdate(gameTime);
			duplicateCooldown.Reset(DuplicateCooldownMs);
			if (Count > 1)
			{
				originalClone = (MultiBullet)this.Clone();
			}
		}

		bool hadOneFullUpdate = false;
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (!hadOneFullUpdate)
			{
				hadOneFullUpdate = true;
				return;
			}

			duplicateCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
			if (duplicateCooldown.IsElapsed && Count > 1)
			{
				var c = this.originalClone;
				this.originalClone = null;
				this.Count = 0;
				c.Count--;
				Game.Objects.Add( c );
				c.GeneralDirection += DegIncrement;
			}
		}

		public override IPrototype Clone()
		{
			var c = (MultiBullet)base.Clone();
			c.duplicateCooldown = new Cooldown();
			c.hadOneFullUpdate = false;
			return c;
		}
	}
}
