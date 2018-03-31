using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using DoB.Utility;
using DoB.Xaml;
using DoB.Extensions;

namespace DoB.Behaviors
{
	public class FireEventBehavior : Behavior
	{
		public string EventName { get; set; }
		public double CooldownMs { get; set; }
		public override bool IsElapsed => fireCooldown?.IsElapsed ?? false;

		protected Cooldown fireCooldown = null;

		public override void ResetTimers()
		{
			base.ResetTimers();
			fireCooldown = null;
		}

		public override IPrototype Clone()
		{
			var c = (FireEventBehavior)base.Clone();
			c.fireCooldown = null;
			return c;
		}

		public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
		{
			base.OnFirstUpdate(gameTime, gameObject);
			fireCooldown = new Cooldown(CooldownMs);
		}

		public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
		{
			fireCooldown.Update(gameTime.ElapsedMs());

			if (fireCooldown.IsElapsed)
			{
				EventBroker.FireEvent(EventName);
			}
		}
	}
}
