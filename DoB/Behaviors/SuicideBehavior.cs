using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoB.Utility;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using DoB.Xaml;
using DoB.Extensions;

namespace DoB.Behaviors
{
	public class SuicideBehavior : Behavior
	{
		public string OnEvent { get; set; }

		private Cooldown timeoutCooldown = null;
		public double? TimeoutMs { get; set; }

		public override void ResetTimers()
		{
			base.ResetTimers();
			timeoutCooldown = null;
		}

		private bool readyToCommit;

		public override void OnFirstUpdate( GameTime gameTime, GameObject gameObject )
		{
			base.OnFirstUpdate( gameTime, gameObject );
			if( !string.IsNullOrEmpty( OnEvent ) )
				EventBroker.SubscribeOnce( OnEvent, Commit );
			if( TimeoutMs.HasValue )
				timeoutCooldown = new Cooldown( TimeoutMs.Value );
		}

		public override void UpdateOverride( GameTime gameTime, GameObject gameObject )
		{
			base.UpdateOverride( gameTime, gameObject );
			if( TimeoutMs != null )
				timeoutCooldown.Update( gameTime.ElapsedMs() );
			if( readyToCommit || ( timeoutCooldown != null && timeoutCooldown.IsElapsed ) )
				gameObject.RemoveSelf();
		}

		public void Commit( string eventName )
		{
			readyToCommit = true;
		}
	}
}
