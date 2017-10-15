using System;
using Concept.Cqrs;

namespace Example.TodoApp.Domain.V1.Events
{
	public class TodoCreated : EventBase
	{
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public string Topic { get; set; }
		public DateTime DueDate { get; set; }
	}
}
