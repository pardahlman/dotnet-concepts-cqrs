using System;

namespace Concept.Cqrs
{
	public abstract class EventBase
	{
		public Guid AggregateId { get; set; }
	}

	public class VersionedEvent<TEvent> where TEvent : EventBase
	{
		public VersionedEvent(long version, TEvent @event)
		{
			Version = version;
			Event = @event;
		}

		public long Version { get; }
		public DateTime Created { get; protected set; }
		public TEvent Event { get; }
	}

	public class VersionedEvent : VersionedEvent<EventBase>
	{
		public VersionedEvent(long version, EventBase @event)
			: base(version, @event) { }
	}

	public class UncommittedEvent : VersionedEvent
	{
		public UncommittedEvent(long version, EventBase @event)
			: base(version, @event)
		{
			Created = DateTime.Now;
		}
	}

	public class CommittedEvent : VersionedEvent
	{
		public CommittedEvent(long version, DateTime created, EventBase @event)
			: base(version, @event)
		{
			Created = created;
		}
	}
}