using System;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Persistance;

namespace Concept.Cqrs.Read.Denormalize
{
	public abstract class DenormalizerBase<TReadModel, TEvent> : IEventDenormalizer
    where TEvent : EventBase where TReadModel : ReadModelBase
	{
		protected readonly IModelRepository<TReadModel> Repository;

		protected DenormalizerBase(IModelRepository<TReadModel> repository)
		{
			Repository = repository;
		}
		public bool IsApplicable(EventBase @event)
		{
			return @event is TEvent;
		}

		public Type GetSupportedEventType()
		{
			return typeof(TEvent);
		}

		public async Task HandleAsync(EventBase @event)
		{
			if (!(@event is TEvent typedEvent))
			{
				throw new ArgumentException(nameof(@event));
			}

			var model = await GetModelAsync(typedEvent, Repository);
			ApplyEvent(model, typedEvent);
			await UpdateModelAsync(model);
		}

		protected virtual Task UpdateModelAsync(TReadModel model)
		{
			return Repository.AddOrUpdate(model);
		}

		protected virtual Task<TReadModel> GetModelAsync(TEvent @event, IReadOnlyRepository<TReadModel> repo)
		{
			return repo.GetAsync(@event.AggregateId);
		}

		protected abstract void ApplyEvent(TReadModel model, TEvent @event);
	}
}
