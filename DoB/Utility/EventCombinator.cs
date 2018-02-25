using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Utility
{
	public static class EventCombinator
	{
		private static int nextId = 0;

		private static Dictionary<string, EventCombination> combinators = new Dictionary<string, EventCombination>();

		public static string Combine( string[] eventList )
		{
			var eventName = "eventCombination_" + nextId++;
			combinators[eventName] = new EventCombination( eventList ) { Name = eventName };
			EventBroker.SubscribeOnce( eventName, n => combinators.Remove( n ) );
			return eventName;
		}
	}
}
