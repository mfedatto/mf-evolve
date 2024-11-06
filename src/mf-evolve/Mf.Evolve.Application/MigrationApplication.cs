using Mf.Evolve.Domain.Migration;
using Mf.Evolve.Domain.MigrationDefinitions;
using Microsoft.Extensions.Logging;

namespace Mf.Evolve.Application;

/// <summary>
///     Provides an application layer implementation for managing and executing migrations.
///     This class encapsulates migration logic, enabling synchronous and asynchronous execution.
/// </summary>
public class MigrationApplication : IMigrationApplication
{
	// ReSharper disable once NotAccessedField.Local
	private readonly ILogger<MigrationApplication> _logger;
	private readonly MigrationDefinitionsFactory _migrationDefinitionsFactory;
	private readonly IMigrationDefinitionsService _migrationDefinitionsService;

	public MigrationApplication(
		ILogger<MigrationApplication> logger,
		MigrationDefinitionsFactory migrationDefinitionsFactory,
		IMigrationDefinitionsService migrationDefinitionsService)
	{
		_logger = logger;
		_migrationDefinitionsFactory = migrationDefinitionsFactory;
		_migrationDefinitionsService = migrationDefinitionsService;
	}

	/// <summary>
	///     Executes a migration process from a specified file path.
	/// </summary>
	/// <param name="filePath">The path to the migration file to execute.</param>
	/// <param name="cancellationToken">A token to observe for cancellation requests.</param>
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
	///     Asynchronously executes a migration process from a specified file path.
	/// </summary>
	/// <param name="filePath">The path to the migration file to execute.</param>
	/// <param name="cancellationToken">A token to observe for cancellation requests.</param>
	/// <returns>A task representing the asynchronous migration execution operation.</returns>
	public async Task ExecAsync(
		string filePath,
		CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		IMigrationDefinitions[] migrationDefinitionsList =
			await _migrationDefinitionsService.GetDefinitionsAsync(
				filePath,
				cancellationToken);
		return;
		IMigrationDefinitions[] migrationDefinitionsFlattenedList =
			_migrationDefinitionsFactory.CreateFlattenedList(
				migrationDefinitionsList);

		return;
	}
}
