using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Concept.Cqrs.Read.Persistance
{
	public class InMemoryRepository<TModel> : IModelRepository<TModel> where TModel : ReadModelBase
	{
		private readonly ConcurrentDictionary<Guid, TModel> _models;

		public InMemoryRepository()
		{
			_models = new ConcurrentDictionary<Guid, TModel>();
		}

		public Task<TModel> GetAsync(Guid aggregateId)
		{
			return _models.TryGetValue(aggregateId, out var model)
					? Task.FromResult(model)
					: Task.FromResult <TModel>(null);
		}

		public Task AddOrUpdate(TModel model)
		{
			_models.AddOrUpdate(model.AggregateId,
					aggId => model,
					(aggId, existing) => model
			);
			return Task.CompletedTask;
		}
	}
}
