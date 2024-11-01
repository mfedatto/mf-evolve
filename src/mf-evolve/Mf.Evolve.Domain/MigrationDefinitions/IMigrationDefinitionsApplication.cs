namespace Mf.Evolve.Domain.MigrationDefinitions;

public interface IMigrationDefinitionsApplication
{
	IMigrationDefinitions[] GetDefinitions(
		string filePath);
}
