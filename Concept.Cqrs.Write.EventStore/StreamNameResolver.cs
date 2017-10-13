using System;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;

namespace Concept.Cqrs.Write.EventStore
{
	public interface IStreamNameResolver
	{
		Task<string> ResolveAsync<TAggregate>(Guid id) where TAggregate : AggregateBase;
		Task<string> ResolveAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase;
	}

	public class StreamNameResolver : IStreamNameResolver
	{
		public Task<string> ResolveAsync<TAggregate>(Guid id) where TAggregate : AggregateBase
		{
			var identifier = $"{typeof(TAggregate).Name}-{id}";
			return Task.FromResult(identifier);
		}

		public Task<string> ResolveAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase
		{
		  var identifier = $"{aggregate.GetType().Name}-{aggregate.Id}";
		  return Task.FromResult(identifier);
		}
	}
}