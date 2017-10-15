using System;
using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Commands
{
	public class CreateTodo : CommandBase
	{
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string Topic { get; set; }
		public DateTime DueDate { get; set; }
	}
}
