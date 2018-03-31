using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Drawers;
using Microsoft.Xna.Framework;
using DoB.Behaviors;

namespace DoB.GameObjects
{
	public abstract class Collideable : GameObject
	{
		public bool IsFriendly { get; set; }

		public Collideable()
		{
			Drawers.Add(new HitboxDrawer());
			Behaviors.Add( new SpawnOnRemovalBehavior { Prototype = new Decoration() { BaseTexture = "explosion", LengthMs=600, LengthVariance=300 }, InheritSize = true } );
			CollisionBoxScale = 1;
		}

		public double CollisionBoxScale { get; set; }
		
		public double CollisionRadius
		{
			get
			{
				return R * CollisionBoxScale;
			}
		}

		public abstract void CollideWith(Collideable c);


		public virtual void Collide(Bullet b)
		{
		}

		public virtual void Collide(Player p)
		{
		}

		public virtual void Collide(Enemy e)
		{
		}

		public Rectangle GetCollisionRectangle(bool applyPendingMovement = false)
		{
			return applyPendingMovement ?
				new Rectangle((int)(X + MoveX - CollisionRadius), (int)(Y + MoveY - CollisionRadius), (int)(2 * CollisionRadius), (int)(2 * CollisionRadius)) :
				new Rectangle((int)(X - CollisionRadius), (int)(Y - CollisionRadius), (int)(2 * CollisionRadius), (int)(2 * CollisionRadius));
		}
	}
}
