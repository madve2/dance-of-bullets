using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Utility;
using Microsoft.Xna.Framework;

namespace DoB.Components
{
	public abstract class ComponentBase : IComponent
	{
		Cooldown delay = new Cooldown();

		private double _DelayMs;
		public double DelayMs
		{
			get { return _DelayMs; }
			set
			{
				_DelayMs = value;
				delay.Reset( value );
			}
		}

		private bool isWaiting = false;
		private string _WaitForEvent;
		public string WaitForEvent
		{
			get { return _WaitForEvent; }
			set
			{
				_WaitForEvent = value;
				isWaiting = true;
				EventBroker.SubscribeOnce( value, s => isWaiting = false );
			}
		}

		private bool wasWaiting = false;
		public void Update( GameTime gameTime )
		{
			if( isWaiting )
			{
				wasWaiting = true;
				return;
			}

			if( wasWaiting )
			{
				wasWaiting = false;
				delay.Reset( DelayMs );
			}

			if( !delay.IsElapsed )
			{
				delay.Update( gameTime.ElapsedGameTime.TotalMilliseconds );
				if( !delay.IsElapsed )
					return;
			}

			UpdateOverride( gameTime );
		}

		protected abstract void UpdateOverride( GameTime gameTime );
	}
}
