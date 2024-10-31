using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when a null base namespace is encountered.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class NullBaseNamespaceException : Exception
{
	/// <summary>
	///     Initializes a new instance of the <see cref="NullBaseNamespaceException" /> class
	///     with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullBaseNamespaceException()
		: base("Can't handle a null base namespace.")
	{
	}
}
