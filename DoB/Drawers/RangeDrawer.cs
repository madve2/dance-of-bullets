using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using DoB.Xaml;

namespace DoB.Drawers
{
	public class RangeDrawer : Drawer
	{
		protected Texture2D textureObj;

		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Color Color { get; set; }
		public Color BorderColor { get; set; }

		public double Rate { get; set; }

		public Rectangle Position
		{
			get { return new Rectangle(X, Y, Width, Height); }
			set
			{
				X = value.X;
				Y = value.Y;
				Width = value.Width;
				Height = value.Height;
			}
		}

		public RangeDrawer()
		{
			if (GameObject.Game != null)
				textureObj = GameObject.Game.Content.Load<Texture2D>("white");
		}

		public override IPrototype Clone()
		{
			var c = (HealthbarDrawer)base.Clone();
			c.textureObj = GameObject.Game.Content.Load<Texture2D>("white");
			return c;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, GameObject gameObject)
		{
			DrawInner(spriteBatch, Rate);
		}

		protected void DrawInner(SpriteBatch spriteBatch, double rate)
		{
			if (textureObj == null)
				return;

			var pos = Position;
			pos.Width = (int)(rate * Position.Width);

			var borderPos = Position;
			borderPos.X -= 2;
			borderPos.Width += 4;
			borderPos.Y -= 2;
			borderPos.Height += 4;

			spriteBatch.Draw(textureObj, borderPos, BorderColor);
			spriteBatch.Draw(textureObj, pos, Color);
		}
	}
}
