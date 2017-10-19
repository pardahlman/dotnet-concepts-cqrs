using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concept.Cqrs.Read.Process
{
	public interface IEventProcessor
	{
		Task ProcessAsync(EventBase @event);
		List<Type> GetEventTypes();
	}
}
