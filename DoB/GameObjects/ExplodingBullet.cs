using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Windows.Markup;
using DoB.Behaviors;

namespace DoB.GameObjects
{
	[ContentProperty( "ClusterBulletPrototype" )]
	public class ExplodingBullet : Bullet
	{
		public int ClusterCount { get; set; }
		public Bullet ClusterBulletPrototype { get; set; }
		public double Vexpl { get; set; }

		public override void OnFirstUpdate( GameTime gameTime )
		{
			base.OnFirstUpdate( gameTime );
			var degIncrement = MathHelper.TwoPi / ClusterCount;
			for( int i = 0; i < ClusterCount; i++ )
			{
				var c = (Bullet)this.ClusterBulletPrototype.Clone();
				Game.Objects.Add( c );
				c.X = X;
				c.Y = Y;
				c.GeneralDirection = GeneralDirection;

				if( Vexpl != 0 )
				{ 
					c.AutoUpdateGeneralDirection = false;
					c.Behaviors.Add( new RadialMovementBehavior { DirectionOverride = i * degIncrement, Vr = Vexpl } );
				}
			}

            RemoveSelf(); //No need for the container anymore
        }
	}
}
