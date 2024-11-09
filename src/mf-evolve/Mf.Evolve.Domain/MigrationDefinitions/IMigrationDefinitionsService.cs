using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IMigrationDefinitionsService
{
	/// <summary>
	///     Retrieves an array of migration definitions from a specified file
	///     path.
	/// </summary>
	IMigrationDefinitions[] GetDefinitions(
		string filePath,
		CancellationToken cancellationToken);

	/// <summary>
	///     Asynchronously retrieves an array of migration definitions from a
	///     specified file path.
	/// </summary>
	Task<IMigrationDefinitions[]> GetDefinitionsAsync(
		string filePath,
		CancellationToken cancellationToken);

	/// <summary>
	///     Creates a flattened list of migration definitions from a provided
	///     array of definitions.
	/// </summary>
	IMigrationDefinitions[] CreateFlattenedDefinitionsList(
		IMigrationDefinitions[] definitionsList,
		CancellationToken cancellationToken);
}
