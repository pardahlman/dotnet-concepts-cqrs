using Concept.Cqrs.Read.Denormalize;
using Concept.Cqrs.Read.Persistance;
using Example.TodoApp.Domain.V1.Events;
using Example.TodoApp.Read.Models;

namespace Example.TodoApp.Read.EventHandlers
{
  public class TodoAssignedDenormalizer : DenormalizerBase<Todo, TodoAssigned>
  {
    public TodoAssignedDenormalizer(IModelRepository<Todo> repository)
      : base(repository) { }

    protected override void ApplyEvent(Todo model, TodoAssigned @event)
    {
      model.Assigne = @event.Assigne;
    }
  }
}
