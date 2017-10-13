using System.Threading.Tasks;
using Concept.Logging;

namespace Concept.Service
{
  public abstract class Service
  {
    private readonly ILog _logger = LogProvider.For<Service>();
    public abstract Task StartAsync();

    public virtual Task StopAsync()
    {
      _logger.Info("Stopping service.");
      return Task.CompletedTask;
    }
  }
}
