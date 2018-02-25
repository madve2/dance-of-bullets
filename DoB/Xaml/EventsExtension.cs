using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using DoB.Utility;

namespace DoB.Xaml
{
	public class EventsExtension : MarkupExtension
	{
		public string EventsCSV { get; set; }

		public EventsExtension(string eventsCSV)
		{
			EventsCSV = eventsCSV;
		}

		public EventsExtension()
		{

		}

		public override object ProvideValue( IServiceProvider serviceProvider )
		{
			var eventList = EventsCSV.Split( ';' );
			return EventCombinator.Combine( eventList );
		}
	}
}
