using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;

namespace DoB.Behaviors
{
	public  class FramedMovementBehavior : Behavior
	{
		public Rectangle Screen { get; set; }

		public FramedMovementBehavior()
		{
			if (GameObject.Game != null)
				Screen = GameObject.Game.GameplayRectangle;
		}

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			base.UpdateOverride(gameTime, gameObject);
			var rect = gameObject.GetRectangle(true);
			if (rect.X < Screen.X)
				gameObject.MoveX = Screen.X + gameObject.R - gameObject.X;
			if ((rect.X + rect.Width) > (Screen.X + Screen.Width))
				gameObject.MoveX = Screen.X + Screen.Width - gameObject.R - gameObject.X;
			if (rect.Y < Screen.Y)
				gameObject.MoveY = Screen.Y + gameObject.R - gameObject.Y;
			if ((rect.Y + rect.Height) > (Screen.Y + Screen.Height))
				gameObject.MoveY = Screen.Y + Screen.Height - gameObject.R - gameObject.Y;
		}
	}
}
