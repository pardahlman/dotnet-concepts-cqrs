using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concept.Cqrs.Write.Persistance
{
	public interface IEventStreamRepository
	{
		Task<List<CommittedEvent>> GetStreamEventsAsync(string streamName);
		Task AppendEventsAsync(string streamName, IEnumerable<VersionedEvent> events);
	}
}
