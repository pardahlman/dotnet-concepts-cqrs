using System;

namespace Concept.Cqrs.Write.MongoDb
{
  public class PersistedEvent : CommittedEvent
  {
    public Guid Id { get; set; }

    public PersistedEvent(long version, DateTime created, EventBase @event, string streamName)
      : base(version, created, @event, streamName){ }
  }
}
