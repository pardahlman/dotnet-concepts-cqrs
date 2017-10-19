using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write.Persistance;
using Example.TodoApp.Domain.V1.Commands;
using Example.TodoApp.Domain.V1.Events;

namespace Example.TodoApp.Write.CommandHandlers
{
  public class ChangeStateHandler : TodoHandlerBase<ChangeTodoState>
  {

    public ChangeStateHandler(IAggregateReadRepository repo)
      : base(repo) { }

    protected override IEnumerable<EventBase> CreateEvents(Todo aggregate, ChangeTodoState command)
    {
      yield return new TodoStateChanged
      {
        AggregateId = aggregate.Id,
        Previous = aggregate.State,
        Current = command.State
      };
    }
    
    protected override Task<bool> ValidateCommandAsync(Todo aggregate, ChangeTodoState command)
    {
      return Task.FromResult(true); //TODO: User defined rules
    }
  }
}
