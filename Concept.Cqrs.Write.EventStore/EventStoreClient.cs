using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Persistance;
using EventStore.ClientAPI;

namespace Concept.Cqrs.Write.EventStore
{
	public class EventStoreClient : IEventStreamRepository, IDisposable
	{
		private readonly IEventStoreConnection _connection;
		private readonly IEventConverter _eventConverter;
		private readonly int _readSize = 10;

		public EventStoreClient(IEventStoreConnection connection, IEventConverter eventConverter)
		{
			_connection = connection;
			_eventConverter = eventConverter;
		}

		public async Task<List<CommittedEvent>> GetStreamEventsAsync(string streamName)
		{
			var resolvedEvents = new List<ResolvedEvent>();
			var eventNumber = 0;
			StreamEventsSlice currentSlice;
			do
			{
				currentSlice = await _connection.ReadStreamEventsForwardAsync(streamName, eventNumber, _readSize, false);
				resolvedEvents.AddRange(currentSlice.Events);
				eventNumber += _readSize;
			} while (!currentSlice.IsEndOfStream);

			var domainEvents = resolvedEvents
				.Select(e => _eventConverter.ToEvent(e.OriginalEvent))
				.ToList();

			return domainEvents;
		}

		public async Task AppendEventsAsync(string streamName, IEnumerable<VersionedEvent> events)
		{
			events = events.ToList();
			var eventData = _eventConverter.ToEventData(events.Select(e => e.Event));
			var expectedVersion = events.Min(e => e.Version) - 1;
			await _connection.AppendToStreamAsync(streamName, expectedVersion, eventData);
		}

		public void Dispose()
		{
			_connection?.Dispose();
		}
	}
}