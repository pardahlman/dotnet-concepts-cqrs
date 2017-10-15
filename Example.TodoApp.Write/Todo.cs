using System;
using Concept.Cqrs.Write.Aggregate;
using Example.TodoApp.Domain.V1.Events;

namespace Example.TodoApp.Write
{
  public class Todo : AggregateBase, IEventApplier<TodoCreated>, IEventApplier<TodoStateChanged>
  {
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }

    public DateTime DueDate { get; set; }
    public string Topic { get; set; }
    public string State { get; set; }
    public string Assigne { get; set; }

    public void Apply(TodoCreated @event)
    {
      CreatedAt = @event.CreatedAt;
      CreatedBy = @event.CreatedBy;
      DueDate = @event.DueDate;
      Topic = @event.Topic;
    }

    public void Apply(TodoStateChanged @event)
    {
      throw new NotImplementedException();
    }
  }
}
