using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Persistance;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Concept.Cqrs.Write.MongoDb
{
  public class MongoDbEventStreamRepository : IEventStreamRepository
  {
    private readonly IMongoCollection<PersistedEvent> _collection;
    private const string EventStreamDbName = "event_stream";

    public MongoDbEventStreamRepository(IMongoClient mongoClient)
    {
      _collection = mongoClient.GetDatabase(EventStreamDbName).GetCollection<PersistedEvent>(nameof(CommittedEvent));
      _collection.Indexes.CreateOne(Builders<PersistedEvent>.IndexKeys.Combine(
          Builders<PersistedEvent>.IndexKeys.Text(e => e.StreamName),
          Builders<PersistedEvent>.IndexKeys.Ascending(e => e.Version)),
          new CreateIndexOptions {Unique = true});
    }

    public Task<List<CommittedEvent>> GetStreamEventsAsync(string streamName)
    {
      return _collection
        .Find(s => s.StreamName == streamName)
        .Project(p => p as CommittedEvent)
        .ToListAsync();
    }

    public Task AppendEventsAsync(string streamName, IEnumerable<VersionedEvent> events)
    {
      var toPersist = new List<PersistedEvent>();
      foreach (var @event in events)
      {
        if (!BsonClassMap.IsClassMapRegistered(@event.Event.GetType()))
        {
          BsonClassMap.RegisterClassMap(new BsonClassMap(@event.Event.GetType()));
        }
        var committedEvent = new PersistedEvent(@event.Version, @event.Created, @event.Event, streamName);
        toPersist.Add(committedEvent);
      }
      return _collection
        .InsertManyAsync(toPersist, new InsertManyOptions {IsOrdered = true});
    }
  }
}
