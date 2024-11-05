using Mf.Evolve.Domain.Common;
using Mf.Evolve.Domain.MigrationDefinitions;

namespace Mf.Evolve.Domain.ConnectionStringTemplate;

public interface IConnectionStringTemplate : IPlaceholder
{
	string ConnectionString { get; }
}
