using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write.Persistance;
using Example.TodoApp.Domain.V1.Commands;
using Example.TodoApp.Domain.V1.Events;

namespace Example.TodoApp.Write.CommandHandlers
{
  public class AssignHandler : TodoHandlerBase<AssignTodo>
  {
    public AssignHandler(IAggregateReadRepository repo)
      : base(repo) { }

    protected override IEnumerable<EventBase> CreateEvents(Todo aggregate, AssignTodo command)
    {
      yield return new TodoAssigned
      {
        AggregateId = aggregate.Id,
        Assigne = command.Assigne
      };
    }

    protected override Task<bool> ValidateCommandAsync(Todo aggregate, AssignTodo command)
    {
      if (string.Equals(aggregate.Assigne, command.Assigne))
      {
        return Task.FromResult(false);
      }
      return Task.FromResult(true);
    }
  }
}
