using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using Xunit;

namespace Concept.Cqrs.Write.MongoDb.Tests
{
  public class MongoDbEventStreamRepositoryTests
  {
    [Fact]
    public async Task Should_Throw_If_Version_Is_The_Same()
    {
      /* Setup */
      var repo = new MongoDbEventStreamRepository(new MongoClient());
      var versionedEvent = new VersionedEvent(1, new TestEvent());
      var streamName = Guid.NewGuid().ToString();

      /* Test */
      await repo.AppendEventsAsync(streamName, new[] { versionedEvent });
      var evs = await repo.GetStreamEventsAsync(streamName);
      /* Assert */
    }
  }

  class TestEvent : EventBase
  {

  }
}
