using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Concept.Cqrs.Read.EventStore
{
	public interface IEventDeserializer
	{
		EventBase Deserialize(ResolvedEvent @event, Type eventType);
	}

	public class JsonEventDeserializer : IEventDeserializer
	{
		private readonly JsonSerializer _serializer;

		public JsonEventDeserializer(JsonSerializer serializer)
		{
			_serializer = serializer;
		}

		public EventBase Deserialize(ResolvedEvent @event, Type eventType)
		{
			var eventBase = Deserialize(@event.Event.Data, eventType) as EventBase;
			return eventBase;
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
