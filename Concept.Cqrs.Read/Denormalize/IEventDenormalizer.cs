using System;
using System.Threading.Tasks;

namespace Concept.Cqrs.Read.Denormalize
{
	public interface IEventDenormalizer
	{
		Type GetSupportedEventType();
		Task HandleAsync(EventBase @event);
	}
}