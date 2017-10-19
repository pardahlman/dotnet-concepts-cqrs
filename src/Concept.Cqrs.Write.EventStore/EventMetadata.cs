using System;

namespace Concept.Cqrs.Write.EventStore
{
	public class EventMetadata
	{
		public string Server { get; set; }
		public DateTime ServerTime { get; set; }
	}
}
