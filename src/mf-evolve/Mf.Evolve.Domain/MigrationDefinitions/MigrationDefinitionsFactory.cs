namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
/// Factory class for creating instances of <see cref="IMigrationDefinitions"/>.
/// </summary>
public class MigrationDefinitionsFactory
{
	/// <summary>
	/// Creates a new instance of <see cref="IMigrationDefinitions"/> with the specified parameters.
	/// </summary>
	/// <param name="locations">An array of locations where the migrations are defined.</param>
	/// <param name="schemas">An array of schemas associated with the migrations.</param>
	/// <param name="command">The command type to be executed.</param>
	/// <param name="transactionMode">The transaction mode to use during migration.</param>
	/// <param name="eraseDisabled">Indicates whether the erase operation is disabled.</param>
	/// <param name="eraseOnValidationError">Indicates whether to erase on validation error.</param>
	/// <param name="startVersion">The starting version for the migration.</param>
	/// <param name="targetVersion">The target version for the migration, if any.</param>
	/// <param name="outOfOrder">Indicates whether to allow out-of-order migrations.</param>
	/// <param name="skipNextMigrations">Indicates whether to skip the next migrations.</param>
	/// <param name="retryRepeatableMigrationsUntilNoError">Indicates whether to retry repeatable migrations until no error occurs.</param>
	/// <param name="embeddedResourceAssemblies">An array of embedded resource assemblies.</param>
	/// <param name="embeddedResourceFilters">An array of filters for embedded resources.</param>
	/// <param name="commandTimeout">Optional timeout for the command execution.</param>
	/// <param name="encoding">The encoding to be used for migration scripts.</param>
	/// <param name="sqlMigrationPrefix">The prefix to use for SQL migration files.</param>
	/// <param name="sqlRepeatableMigrationPrefix">The prefix for repeatable SQL migration files.</param>
	/// <param name="sqlMigrationSeparator">The separator used in SQL migration files.</param>
	/// <param name="sqlMigrationSuffix">The suffix to use for SQL migration files.</param>
	/// <param name="metadataTableSchema">Optional schema for the metadata table.</param>
	/// <param name="metadataTableName">Optional name for the metadata table.</param>
	/// <param name="placeholderPrefix">The prefix for placeholders in migration scripts.</param>
	/// <param name="placeholderSuffix">The suffix for placeholders in migration scripts.</param>
	/// <param name="placeholders">An array of dictionaries for placeholder values.</param>
	/// <param name="enableClusterMode">Indicates whether cluster mode is enabled.</param>
	/// <param name="children">Optional array of child migration definitions.</param>
	/// <returns>An instance of <see cref="IMigrationDefinitions"/>.</returns>
	public IMigrationDefinitions Create(
		string[] locations,
		string[] schemas,
		CommandTypes command,
		TransactionModes transactionMode,
		bool eraseDisabled,
		bool eraseOnValidationError,
		int startVersion,
		int? targetVersion,
		bool outOfOrder,
		bool skipNextMigrations,
		bool retryRepeatableMigrationsUntilNoError,
		string[] embeddedResourceAssemblies,
		string[] embeddedResourceFilters,
		int? commandTimeout,
		string encoding,
		string sqlMigrationPrefix,
		string sqlRepeatableMigrationPrefix,
		string sqlMigrationSeparator,
		string sqlMigrationSuffix,
		string? metadataTableSchema,
		string? metadataTableName,
		string placeholderPrefix,
		string placeholderSuffix,
		Dictionary<string, string>[] placeholders,
		bool enableClusterMode,
		IMigrationDefinitions[]? children)
	{
		return new MigrationDefinitionsVo(
			Locations: locations,
			Schemas: schemas,
			Command: command,
			TransactionMode: transactionMode,
			EraseDisabled: eraseDisabled,
			EraseOnValidationError: eraseOnValidationError,
			StartVersion: startVersion,
			TargetVersion: targetVersion,
			OutOfOrder: outOfOrder,
			SkipNextMigrations: skipNextMigrations,
			RetryRepeatableMigrationsUntilNoError: retryRepeatableMigrationsUntilNoError,
			EmbeddedResourceAssemblies: embeddedResourceAssemblies,
			EmbeddedResourceFilters: embeddedResourceFilters,
			CommandTimeout: commandTimeout,
			Encoding: encoding,
			SqlMigrationPrefix: sqlMigrationPrefix,
			SqlRepeatableMigrationPrefix: sqlRepeatableMigrationPrefix,
			SqlMigrationSeparator: sqlMigrationSeparator,
			SqlMigrationSuffix: sqlMigrationSuffix,
			MetadataTableSchema: metadataTableSchema,
			MetadataTableName: metadataTableName,
			PlaceholderPrefix: placeholderPrefix,
			PlaceholderSuffix: placeholderSuffix,
			Placeholders: placeholders,
			EnableClusterMode: enableClusterMode,
			Children: children);
	}
}

