using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DoB.GameObjects;

namespace DoB.Drawers
{
	public class HitboxDrawer : TextureDrawer
	{
		public static bool AreVisible { get; set; }

		static HitboxDrawer()
		{
			AreVisible = false;
		}

		public HitboxDrawer()
		{
			Texture = "hitbox";
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
		{
			if (!AreVisible)
				return;

			spriteBatch.Draw(textureObj, ((Collideable)gameObject).GetCollisionRectangle(), Color.White);
		}
	}
}
