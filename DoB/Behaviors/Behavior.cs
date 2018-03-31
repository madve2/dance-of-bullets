using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using DoB.Xaml;
using DoB.Utility;
using DoB.Extensions;

namespace DoB.Behaviors
{
	public abstract class Behavior : IPrototype
	{
		public double DelayMs { get; set; }
		public double LengthMs { get; set; }
		public string WaitForEvent { get; set; }

		bool isWaitingForEvent = false;
		bool isFirstUpdate = true;

		Cooldown delay = null;
		Cooldown length = null;

		public Behavior()
		{
			LengthMs = double.PositiveInfinity;
		}

		public virtual bool IsElapsed
		{
			get
			{
				return length != null && length.IsElapsed;
			}
		}

		public virtual IPrototype Clone()
		{
			var c = (Behavior)this.MemberwiseClone();
			c.isFirstUpdate = true;
			c.delay = null;
			c.length = null;

			return c;
		}

		public virtual void ResetTimers()
		{
			delay = null;
			length = null;
			isFirstUpdate = true;
		}

		public void Update( GameTime gameTime, GameObject gameObject )
		{
			if (!isWaitingForEvent && WaitForEvent != null)
			{
				isWaitingForEvent = true;
				EventBroker.SubscribeOnce(WaitForEvent, s => { isWaitingForEvent = false; WaitForEvent = null; });
			}

			if (isWaitingForEvent)
				return;

			if( delay == null )
				delay = new Cooldown( DelayMs );

			if( length == null )
				length = new Cooldown( DelayMs + LengthMs );

			length.Update( gameTime.ElapsedMs() );
			if( length.IsElapsed )
				return;

			if( !delay.IsElapsed )
			{
				delay.Update( gameTime.ElapsedMs() );
				if( !delay.IsElapsed )
					return;
			}

			if( isFirstUpdate )
			{
				OnFirstUpdate( gameTime, gameObject );
				isFirstUpdate = false;
			}

			UpdateOverride( gameTime, gameObject );
		}

		public virtual void OnFirstUpdate( GameTime gameTime, GameObject gameObject )
		{
		}

		public virtual void UpdateOverride( GameTime gameTime, GameObject gameObject )
		{
		}

		public virtual void OnRemoval( GameObject gameObject )
		{
		}
	}
}
