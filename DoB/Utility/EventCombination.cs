using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Utility
{
	public class EventCombination
	{
		public List<string> Events { get; private set; }

		public string Name { get; set; }

		public EventCombination(IEnumerable<string> events)
		{
			Events = events.ToList();
			for( int i = 0; i < Events.Count; i++ )
			{
				EventBroker.SubscribeOnce( Events[i], HandleEvent );
			}
		}

		private void HandleEvent(string eventName)
		{
			Events.Remove( eventName );
			if( Events.Count == 0 )
			{
				EventBroker.FireEvent( Name );
			}
		}
	}
}
