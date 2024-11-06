using Mf.Evolve.Domain.Common;

namespace Mf.Evolve.Domain.ConnectionStringTemplate;

public interface IConnectionStringTemplate : IPlaceholder
{
	// ReSharper disable once UnusedMember.Global
	string? ConnectionString { get; }
}
