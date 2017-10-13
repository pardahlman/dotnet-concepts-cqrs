using System;
using System.Collections.Concurrent;

namespace Concept.Cqrs.Read.EventStore
{
	public interface ITypeResolver
	{
		bool TryGetType(string typeStr, out Type resolved);
	}

	public class TypeResolver : ITypeResolver
	{
		private readonly ConcurrentDictionary<string, Type> _typeCache;

		public TypeResolver()
		{
			_typeCache = new ConcurrentDictionary<string, Type>();
		}

		public bool TryGetType(string typeStr, out Type resolved)
		{
			resolved = _typeCache.GetOrAdd(typeStr, typeString => Type.GetType(typeString, false));
			return resolved != null;
		}
	}
}
