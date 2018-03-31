using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using DoB.Xaml;

namespace DoB.Drawers
{
	public class TextureDrawer : Drawer
	{
		protected static readonly string[] rotatableTextures = new[] { "arrow" };

		protected bool rotate = false;

		public string Texture
		{
			get { return _Texture; }
			set
			{
				if (_Texture != value)
				{
					_Texture = value;
					rotate = rotatableTextures.Contains( value );
					if (GameObject.Game != null)
						textureObj = GameObject.Game.Content.Load<Texture2D>(_Texture);
				}
			}
		}

		protected Texture2D textureObj;

		private string _Texture = "";

		public override IPrototype Clone()
		{
			var c = (TextureDrawer)base.Clone();
			if (!string.IsNullOrEmpty(Texture))
				c.textureObj = GameObject.Game.Content.Load<Texture2D>(Texture);
			return c;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
		{
			if (textureObj == null)
				return;

			if( !rotate )
			{
				spriteBatch.Draw( textureObj, gameObject.GetDrawingRectangle(), gameObject.Tint );
				return;
			}

			var rect = gameObject.GetDrawingRectangle();
			
			var origin = new Vector2();
			origin.X = textureObj.Width / 2f;
			origin.Y = textureObj.Height / 2f;
			
			var scale = new Vector2();
			scale.X = rect.Width / (float)textureObj.Width;
			scale.Y = rect.Height / (float)textureObj.Height;
			
			var pos = new Vector2();
			pos.X = rect.X + rect.Width / 2f;
			pos.Y = rect.Y + rect.Height / 2f;

			spriteBatch.Draw( textureObj, pos, null, gameObject.Tint, (float)gameObject.GeneralDirection,
			origin, scale, SpriteEffects.None, 0f );
		}
	}
}
