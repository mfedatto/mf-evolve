using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum EvolveDbms
{
	// ReSharper disable once IdentifierTypo
	PostgreSQL,
	SQLite,
	SQLServer,
	MySQL,
	MariaDB,
	Cassandra,
	CockroachDB
}
