using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.Xaml;

namespace DoB.Behaviors
{
	public class GunBehaviorReference : BehaviorReference, IGunConfigurator
	{
		public Color? BulletTintOverride { get; set; }

		public string BulletTextureOverride { get; set; }

		public override void OnFirstUpdate(GameTime gameTime, GameObjects.GameObject gameObject)
		{
			base.OnFirstUpdate(gameTime, gameObject); 

			((IGunConfigurator)referencedBehavior).BulletTintOverride = BulletTintOverride;
			((IGunConfigurator)referencedBehavior).BulletTextureOverride = BulletTextureOverride;
		}
	}
}
