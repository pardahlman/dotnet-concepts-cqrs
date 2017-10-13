using System;
using System.Collections.Generic;
using System.Linq;

namespace Concept.Cqrs.Write.Aggregate
{
	public abstract class AggregateBase
	{
		public Guid Id { get; set; }
		public long Version { get; private set; }

		private readonly List<UncommittedEvent> _uncommitedEvents;
		public IReadOnlyList<UncommittedEvent> UncommittedEvents => _uncommitedEvents.AsReadOnly();
		public IReadOnlyList<CommittedEvent> CommittedEvents { get; private set; }
		public IReadOnlyCollection<Type> SupportedEvents;

		protected AggregateBase()
		{
			_uncommitedEvents = new List<UncommittedEvent>();
			SupportedEvents = AggregateReflectionHelper.GetEventTypes(GetType());
			Version = -1;
		}

		public void Load(List<CommittedEvent> events)
		{
			if (CommittedEvents?.Any() ?? false)
			{
				throw new Exception("Aggregate already loaded.");
			}
			foreach (var committedEvent in events)
			{
				AggregateReflectionHelper.Apply(this, committedEvent.Event);
			}
			CommittedEvents = events;
			Version = events.Any() ? events.Max(e => e.Version) : -1;
		}

		public void Commit()
		{
			CommittedEvents = Enumerable
				.Concat(CommittedEvents, Enumerable.Select<UncommittedEvent, CommittedEvent>(_uncommitedEvents, e => new CommittedEvent(e.Version, e.Created, e.Event)))
				.ToList()
				.AsReadOnly();
		}

		public void ApplyEvents(IEnumerable<EventBase> events)
		{
			foreach (var @event in events)
			{
				ApplyEvent(@event);
			}
		}

		public void ApplyEvent<TEvent>(TEvent @event) where TEvent : EventBase
		{
			try
			{
				AggregateReflectionHelper.Apply(this, @event);
			}
			catch (Exception e)
			{
				//TODO: handle
			}
			Version++;
			_uncommitedEvents.Add(new UncommittedEvent(Version, @event));
		}
	}
}
