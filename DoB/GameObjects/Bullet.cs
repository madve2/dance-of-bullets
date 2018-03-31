using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB;
using DoB.Behaviors;
using DoB.Drawers;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace DoB.GameObjects
{
	public class Bullet : Collideable
	{
		private DieOffScreenBehavior dieOffScreen;

		public Bullet()
		{
			//some sensible defaults
			R = 7; 
			dieOffScreen = new DieOffScreenBehavior();
			Behaviors.Add(dieOffScreen);
		}

		public bool DieOffScreen
		{
			get
			{
				return Behaviors.Contains( dieOffScreen );
			}

			set
			{
				if( value )
					Behaviors.Add( dieOffScreen );
				else
					Behaviors.Remove( dieOffScreen );
			}
		}

		public override void CollideWith(Collideable c)
		{
			c.Collide(this);
		}

		public override void Collide(Bullet b)
		{
		}

		public override void Collide(Player p)
		{
			if (!IsFriendly)
				this.RemoveSelf();
		}

		public override void Collide(Enemy e)
		{
			if (IsFriendly)
				this.RemoveSelf();
		}
	}
}
