using System;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;
using Concept.Cqrs.Write.Persistance;

namespace Concept.Cqrs.Write.EventStore
{
	public class EventStoreAggregateRepository : IAggregateRepository
	{
		private readonly EventStoreClient _client;
		private readonly StreamNameResolver _streamNameResolver;
		private readonly AggregatorActivator _activator;

		public EventStoreAggregateRepository(EventStoreClient client, StreamNameResolver streamNameResolver, AggregatorActivator activator)
		{
			_client = client;
			_streamNameResolver = streamNameResolver;
			_activator = activator;
		}

		public async Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase
		{
			var streamName = await _streamNameResolver.ResolveAsync(aggregate);
			await _client.AppendEventsAsync(streamName, aggregate.UncommittedEvents);
		}

		public async Task<TAggregate> GetAsync<TAggregate>(Guid aggregateId) where TAggregate : AggregateBase
		{
			var streamName = await _streamNameResolver.ResolveAsync<TAggregate>(aggregateId);
			var stream = await _client.GetStreamEventsAsync(streamName);
			if (!stream.Any())
			{
				return default(TAggregate);
			}
			return _activator.CreateInstance<TAggregate>(stream);
		}
	}
}
