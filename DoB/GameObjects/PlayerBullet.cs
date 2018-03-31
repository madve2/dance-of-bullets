using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Drawers;
using DoB.Behaviors;
using Microsoft.Xna.Framework;

namespace DoB.GameObjects
{
	public class PlayerBullet : Bullet
	{
		public PlayerBullet(double vx)
		{
			IsFriendly = true;
			Behaviors.Add(new XYMovementBehavior() { Vx = vx });
			BaseTexture = "playerbullet";
		}

		public override Microsoft.Xna.Framework.Rectangle GetDrawingRectangle()
		{
			return new Rectangle { X=(int)this.X, Y=(int)this.Y - 4, Width = 56, Height = 8};
		}
	}
}
