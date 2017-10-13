using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concept.Cqrs.Write
{
	public interface IEventDispatcher
	{
		Task DispatchAsync(IEnumerable<UncommittedEvent> events);
	}

	public class SilentEventDispatcher : IEventDispatcher
	{
		public Task DispatchAsync(IEnumerable<UncommittedEvent> events)
		{
			return Task.CompletedTask;
		}
	}

	public class InMemoryEventDispatcher : IEventDispatcher
	{
		private readonly Func<EventBase, Task> _dispatchFunc;

		public InMemoryEventDispatcher(Func<EventBase, Task> dispatchFunc)
		{
			_dispatchFunc = dispatchFunc;
		}

		public async Task DispatchAsync(IEnumerable<UncommittedEvent> events)
		{
			foreach (var @event in events)
			{
				await _dispatchFunc(@event.Event);
			}
		}
	}
}