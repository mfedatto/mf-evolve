namespace Mf.Evolve.Domain.MigrationDefinitions;

public interface IMigrationDefinitionsService
{
	IMigrationDefinitions[] GetDefinitions(
		string filePath,
		CancellationToken cancellationToken);
	
	Task<IMigrationDefinitions[]> GetDefinitionsAsync(
		string filePath,
		CancellationToken cancellationToken);
}
