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

    public long Version { get; protected set; }
    public DateTime Created { get; protected set; }
    public TEvent Event { get; protected set; }
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
    public string StreamName { get; set; }

    public CommittedEvent(long version, DateTime created, EventBase @event, string streamName)
      : base(version, @event)
    {
      Created = created;
      StreamName = streamName;
    }
  }
}
