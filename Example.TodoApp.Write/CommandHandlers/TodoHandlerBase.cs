using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write;
using Concept.Cqrs.Write.Persistance;

namespace Example.TodoApp.Write.CommandHandlers
{
  public abstract class TodoHandlerBase<TCommand>
    : CommandHandlerBase<Todo, TCommand> where TCommand : CommandBase
  {
    protected readonly IAggregateReadRepository AggregateRepository;

    protected TodoHandlerBase(IAggregateReadRepository repo)
    {
      AggregateRepository = repo;
    }

    protected override Task<Todo> GetAggregateAsync(TCommand command)
    {
      return AggregateRepository.GetAsync<Todo>(command.AggregateId);
    }
  }
}
