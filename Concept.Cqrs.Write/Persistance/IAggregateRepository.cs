using System;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;

namespace Concept.Cqrs.Write.Persistance
{
	public interface IAggregateRepository : IAggregateReadRepository, IAggregateWriteRepository { }

  public interface IAggregateReadRepository
	{
		Task<TAggregate> GetAsync<TAggregate>(Guid aggregateId) where TAggregate : AggregateBase;
	}

	public interface IAggregateWriteRepository
	{
		Task AddOrUpdateAsync<TAggregate>(TAggregate aggregate) where TAggregate : AggregateBase;
	}
}
