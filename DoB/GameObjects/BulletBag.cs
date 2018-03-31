using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace DoB.GameObjects
{
	[ContentProperty("Bullets")]
	public class BulletBag : Bullet
	{
		public List<Bullet> Bullets { get; set; }

		public BulletBag()
		{
			Bullets = new List<Bullet>();
		}

		public override IPrototype Clone()
		{
			var c = (BulletBag)base.Clone();
			c.Bullets = new List<Bullet>();
			for (int i = 0; i < Bullets.Count; i++)
			{
				c.Bullets.Add((Bullet)Bullets[i].Clone());
			}
			return c;
		}

		public override void RemoveSelf()
		{
			base.RemoveSelf();
			for (int i = 0; i < Bullets.Count; i++)
			{
				Bullets[i].RemoveSelf();
			}
		}

		public override void OnFirstUpdate(GameTime gameTime)
		{
			base.OnFirstUpdate(gameTime);
			for (int i = 0; i < Bullets.Count; i++)
			{
				var b = Bullets[i];
				b.X += X;
				b.Y += Y;
				b.R += R;
				b.Tint = Tint; //TODO: maybe we shouldn't override the bullet's tint if the bag's tint is not set
				b.GeneralDirection += GeneralDirection;
				Game.Objects.Add( b );
			}

			base.RemoveSelf(); //No need for the container anymore
		}
	}
}
