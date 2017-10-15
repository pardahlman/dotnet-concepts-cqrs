using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Process;
using RawRabbit;

namespace Concept.Cqrs.Read.RabbitMq
{
  public class RabbitMqEventSubscriber : IEventSubscriber
  {
    private readonly IBusClient _busClient;
    private readonly ConcurrentBag<IEventProcessor> _processors;
    private readonly ConcurrentBag<Type> _subscribedEventTypes;

    public RabbitMqEventSubscriber(IBusClient busClient)
      : this(busClient, Enumerable.Empty<IEventProcessor>()) { }

    public RabbitMqEventSubscriber(IBusClient busClient, IEnumerable<IEventProcessor> processors)
    {
      _busClient = busClient;
      _processors = new ConcurrentBag<IEventProcessor>(processors);
      _subscribedEventTypes = new ConcurrentBag<Type>();
    }

    public async Task RegisterAsync<TEvent>() where TEvent : EventBase
    {
      var eventType = typeof(TEvent);
      if (_subscribedEventTypes.Contains(eventType))
      {
        return;
      }
      await _busClient.SubscribeAsync<TEvent>(async @event =>
      {
        foreach (var processor in _processors)
        {
          await processor.ProcessAsync(@event);
        }
      });
      _subscribedEventTypes.Add(eventType);
    }
  }
}
