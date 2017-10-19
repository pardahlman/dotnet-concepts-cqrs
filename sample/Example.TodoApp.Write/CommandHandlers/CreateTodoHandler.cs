using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write.Persistance;
using Example.TodoApp.Domain.V1.Commands;
using Example.TodoApp.Domain.V1.Events;

namespace Example.TodoApp.Write.CommandHandlers
{
  public class CreateTodoHandler : TodoHandlerBase<CreateTodo>
  {
    public CreateTodoHandler(IAggregateReadRepository repo)
      : base(repo) { }

    protected override IEnumerable<EventBase> CreateEvents(Todo aggregate, CreateTodo command)
    {
      yield return new TodoCreated
      {
        AggregateId = aggregate.Id,
        CreatedAt = command.CreatedAt,
        DueDate = command.DueDate,
        Topic = command.Topic,
        CreatedBy = command.CreatedBy
      };
    }

    protected override Task<Todo> GetAggregateAsync(CreateTodo command)
    {
      return Task.FromResult(new Todo());
    }

    protected override async Task<bool> ValidateCommandAsync(Todo aggregate, CreateTodo command)
    {
      var existing = await AggregateRepository.GetAsync<Todo>(command.AggregateId);
      return existing == null;
    }
  }
}
