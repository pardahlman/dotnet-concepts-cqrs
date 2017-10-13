using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using RecordedEvent = EventStore.ClientAPI.RecordedEvent;

namespace Concept.Cqrs.Write.EventStore
{
	public interface IEventConverter
	{
		EventData ToEventData<TEvent>(TEvent @event) where TEvent : EventBase;
		List<EventData> ToEventData<TEvent>(IEnumerable<TEvent> @event) where TEvent : EventBase;
		CommittedEvent ToEvent(RecordedEvent eventData);
		List<CommittedEvent> ToEvent(IEnumerable<RecordedEvent> eventData);
	}

	public class JsonEventConverter : IEventConverter
	{
		private readonly JsonSerializer _serializer;
		private readonly Dictionary<string, Type> _nameToType;
		protected readonly bool IsJson = true;

		public JsonEventConverter(JsonSerializer serializer)
		{
			_serializer = serializer;
			_nameToType = new Dictionary<string, Type>();
		}

		public EventData ToEventData<TEvent>(TEvent @event) where TEvent : EventBase
		{
			var eventData = new EventData(
				GetEventId(@event),
				GetEventName(@event),
				IsJson,
				GetEventBody(@event),
				GetEventMetadata(@event)
			);
			return eventData;
		}

		public List<EventData> ToEventData<TEvent>(IEnumerable<TEvent> @event) where TEvent : EventBase
		{
			return @event.Select(ToEventData).ToList();
		}

		public CommittedEvent ToEvent(RecordedEvent eventData)
		{
			if (!_nameToType.ContainsKey(eventData.EventType))
			{
				_nameToType.Add(eventData.EventType, Type.GetType(eventData.EventType, true));
			}
			var type = _nameToType[eventData.EventType];
			var eventBase = Deserialize(eventData.Data, type) as EventBase;
			return new CommittedEvent(eventData.EventNumber, eventData.Created, eventBase);
		}

		public List<CommittedEvent> ToEvent(IEnumerable<RecordedEvent> eventData)
		{
			return eventData.Select(ToEvent).ToList();
		}

		protected virtual Guid GetEventId<TEvent>(TEvent @event)
		{
			return Guid.NewGuid();
		}

		protected virtual string GetEventName<TEvent>(TEvent @event)
		{
			return $"{@event.GetType().FullName}, {@event.GetType().Assembly.GetName().Name}";
		}

		protected virtual byte[] GetEventBody<TEvent>(TEvent @event)
		{
			return Serialize(@event);
		}

		protected virtual byte[] GetEventMetadata<TEvent>(TEvent @event)
		{
			return Serialize(new EventMetadata
			{
				Server = Environment.MachineName,
				ServerTime = DateTime.Now
			});
		}

		private byte[] Serialize<TEvent>(TEvent @event)
		{
			using (var strWriter = new StringWriter())
			{
				_serializer.Serialize(strWriter, @event);
				return Encoding.UTF8.GetBytes(strWriter.ToString());
			}
		}

		private object Deserialize(byte[] body, Type type)
		{
			var dataStr = Encoding.UTF8.GetString(body);
			using (var reader = new StringReader(dataStr))
			{
				return _serializer.Deserialize(reader, type);
			}
		}
	}
}
