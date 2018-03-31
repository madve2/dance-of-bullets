using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DoB.GameObjects;
using DoB.Xaml;

namespace DoB.Drawers
{
	public abstract class Drawer : IPrototype
	{
		public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject);

		public virtual IPrototype Clone()
		{
			return (Drawer)this.MemberwiseClone();
		}
	}
}
