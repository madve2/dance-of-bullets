using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.GameObjects;
using DoB.Behaviors;
using DoB.Drawers;
using Microsoft.Xna.Framework;

namespace DoB.Behaviors
{
	public class DieOffScreenBehavior : Behavior
	{
		public Rectangle Screen
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

		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public DieOffScreenBehavior()
		{
			if (GameObject.Game != null)
				Screen = GameObject.Game.GameplayRectangle;
		}

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			base.UpdateOverride(gameTime, gameObject);
			if (!Screen.Intersects(gameObject.GetRectangle()))
			{
				gameObject.RemoveSelf();
			}
		}
	}
}
