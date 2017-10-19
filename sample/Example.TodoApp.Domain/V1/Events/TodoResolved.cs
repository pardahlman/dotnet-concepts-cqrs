using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Events
{
	public class TodoResolved : EventBase
	{
		public string Comment { get; set; }
	}
}
