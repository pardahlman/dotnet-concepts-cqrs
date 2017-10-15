using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Commands
{
	public class AssignTodo : CommandBase
	{
		public string Assigne { get; set; }
	}
}
