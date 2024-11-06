using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class ExpectedItemKeyNotFoundException : Exception
{
	public ExpectedItemKeyNotFoundException()
		: base("Expected item key not found.")
	{
	}

	public ExpectedItemKeyNotFoundException(string message)
		: base(message)
	{
	}
}
