namespace Concept.Cqrs.Write.Aggregate
{
	public interface IEventApplier<in TEvent> where TEvent : EventBase
	{
		void Apply(TEvent @event);
	}
}
