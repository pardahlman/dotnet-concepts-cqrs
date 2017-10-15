using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Write;
using Concept.Service.RabbitMq;
using Concept.Service.RabbitMq.Bus;
using Example.TodoApp.Domain.V1.Commands;
using RawRabbit;

namespace Example.TodoApp.Write
{
  public class TodoWriteService : RabbitMqService
  {
    private readonly ICommandProcesser _processer;

    public TodoWriteService(IBusClient busClient, ICommandProcesser processer) : base(busClient)
    {
      _processer = processer;
    }

    public override async Task StartAsync(CancellationToken ct = default(CancellationToken))
    {
      await SubscribeAsync<CreateTodo>(ProcessCommandAsync, ct: ct);
      await SubscribeAsync<AssignTodo>(ProcessCommandAsync, ct: ct);
      await SubscribeAsync<ChangeTodoState>(ProcessCommandAsync, ct: ct);
      await SubscribeAsync<DeleteTodo>(ProcessCommandAsync, ct: ct);
    }

    private Task ProcessCommandAsync<TCommand>(TCommand command, ConceptContext context) where TCommand:CommandBase  => _processer.ProcessAsync(command);
  }
}
