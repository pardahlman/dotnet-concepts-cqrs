using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Events
{
	public class TodoAssigned : EventBase
	{
		public string Assigne { get; set; }
	}
}
