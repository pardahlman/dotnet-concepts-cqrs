using System.Threading.Tasks;

namespace Concept.Cqrs.Read
{
  public interface IEventSubscriber
  {
    Task RegisterAsync<TEvent>() where TEvent : EventBase;
  }
}
