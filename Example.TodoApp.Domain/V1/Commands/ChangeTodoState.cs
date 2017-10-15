using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Commands
{
	public class ChangeTodoState : CommandBase
	{
		public string State { get; set; }
	}
}
