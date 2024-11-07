namespace Mf.Evolve.Domain.Migration;

public interface IMigrationApplication
{
	/// <summary>
	///     Executes a migration process from a specified file path.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	void Exec(
		string filePath,
		CancellationToken cancellationToken);

	/// <summary>
	///     Asynchronously executes a migration process from a specified file
	///     path.
	/// </summary>
	Task ExecAsync(
		string filePath,
		CancellationToken cancellationToken);
}
