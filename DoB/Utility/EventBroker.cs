using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoB.Utility
{
	public static class EventBroker
	{
		public static bool IsEnabled { get; set; }

		static EventBroker()
		{
			IsEnabled = true;
		}

		public static void SubscribeOnce( string eventName, Action<string> handler )
		{
			if( !subscriptions.ContainsKey( eventName ) )
			{
				subscriptions[eventName] = new List<Action<string>>();
			}

			subscriptions[eventName].Add( handler );
		}

		public static void FireEvent( string eventName )
		{
			if( !IsEnabled )
				return;

			if( subscriptions.ContainsKey( eventName ) )
			{
				for( int i = 0; i < subscriptions[eventName].Count; i++ )
				{
					subscriptions[eventName][i](eventName);
				}
				subscriptions.Remove( eventName );
			}
		}

		private static Dictionary<string, List<Action<string>>> subscriptions = new Dictionary<string, List<Action<string>>>();
	}
}
