using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Read.Process;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace Concept.Cqrs.Read.EventStore
{
  public class EventStoreSubscriber : IDisposable, IEventSubscriber
	{
		private readonly IEventStoreConnection _connection;
		private readonly ITypeResolver _typeResolver;
		private readonly IEventDeserializer _deserializer;
		private EventStoreSubscription _subscription;
		private readonly ConcurrentBag<Type> _subscribedEventTypes;
		private readonly ConcurrentBag<IEventProcessor> _eventProcessors;

		public EventStoreSubscriber(IEventStoreConnection openConnection, ITypeResolver typeResolver, IEventDeserializer deserializer)
		{
			_connection = openConnection;
			_typeResolver = typeResolver;
			_deserializer = deserializer;
			_subscribedEventTypes = new ConcurrentBag<Type>();
			_eventProcessors = new ConcurrentBag<IEventProcessor>();
		}

		public async Task RegisterAsync(IEventProcessor eventProcessor)
		{
			_eventProcessors.Add(eventProcessor);
			foreach (var eventType in eventProcessor.GetEventTypes())
			{
				await RegisterAsync(eventType);
			}
		}

		public Task RegisterAsync<TEvent>() where TEvent : EventBase
		{
			return RegisterAsync(typeof(TEvent));
		}

		public async Task RegisterAsync(Type eventType)
		{
			if (_subscribedEventTypes.Contains(eventType))
			{
				return;
			}
			if (_subscription == null)
			{
				//TODO: lock etc?
				_subscription = await _connection.SubscribeToAllAsync(true, OnEventDelivered, OnSubscriptionDropped, new UserCredentials("admin", "changeit"));
			}
			_subscribedEventTypes.Add(eventType);
		}

		private void OnSubscriptionDropped(EventStoreSubscription subscription, SubscriptionDropReason reason, Exception exception) { }

		private async Task OnEventDelivered(EventStoreSubscription subscription, ResolvedEvent resolvedEvent)
		{
			if (!_typeResolver.TryGetType(resolvedEvent.Event.EventType, out var eventType))
			{
				return;
			}
			if (!_subscribedEventTypes.Contains(eventType))
			{
				return;
			}
			var @event = _deserializer.Deserialize(resolvedEvent, eventType);
			foreach (var eventProcessor in _eventProcessors)
			{
				await eventProcessor.ProcessAsync(@event);
			}
		}

		public void Dispose()
		{
			_connection?.Dispose();
			_subscription?.Dispose();
		}
	}
}
