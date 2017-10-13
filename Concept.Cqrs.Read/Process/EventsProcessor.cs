using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Denormalize;

namespace Concept.Cqrs.Read.Process
{
	public class EventsProcessor : IEventProcessor
	{
		private readonly Dictionary<Type, List<IEventHandler>> _typeToHandlers;

		public EventsProcessor(IEnumerable<IEventHandler> handlers)
		{
			_typeToHandlers = handlers
				.GroupBy(h => h.GetSupportedEventType())
				.ToDictionary(
					h => h.Key,
					h => h.ToList()
				);
		}

		public async Task ProcessAsync(EventBase @event)
		{
			var eventType = @event.GetType();
			if (!_typeToHandlers.ContainsKey(eventType))
			{
				return;
			}
			foreach (var handler in _typeToHandlers[eventType])
			{
				await handler.HandleAsync(@event);
			}
		}

		public List<Type> GetEventTypes()
		{
			return _typeToHandlers.Keys.ToList();
		}
	}
}