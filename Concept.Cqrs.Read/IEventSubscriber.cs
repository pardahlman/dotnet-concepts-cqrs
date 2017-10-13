using System.Threading.Tasks;
using Concept.Cqrs.Read.Process;

namespace Concept.Cqrs.Read
{
  public interface IEventSubscriber
  {
    Task RegisterAsync<TEvent>() where TEvent : EventBase;
    Task RegisterAsync(IEventProcessor eventProcessor);
  }
}