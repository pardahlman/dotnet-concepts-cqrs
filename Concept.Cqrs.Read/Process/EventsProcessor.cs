using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Denormalize;

namespace Concept.Cqrs.Read.Process
{
	public class EventsProcessor : IEventProcessor
	{
		private readonly Dictionary<Type, List<IEventDenormalizer>> _typeToDenormalizer;

		public EventsProcessor(IEnumerable<IEventDenormalizer> denormalizers)
		{
			_typeToDenormalizer = denormalizers
				.GroupBy(h => h.GetSupportedEventType())
				.ToDictionary(
					h => h.Key,
					h => h.ToList()
				);
		}

		public async Task ProcessAsync(EventBase @event)
		{
			var eventType = @event.GetType();
			if (!_typeToDenormalizer.ContainsKey(eventType))
			{
				return;
			}
			foreach (var handler in _typeToDenormalizer[eventType])
			{
				await handler.HandleAsync(@event);
			}
		}

		public List<Type> GetEventTypes()
		{
			return _typeToDenormalizer.Keys.ToList();
		}
	}
}
