using Mf.Evolve.Domain.MigrationDefinitions;

namespace Mf.Evolve.Application;

public class MigrationDefinitionsApplication : IMigrationDefinitionsApplication
{
	private readonly MigrationDefinitionsFactory _factory;

	public MigrationDefinitionsApplication(
		MigrationDefinitionsFactory factory)
	{
		_factory = factory;
	}
	
	public IMigrationDefinitions[] GetDefinitions(
			string filePath)
	{
		throw new NotImplementedException();
	}
}
