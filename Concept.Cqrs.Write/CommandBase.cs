using System;

namespace Concept.Cqrs.Write
{
	public abstract class CommandBase
	{
		public Guid AggregateId { get; set; }
	}
}
