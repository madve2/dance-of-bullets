using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Drawers;
using DoB.Behaviors;
using DoB.Utility;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using DoB.Xaml;

namespace DoB.GameObjects
{
	public class Enemy : Collideable, IHealth
	{
		public string EventOnDeath { get; set; }

		private Cooldown damageCooldown = new Cooldown();
		public double Lives
		{
			get
			{
				return Health.Amount;
			}
			set
			{
				Health.Amount = value;
			}
		}

		public Health Health { get; set; }

		public Enemy()
		{
			Health = new Health(this, 0);
		}

		public override void OnFirstUpdate(GameTime gameTime)
		{
			base.OnFirstUpdate(gameTime);
			originalTint = Tint;
		}

		private Color originalTint;
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			damageCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
			if (damageCooldown.IsElapsed)
			{
				Tint = originalTint;
			}
		}

		public override IPrototype Clone()
		{
			var b = (Enemy)base.Clone();
			b.Health = new Health(b, this.Lives);
			return b;
		}

		public override void CollideWith(Collideable c)
		{
			c.Collide(this);
		}

		public override void Collide(Bullet b)
		{
			if (b.IsFriendly)
			{
				if (damageCooldown.IsElapsed)
				{
					Health.Hit();
					damageCooldown.Reset(100);
					originalTint = Tint;
					Tint = Color.FromNonPremultiplied(255, 50, 50, 255);
				}
			}
		}

		public override void Collide(Player p)
		{
		}

		public override void Collide(Enemy e)
		{
		}

		public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}

		public override void RemoveSelf()
		{
			base.RemoveSelf();
			if( !string.IsNullOrEmpty( EventOnDeath ) )
				EventBroker.FireEvent( EventOnDeath );
		}
	}
}
