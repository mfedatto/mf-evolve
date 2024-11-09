using Mf.Evolve.Domain.MigrationDefinitions;
using Microsoft.Extensions.Logging;

namespace Mf.Evolve.Service;

public class MigrationDefinitionsService : IMigrationDefinitionsService
{
	private readonly MigrationDefinitionsFactory _factory;

	// ReSharper disable once NotAccessedField.Local
	private readonly ILogger<MigrationDefinitionsService> _logger;

	// ReSharper disable once InconsistentNaming
	private readonly IMigrationDefinitionsIO _migrationDefinitionsIO;

	public MigrationDefinitionsService(
		ILogger<MigrationDefinitionsService> logger,
		// ReSharper disable once InconsistentNaming
		IMigrationDefinitionsIO migrationDefinitionsIO,
		MigrationDefinitionsFactory factory)
	{
		_logger = logger;
		_migrationDefinitionsIO = migrationDefinitionsIO;
		_factory = factory;
	}

	/// <summary>
	///     Retrieves an array of migration definitions from a specified file
	///     path.
	/// </summary>
	public IMigrationDefinitions[] GetDefinitions(
		string filePath,
		CancellationToken cancellationToken)
	{
		IMigrationDefinitions[] result = GetDefinitionsAsync(
				filePath,
				cancellationToken)
			.GetAwaiter()
			.GetResult();

		return result;
	}

	/// <summary>
	///     Asynchronously retrieves an array of migration definitions from a
	///     specified file path.
	/// </summary>
	public async Task<IMigrationDefinitions[]> GetDefinitionsAsync(
		string filePath,
		CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		string rawContent = await _migrationDefinitionsIO.GetRawContentAsync(
			filePath,
			cancellationToken);
		IMigrationDefinitions[]? result = _factory.Create(rawContent);

		return result
		       ?? [];
	}

	/// <summary>
	///     Creates a flattened list of migration definitions from a provided
	///     array of definitions.
	/// </summary>
	public IMigrationDefinitions[] CreateFlattenedDefinitionsList(
		IMigrationDefinitions[] definitionsList,
		CancellationToken cancellationToken)
	{
		return _factory.CreateFlattenedList(definitionsList);
	}
}
