using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
///     Represents the database management system.
/// </summary>
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
