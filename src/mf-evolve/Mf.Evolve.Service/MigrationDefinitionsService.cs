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
}
