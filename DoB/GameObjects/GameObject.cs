using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DoB.Drawers;
using DoB.Behaviors;
using System.Windows.Markup;
using System.Diagnostics;
using DoB.Xaml;

namespace DoB.GameObjects
{
	[ContentProperty("Behaviors")]
	public class GameObject : IPrototype
	{
		public virtual void OnIsEnabledChanged()
		{ }


		public double X { get; set; }
		public double Y { get; set; }
		public double R { get; set; }

		private TextureDrawer baseTextureDrawer;
		public string BaseTexture
		{
			get
			{
				return baseTextureDrawer.Texture;
			}
			set
			{
				baseTextureDrawer.Texture = value;
			}
		}

		public Color Tint { get; set; }
		public bool AutoUpdateGeneralDirection { get; set; }

		//Origin (used by some behaviors)
		public double Ox { get; private set; }
		public double Oy { get; private set; }

		public double GeneralDirection { get; set; }

		public double MoveX = 0;
		public double MoveY = 0;

		public List<Drawer> Drawers { get; set; }
		public List<Behavior> Behaviors { get; set; }

		public static DoBGame Game;

		private bool isFirstUpdate = true;
		public bool IsMarkedForRemoval = false;

		public GameObject()
		{
			AutoUpdateGeneralDirection = true;
			Tint = Color.White;
			Drawers = new List<Drawer>();
			baseTextureDrawer = new TextureDrawer(); //Texture will (should) be set by setting BaseTexture
			Drawers.Add(baseTextureDrawer);
			Behaviors = new List<Behavior>();
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			for (int i = 0; i < Drawers.Count; i++)
			{
				Drawers[i].Draw(gameTime, spriteBatch, this);
			}
		}

		public virtual void OnFirstUpdate(GameTime gameTime)
		{
			Ox = X;
			Oy = Y;
		}

		public virtual void Update(GameTime gameTime)
		{
			if (isFirstUpdate)
			{
				OnFirstUpdate(gameTime);
				isFirstUpdate = false;
			}

			MoveX = 0;
			MoveY = 0;

			for (int i = 0; i < Behaviors.Count; i++)
			{
				Behaviors[i].Update(gameTime, this);
			}

			X += MoveX;
			Y += MoveY;

			if( AutoUpdateGeneralDirection )
			{
				var corr = X - Ox >= 0 ? 0 : Math.PI;
				var newDir = corr + Math.Atan( ( Y - Oy ) / ( X - Ox ) );
				GeneralDirection = newDir;
			}
		}

		public Rectangle GetRectangle(bool applyPendingMovement = false)
		{
			return applyPendingMovement ?
				new Rectangle((int)(X + MoveX - R), (int)(Y + MoveY - R), (int)(2 * R), (int)(2 * R)) :
				new Rectangle((int)(X - R), (int)(Y - R), (int)(2 * R), (int)(2 * R));
		}

		public virtual Rectangle GetDrawingRectangle()
		{
			return GetRectangle();
		}

		public virtual void RemoveSelf()
		{
			IsMarkedForRemoval = true;
			for( int i = 0; i < Behaviors.Count; i++ )
			{
				Behaviors[i].OnRemoval( this );
			}
		}

		public virtual IPrototype Clone()
		{
			var c = (GameObject)this.MemberwiseClone();
			c.Behaviors = new List<Behavior>();
			for (int i = 0; i < Behaviors.Count; i++)
			{
				c.Behaviors.Add((Behavior)Behaviors[i].Clone());
			}
			c.Drawers = new List<Drawer>();
			for (int i = 0; i < Drawers.Count; i++)
			{
				c.Drawers.Add((Drawer)Drawers[i].Clone());
			}

			c.baseTextureDrawer = (TextureDrawer)c.Drawers[0];
			c.isFirstUpdate = true;
			c.IsMarkedForRemoval = false;

			return c;
		}
	}
}
