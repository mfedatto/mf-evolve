using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.Common;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IPlaceholder
{
	/// <summary>
	///     The prefix of the placeholders.
	/// </summary>
	string? PlaceholderPrefix { get; }

	/// <summary>
	///     The suffix of the placeholders.
	/// </summary>
	string? PlaceholderSuffix { get; }

	/// <summary>
	///     Placeholders are strings to replace in SQL migrations at runtime.
	/// </summary>
	Dictionary<string, string>? Placeholders { get; }
}
