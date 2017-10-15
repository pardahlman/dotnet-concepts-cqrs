using System.Threading;
using System.Threading.Tasks;
using Concept.Cqrs;
using Concept.Cqrs.Read.Process;
using Concept.Service.RabbitMq;
using Concept.Service.RabbitMq.Bus;
using Example.TodoApp.Domain.V1.Events;
using RawRabbit;

namespace Example.TodoApp.Read
{
  public class TodoReadService : RabbitMqService
  {
    private readonly IEventProcessor _eventProcessor;

    public TodoReadService(IBusClient busClient, IEventProcessor eventProcessor)
      : base(busClient)
    {
      _eventProcessor = eventProcessor;
    }

    public override async Task StartAsync(CancellationToken ct = default(CancellationToken))
    {
      //foreach (var eventType in _eventProcessor.GetEventTypes())
      //{
      //  await SubscribeAsync<EventBase>((@event, context) =>
      //    _eventProcessor.ProcessAsync(@event),
      //    ctx => ctx.Properties.AddOrReplace(PipeKey.MessageType, eventType),
      //    ct
      //  );
      //}
      await SubscribeAsync<TodoCreated>(ProcessEventAsync, ct: ct);
      await SubscribeAsync<TodoDeleted>(ProcessEventAsync, ct: ct);
      await SubscribeAsync<TodoAssigned>(ProcessEventAsync, ct: ct);
      await SubscribeAsync<TodoStateChanged>(ProcessEventAsync, ct: ct);
      await SubscribeAsync<TodoResolved>(ProcessEventAsync, ct: ct);
    }

    private Task ProcessEventAsync<TEvent>(TEvent @event, ConceptContext context) where TEvent : EventBase => _eventProcessor.ProcessAsync(@event);
  }
}