/// <summary>
/// Value object that holds migration definitions.
/// </summary>
/// <param name="Locations">An array of locations where the migrations are defined.</param>
/// <param name="Schemas">An array of schemas associated with the migrations.</param>
/// <param name="Command">The command type to be executed.</param>
/// <param name="TransactionMode">The transaction mode to use during migration.</param>
/// <param name="EraseDisabled">Indicates whether the erase operation is disabled.</param>
/// <param name="EraseOnValidationError">Indicates whether to erase on validation error.</param>
/// <param name="StartVersion">The starting version for the migration.</param>
/// <param name="TargetVersion">The target version for the migration, if any.</param>
/// <param name="OutOfOrder">Indicates whether to allow out-of-order migrations.</param>
/// <param name="SkipNextMigrations">Indicates whether to skip the next migrations.</param>
/// <param name="RetryRepeatableMigrationsUntilNoError">Indicates whether to retry repeatable migrations until no error occurs.</param>
/// <param name="EmbeddedResourceAssemblies">An array of embedded resource assemblies.</param>
/// <param name="EmbeddedResourceFilters">An array of filters for embedded resources.</param>
/// <param name="CommandTimeout">Optional timeout for the command execution.</param>
/// <param name="Encoding">The encoding to be used for migration scripts.</param>
/// <param name="SqlMigrationPrefix">The prefix to use for SQL migration files.</param>
/// <param name="SqlRepeatableMigrationPrefix">The prefix for repeatable SQL migration files.</param>
/// <param name="SqlMigrationSeparator">The separator used in SQL migration files.</param>
/// <param name="SqlMigrationSuffix">The suffix to use for SQL migration files.</param>
/// <param name="MetadataTableSchema">Optional schema for the metadata table.</param>
/// <param name="MetadataTableName">Optional name for the metadata table.</param>
/// <param name="PlaceholderPrefix">The prefix for placeholders in migration scripts.</param>
/// <param name="PlaceholderSuffix">The suffix for placeholders in migration scripts.</param>
/// <param name="Placeholders">An array of dictionaries for placeholder values.</param>
/// <param name="EnableClusterMode">Indicates whether cluster mode is enabled.</param>
/// <param name="Children">Optional array of child migration definitions.</param>
file record MigrationDefinitionsVo(
	string[] Locations,
	string[] Schemas,
	CommandTypes Command,
	TransactionModes TransactionMode,
	bool EraseDisabled,
	bool EraseOnValidationError,
	int StartVersion,
	int? TargetVersion,
	bool OutOfOrder,
	bool SkipNextMigrations,
	bool RetryRepeatableMigrationsUntilNoError,
	string[] EmbeddedResourceAssemblies,
	string[] EmbeddedResourceFilters,
	int? CommandTimeout,
	string Encoding,
	string SqlMigrationPrefix,
	string SqlRepeatableMigrationPrefix,
	string SqlMigrationSeparator,
	string SqlMigrationSuffix,
	string? MetadataTableSchema,
	string? MetadataTableName,
	string PlaceholderPrefix,
	string PlaceholderSuffix,
	Dictionary<string, string>[] Placeholders,
	bool EnableClusterMode,
	IMigrationDefinitions[]? Children) : IMigrationDefinitions;
