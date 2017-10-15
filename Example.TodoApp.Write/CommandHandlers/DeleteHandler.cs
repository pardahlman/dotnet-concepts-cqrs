using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write.Persistance;
using Example.TodoApp.Domain.V1.Commands;
using Example.TodoApp.Domain.V1.Events;

namespace Example.TodoApp.Write.CommandHandlers
{
  public class DeleteHandler : TodoHandlerBase<DeleteTodo>
  {
    public DeleteHandler(IAggregateReadRepository repo)
      : base(repo) { }

    protected override IEnumerable<EventBase> CreateEvents(Todo aggregate, DeleteTodo command)
    {
      yield return new TodoDeleted
      {
        AggregateId = aggregate.Id
      };
    }

    protected override Task<bool> ValidateCommandAsync(Todo aggregate, DeleteTodo command)
    {
      return Task.FromResult(true);
    }
  }
}
