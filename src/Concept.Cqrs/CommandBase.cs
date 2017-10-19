using System;

namespace Concept.Cqrs
{
	public abstract class CommandBase
	{
		public Guid AggregateId { get; set; }
	}
}
