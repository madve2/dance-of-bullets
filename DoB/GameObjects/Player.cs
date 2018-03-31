using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DoB.Drawers;
using DoB.Behaviors;
using DoB.Utility;
using Microsoft.Xna.Framework;
using DoB.Xaml;
using DoB.Extensions;

namespace DoB.GameObjects
{
	public class Player : Collideable, IHealth
	{
		public Health Health { get; set; }
		public Consumable Mana { get; set; }
		public Consumable Payback { get; set; }
		public bool IsManaActive { get; set; }
		public bool IsPaybackActive { get; set; }

		internal bool debug_IsInvincible = false;

		private PlayerControlBehavior controlBehavior = new PlayerControlBehavior();
		private Cooldown damageCooldown = null;
		private Cooldown increaseDifficultyCooldown = null;
		private double increaseDifficultyDelay = 20000;
		private RangeDrawer manaDrawer = new RangeDrawer() { X = 10, Y = 10, Width = 300, Height = 10, Color = Color.FromNonPremultiplied(0, 19, 127, 255), BorderColor = Color.Black };
		private double manaDrainSpeed = 10; //Note: this is also affected by the slowdown
		private double manaRegainSpeed = 1;
		private RangeDrawer paybackDrawer = new RangeDrawer() { X = 340, Y = 10, Width = 300, Height = 10, Color = Color.FromNonPremultiplied( 127, 19, 19, 255 ), BorderColor = Color.Black };
		private double paybackDrainSpeed = 250;
		private double paybackRegainSpeed = 10;

		public Player()
		{
			BaseTexture = "player_new";
			R = 4 * textureScale;
			Behaviors.Add(controlBehavior);
			Behaviors.Add(new FramedMovementBehavior());
			Drawers.Add(manaDrawer);
			Drawers.Add( paybackDrawer );
			Health = new Health(this, 9);
			Mana = new Consumable(20);
			Payback = new Consumable(1000);
			IsFriendly = true;
			Mana.Amount = 0;
			Payback.Amount = 0;
		}

		const double textureScale = 0.6;
		const double textureWidth = 214 * textureScale;
		const double textureHeight = 66 * textureScale;
		public override Rectangle GetDrawingRectangle()
		{
			return new Rectangle((int)(X - textureWidth / 2 ), (int)(Y - textureHeight / 2 ) + (int)(8*textureScale), (int)textureWidth, (int)textureHeight);
		}

		public override void OnFirstUpdate(GameTime gameTime)
		{
			base.OnFirstUpdate(gameTime);
			if (damageCooldown == null)
			{
				damageCooldown = new Cooldown();
			}
			if (increaseDifficultyCooldown == null)
			{
				increaseDifficultyCooldown = new Cooldown();
				increaseDifficultyCooldown.Reset(increaseDifficultyDelay);
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			damageCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
			if (damageCooldown.IsElapsed)
			{
				Tint = Color.White;
			}

			increaseDifficultyCooldown.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
			if (increaseDifficultyCooldown.IsElapsed)
			{
				GameSpeedManager.IncreaseDifficulty();
				increaseDifficultyCooldown.Reset(increaseDifficultyDelay);
			}

			UpdateMana( gameTime );
			UpdatePayback( gameTime );
		}

		private void UpdateMana( GameTime gameTime ) //Slow down time
		{
			if( IsManaActive )
			{
				Mana.Amount -= GameSpeedManager.ApplySpeed( manaDrainSpeed, gameTime.ElapsedMs() );
			}
			else
			{
				Mana.Amount = MathHelper.Clamp( (float)( Mana.Amount + GameSpeedManager.ApplySpeed( manaRegainSpeed, gameTime.ElapsedMs() ) ), 0, (float)Mana.OriginalAmount );
			}

			manaDrawer.Rate = Mana.Amount / (double)Mana.OriginalAmount;
			if( Mana.HasRunOut )
			{
				StopMana();
			}
		}

		private void UpdatePayback( GameTime gameTime )//"Rain of bullets"
		{
			if( IsPaybackActive )
			{
				Payback.Amount -= GameSpeedManager.ApplySpeed( paybackDrainSpeed, gameTime.ElapsedMs() );
			}
			else
			{
				Payback.Amount = MathHelper.Clamp( (float)( Payback.Amount + GameSpeedManager.ApplySpeed( paybackRegainSpeed, gameTime.ElapsedMs() ) ), 0, (float)Payback.OriginalAmount );
				if( Payback.Amount == Payback.OriginalAmount )
					paybackDrawer.Color = Color.DarkGreen;
			}

			paybackDrawer.Rate = Payback.Amount / (double)Payback.OriginalAmount;
			if( Payback.HasRunOut )
			{
				IsPaybackActive = false;
			}
		}

		public void StartMana()
		{
			IsManaActive = true;
			GameSpeedManager.GlobalModifier = 0.5;
			HitboxDrawer.AreVisible = true;
		}

		public void StopMana()
		{
			IsManaActive = false;
			GameSpeedManager.GlobalModifier = 1;
			HitboxDrawer.AreVisible = false;
		}

		public void ActivatePayback()
		{
			if( !IsPaybackActive && Payback.Amount == Payback.OriginalAmount )
			{
				IsPaybackActive = true;
				paybackDrawer.Color = Color.FromNonPremultiplied( 127, 19, 19, 255 );
				Game.ClearBullets();
			}
		}

		public override IPrototype Clone()
		{
			throw new NotSupportedException();
		}

		public override void CollideWith(Collideable c)
		{
			c.Collide(this);
		}

		public override void Collide(Bullet b)
		{
			if (!b.IsFriendly)
			{
				if (damageCooldown.IsElapsed && !debug_IsInvincible)
				{
					Health.Hit();
					damageCooldown.Reset(3000);
					GameSpeedManager.ReduceDifficulty();
					increaseDifficultyCooldown.Reset(increaseDifficultyDelay);
					Tint = Color.FromNonPremultiplied(255, 255,255, 125);
				}
			}
		}

		public override void Collide(Player p)
		{
		}

		public override void Collide(Enemy e)
		{
		}

		Random rnd = new Random();
		public void Shoot()
		{
			var pMain = new PlayerBullet (IsPaybackActive ? 1400 : 700) { X = this.X + 90 * textureScale, Y = this.Y + (100 * (IsPaybackActive ? rnd.NextDouble() - 0.5 : 0)) , R = 10, Tint = IsPaybackActive ? Color.White  : Color.White };
			Game.Objects.Add(pMain);
			var pSide = new PlayerBullet(IsPaybackActive ? 1400 : 700) { X = this.X + 20 * textureScale, Y = this.Y + 28 * textureScale + (100 * (IsPaybackActive ? rnd.NextDouble() - 0.5 : 0)), R = 10, Tint = IsPaybackActive ? Color.White : Color.White };
			Game.Objects.Add(pSide);
		}
	}
}
