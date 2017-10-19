using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Logging;

namespace Concept.Cqrs.Write.RabbitMq
{
  public class RabbitMqEventDispatcher : IEventDispatcher
  {
    private readonly IBusClient _busClient;

    public RabbitMqEventDispatcher(IBusClient busClient)
    {
      _busClient = busClient;
    }

    public async Task DispatchAsync(IEnumerable<UncommittedEvent> events, CancellationToken ct = default(CancellationToken))
    {
      foreach (var @event in events)
      {
        await _busClient.PublishAsync(@event.Event, token: ct);
      }
    }
  }
}
