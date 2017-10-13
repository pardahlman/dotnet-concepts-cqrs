using System;
using System.Threading.Tasks;

namespace Concept.Cqrs.Read.Denormalize
{
	public interface IEventHandler
	{
		Type GetSupportedEventType();
		Task HandleAsync(EventBase @event);
	}
}