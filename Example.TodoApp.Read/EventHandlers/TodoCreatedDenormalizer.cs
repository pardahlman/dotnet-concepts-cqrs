using System.Threading.Tasks;
using Concept.Cqrs.Read.Denormalize;
using Concept.Cqrs.Read.Persistance;
using Example.TodoApp.Domain.V1.Events;
using Example.TodoApp.Read.Models;

namespace Example.TodoApp.Read.EventHandlers
{
  public class TodoCreatedDenormalizer : DenormalizerBase<Todo, TodoCreated>
  {
    public TodoCreatedDenormalizer(IModelRepository<Todo> repository)
      : base(repository) { }

    protected override void ApplyEvent(Todo model, TodoCreated @event)
    {
      model.CreatedBy= @event.CreatedBy;
      model.CreatedAt= @event.CreatedAt;
      model.Topic = @event.Topic;
      model.DueDate = @event.DueDate;
    }

    protected override Task<Todo> GetModelAsync(TodoCreated @event, IReadOnlyRepository<Todo> repo)
    {
      return Task.FromResult(new Todo());
    }
  }
}
