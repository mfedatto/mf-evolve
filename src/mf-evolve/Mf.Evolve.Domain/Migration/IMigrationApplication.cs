namespace Mf.Evolve.Domain.Migration;

/// <summary>
/// Defines the interface for executing migration operations, both synchronously and asynchronously.
/// </summary>
public interface IMigrationApplication
{
	/// <summary>
	/// Executes a migration process from a specified file path.
	/// </summary>
	/// <param name="filePath">The path to the migration file to execute.</param>
	/// <param name="cancellationToken">A token to observe for cancellation requests.</param>
	void Exec(
		string filePath,
		CancellationToken cancellationToken);

	/// <summary>
	/// Asynchronously executes a migration process from a specified file path.
	/// </summary>
	/// <param name="filePath">The path to the migration file to execute.</param>
	/// <param name="cancellationToken">A token to observe for cancellation requests.</param>
	/// <returns>A task representing the asynchronous migration execution operation.</returns>
	Task ExecAsync(
		string filePath,
		CancellationToken cancellationToken);
}
