namespace Mf.Evolve.Domain.MigrationDefinitions;

// ReSharper disable once InconsistentNaming
public interface IMigrationDefinitionsIO
{
	string GetRawContent(
		string filePath,
		CancellationToken cancellationToken);

	Task<string> GetRawContentAsync(
		string filePath,
		CancellationToken cancellationToken);
}
