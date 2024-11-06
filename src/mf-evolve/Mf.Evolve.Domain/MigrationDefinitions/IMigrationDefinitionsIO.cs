using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

// ReSharper disable once InconsistentNaming
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IMigrationDefinitionsIO
{
	string GetRawContent(
		string filePath,
		CancellationToken cancellationToken);

	Task<string> GetRawContentAsync(
		string filePath,
		CancellationToken cancellationToken);
}
