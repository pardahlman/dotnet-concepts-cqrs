using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Concept.Cqrs.Write.Aggregate;

namespace Concept.Cqrs.Write
{
	public interface ICommandHandler
	{
		Task<AggregateBase> UpdatedAggregateAsync(CommandBase command);
		Type GetCommandType();
	}

	public abstract class CommandHandlerBase<TAggregate, TCommand> : ICommandHandler
	  where TAggregate : AggregateBase
	  where TCommand : CommandBase
	{
		protected abstract IEnumerable<EventBase> CreateEvents(TAggregate aggregate, TCommand command);

		protected abstract Task<TAggregate> GetAggregateAsync(TCommand command);

		protected abstract Task<bool> ValidateCommandAsync(TAggregate aggregate, TCommand command);

		public async Task<AggregateBase> UpdatedAggregateAsync(CommandBase command)
		{
			if (!(command is TCommand typed))
			{
				return null;
			}
			var aggregate = await GetAggregateAsync(typed);
			if (! await ValidateCommandAsync(aggregate, typed))
			{
				return null;
			}
			var events = CreateEvents(aggregate, typed);
			aggregate.ApplyEvents(events);
			return aggregate;
		}

		public Type GetCommandType()
		{
			return typeof(TCommand);
		}
	}
}
