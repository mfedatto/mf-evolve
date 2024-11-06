using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IMigrationDefinitionsService
{
	IMigrationDefinitions[] GetDefinitions(
		string filePath,
		CancellationToken cancellationToken);

	Task<IMigrationDefinitions[]> GetDefinitionsAsync(
		string filePath,
		CancellationToken cancellationToken);
}
