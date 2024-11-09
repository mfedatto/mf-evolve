using Mf.Evolve.Domain.Migration;
using Mf.Evolve.Domain.MigrationDefinitions;
using Microsoft.Extensions.Logging;

namespace Mf.Evolve.Application;

public class MigrationApplication : IMigrationApplication
{
	// ReSharper disable once NotAccessedField.Local
	private readonly ILogger<MigrationApplication> _logger;
	private readonly IMigrationDefinitionsService _migrationDefinitionsService;

	public MigrationApplication(
		ILogger<MigrationApplication> logger,
		IMigrationDefinitionsService migrationDefinitionsService)
	{
		_logger = logger;
		_migrationDefinitionsService = migrationDefinitionsService;
	}

	/// <summary>
	///     Executes a migration process from a specified file path.
	/// </summary>
	public void Exec(
		string filePath,
		CancellationToken cancellationToken)
	{
		ExecAsync(
				filePath,
				cancellationToken)
			.GetAwaiter()
			.GetResult();
	}

	/// <summary>
	///     Asynchronously executes a migration process from a specified file
	///     path.
	/// </summary>
	public async Task ExecAsync(
		string filePath,
		CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		IMigrationDefinitions[] migrationDefinitionsList =
			await _migrationDefinitionsService.GetDefinitionsAsync(
				filePath,
				cancellationToken);
		IMigrationDefinitions[] flattenedDefinitionsList =
			_migrationDefinitionsService.CreateFlattenedDefinitionsList(
				migrationDefinitionsList,
				cancellationToken);
	}
}
