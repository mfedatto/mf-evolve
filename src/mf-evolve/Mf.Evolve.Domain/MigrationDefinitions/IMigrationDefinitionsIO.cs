using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

// ReSharper disable once InconsistentNaming
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IMigrationDefinitionsIO
{
	/// <summary>
	///     Retrieves the raw content of a file as a string.
	/// </summary>
	string GetRawContent(
		string filePath,
		CancellationToken cancellationToken);

	
	/// <summary>
	///     Asynchronously retrieves the raw content of a file as a string.
	/// </summary>
	Task<string> GetRawContentAsync(
		string filePath,
		CancellationToken cancellationToken);
}
