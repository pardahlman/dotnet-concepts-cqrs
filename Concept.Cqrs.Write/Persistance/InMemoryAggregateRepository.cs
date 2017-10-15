using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;

namespace Concept.Cqrs.Write.Persistance
{
  public class InMemoryAggregateRepository : IAggregateRepository
  {
    private readonly ConcurrentDictionary<Guid, AggregateBase> _aggregates;

    public InMemoryAggregateRepository()
    {
      _aggregates = new ConcurrentDictionary<Guid, AggregateBase>();
    }

    public Task<TAggregate> GetAsync<TAggregate>(Guid aggregateId) where TAggregate : AggregateBase
    {
      if (_aggregates.TryGetValue(aggregateId, out var aggregate) && aggregate is TAggregate typed)
      {
        return Task.FromResult(typed);
      }
      return Task.FromResult(default(TAggregate));
    }

    public Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase
    {
      _aggregates.AddOrUpdate(aggregate.Id, id => aggregate, (id, agg) => agg);
      return Task.CompletedTask;
    }
  }
}
