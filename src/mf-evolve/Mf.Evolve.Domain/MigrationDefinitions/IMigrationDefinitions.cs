using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public interface IMigrationDefinitions
{
    /// <summary>
    /// Paths to scan recursively for migrations. Mandatory if <see cref="EmbeddedResourceAssemblies"/> is empty.
    /// </summary>
    string[] Locations { get; }

    /// <summary>
    /// A list of schemas managed by Evolve.
    /// If empty, the default schema for the datasource connection is used.
    /// </summary>
    string[] Schemas { get; }

    /// <summary>
    /// Command to execute as defined by <see cref="CommandTypes"/>.
    /// </summary>
    CommandTypes Command { get; }

    /// <summary>
    /// Transaction mode settings for the migration.
    /// See <see cref="TransactionModes"/> for possible values.
    /// </summary>
    TransactionModes TransactionMode { get; }

    /// <summary>
    /// When <c>true</c>, ensures that Evolve will never erase schemas.
    /// </summary>
    bool EraseDisabled { get; }

    /// <summary>
    /// When <c>true</c>, if validation phase fails, Evolve will erase the database schemas
    /// and re-execute migration scripts from scratch. Intended for development only.
    /// </summary>
    bool EraseOnValidationError { get; }

    /// <summary>
    /// Version used as the starting point for already existing databases.
    /// </summary>
    int StartVersion { get; }

    /// <summary>
    /// Target version to reach. If not set, it evolves all the way up.
    /// </summary>
    int? TargetVersion { get; }

    /// <summary>
    /// Allows migrations to be run “out of order”.
    /// </summary>
    bool OutOfOrder { get; }

    /// <summary>
    /// Mark all subsequent migrations as already applied.
    /// </summary>
    bool SkipNextMigrations { get; }

    /// <summary>
    /// Execute repeatedly all repeatable migrations for as long as the number of errors decreases.
    /// </summary>
    bool RetryRepeatableMigrationsUntilNoError { get; }

    /// <summary>
    /// Assemblies to scan to load embedded migration scripts. Mandatory if <see cref="Locations"/> is empty.
    /// </summary>
    string[] EmbeddedResourceAssemblies { get; }

    /// <summary>
    /// Includes embedded migration scripts that start with one of these filters.
    /// </summary>
    string[] EmbeddedResourceFilters { get; }

    /// <summary>
    /// The time in seconds to wait for the migration to execute before generating an error.
    /// </summary>
    int? CommandTimeout { get; }

    /// <summary>
    /// The encoding of SQL migration files.
    /// </summary>
    string Encoding { get; }

    /// <summary>
    /// Migration file name prefix.
    /// </summary>
    string SqlMigrationPrefix { get; }

    /// <summary>
    /// Repeatable migration file name prefix.
    /// </summary>
    string SqlRepeatableMigrationPrefix { get; }

    /// <summary>
    /// Migration file name separator.
    /// </summary>
    string SqlMigrationSeparator { get; }

    /// <summary>
    /// Migration file name suffix.
    /// </summary>
    string SqlMigrationSuffix { get; }

    /// <summary>
    /// The schema containing the metadata table.
    /// If not set, defaults to the first schema defined in Schemas or the one of the datasource connection.
    /// </summary>
    string? MetadataTableSchema { get; }

    /// <summary>
    /// The metadata table name.
    /// </summary>
    string? MetadataTableName { get; }

    /// <summary>
    /// The prefix of the placeholders.
    /// </summary>
    string PlaceholderPrefix { get; }

    /// <summary>
    /// The suffix of the placeholders.
    /// </summary>
    string PlaceholderSuffix { get; }

    /// <summary>
    /// Placeholders are strings to replace in SQL migrations at runtime.
    /// </summary>
    Dictionary<string, string>[] Placeholders { get; }

    /// <summary>
    /// When <c>true</c>, Evolve will use a session level lock to coordinate migrations on multiple nodes.
    /// </summary>
    bool EnableClusterMode { get; }

    /// <summary>
    /// Child migration definitions, if any.
    /// </summary>
    IMigrationDefinitions[]? Children { get; }
}
