using System;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;

namespace Concept.Cqrs.Write.Persistance
{
  public class AggregateRepository : IAggregateRepository
  {
    private readonly IEventStreamRepository _streamRepo;
    private readonly IStreamNameResolver _streamNameResolver;
    private readonly IAggregatorActivator _activator;

    public AggregateRepository(IEventStreamRepository streamRepo, IStreamNameResolver streamNameResolver, IAggregatorActivator activator)
    {
      _streamRepo = streamRepo;
      _streamNameResolver = streamNameResolver;
      _activator = activator;
    }

    public async Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase
    {
      var streamName = await _streamNameResolver.ResolveAsync(aggregate);
      await _streamRepo.AppendEventsAsync(streamName, aggregate.UncommittedEvents);
    }

    public async Task<TAggregate> GetAsync<TAggregate>(Guid aggregateId) where TAggregate : AggregateBase
    {
      var streamName = await _streamNameResolver.ResolveAsync<TAggregate>(aggregateId);
      var stream = await _streamRepo.GetStreamEventsAsync(streamName);
      if (!stream.Any())
      {
        return default(TAggregate);
      }
      return _activator.CreateInstance<TAggregate>(stream);
    }
  }
}
