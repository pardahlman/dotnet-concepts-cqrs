using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Events
{
	public class TodoStateChanged : EventBase
	{
		public string Current { get; set; }
		public string Previous { get; set; }
	}
}
