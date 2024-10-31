using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when the <c>AddCommand</c> method is not found.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class AddCommandMethodNotFoundException : Exception
{
	/// <summary>
	///     Initializes a new instance of the <see cref="AddCommandMethodNotFoundException" /> class.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public AddCommandMethodNotFoundException()
		: base("The method AddCommand was not found.")
	{
	}
}
