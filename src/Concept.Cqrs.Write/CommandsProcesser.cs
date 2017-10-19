using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Persistance;

namespace Concept.Cqrs.Write
{
	public interface ICommandProcesser
	{
		Task ProcessAsync<TCommand>(TCommand command) where TCommand : CommandBase;
	}

	public class CommandsProcesser : ICommandProcesser
	{
		private readonly List<ICommandHandler> _commandHandlers;
		private readonly IEventDispatcher _eventDispatcher;
		private readonly IAggregateRepository _aggregateRepo;

		public CommandsProcesser(IEnumerable<ICommandHandler> commandHandlers, IEventDispatcher eventDispatcher, IAggregateRepository aggregateRepo)
		{
			_commandHandlers = commandHandlers.ToList();
			_eventDispatcher = eventDispatcher;
			_aggregateRepo = aggregateRepo;
		}

		public async Task ProcessAsync<TCommand>(TCommand command) where TCommand : CommandBase
		{
			foreach (var commandHandler in _commandHandlers.Where(e => e.GetCommandType() == command.GetType()))
			{
				var aggregate = await commandHandler.UpdatedAggregateAsync(command);
				if (aggregate != null)
				{
					var newEvents = aggregate.UncommittedEvents;
					await _aggregateRepo.AddOrUpdateAsync(aggregate);
					await _eventDispatcher.DispatchAsync(newEvents);
				}
			}
		}
	}
}
