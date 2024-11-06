using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class UnexpectedDeserializedObjectTypeException : Exception
{
	public UnexpectedDeserializedObjectTypeException()
		: base("Unexpected deserialized object type.")
	{
	}

	public UnexpectedDeserializedObjectTypeException(string message)
		: base(message)
	{
	}
}
