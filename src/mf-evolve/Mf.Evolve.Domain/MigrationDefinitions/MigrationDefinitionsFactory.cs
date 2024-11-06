using System.Diagnostics.CodeAnalysis;
using Mf.Evolve.Domain.ConnectionStringTemplate;
using Mf.Evolve.Domain.WorkingDirectoryTemplate;
using YamlDotNet.Serialization;

namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
///     Factory class for creating instances of <see cref="IMigrationDefinitions" />.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public class MigrationDefinitionsFactory
{
	private readonly ConnectionStringTemplateFactory _connectionStringTemplateFactory;
	private readonly IDeserializer _deserializer;
	private readonly WorkingDirectoryTemplateFactory _workingDirectoryTemplateFactory;

	public MigrationDefinitionsFactory(
		IDeserializer deserializer,
		ConnectionStringTemplateFactory connectionStringTemplateFactory,
		WorkingDirectoryTemplateFactory workingDirectoryTemplateFactory)
	{
		_deserializer = deserializer;
		_connectionStringTemplateFactory = connectionStringTemplateFactory;
		_workingDirectoryTemplateFactory = workingDirectoryTemplateFactory;
	}

	/// <summary>
	///     Creates a new instance of <see cref="IMigrationDefinitions" /> with the specified parameters.
	/// </summary>
	/// <param name="dbms">The DBMS as defined by Evolve.</param>
	/// <param name="connectionStringTemplate">An <see cref="IConnectionStringTemplate" /> for the database connection string.</param>
	/// <param name="workingDirectoryTemplate">
	///     An <see cref="IWorkingDirectoryTemplate" /> for the Evolve execution working
	///     directory.
	/// </param>
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
	/// <param name="retryRepeatableMigrationsUntilNoError">
	///     Indicates whether to retry repeatable migrations until no error
	///     occurs.
	/// </param>
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
	/// <returns>An instance of <see cref="IMigrationDefinitions" />.</returns>
	public IMigrationDefinitions Create(
		EvolveDbms? dbms,
		IConnectionStringTemplate? connectionStringTemplate,
		IWorkingDirectoryTemplate? workingDirectoryTemplate,
		string[]? locations,
		string[]? schemas,
		string? placeholderPrefix,
		string? placeholderSuffix,
		Dictionary<string, string>? placeholders,
		CommandTypes? command,
		TransactionModes? transactionMode,
		bool? eraseDisabled,
		bool? eraseOnValidationError,
		int? startVersion,
		int? targetVersion,
		bool? outOfOrder,
		bool? skipNextMigrations,
		bool? retryRepeatableMigrationsUntilNoError,
		string[]? embeddedResourceAssemblies,
		string[]? embeddedResourceFilters,
		int? commandTimeout,
		string? encoding,
		string? sqlMigrationPrefix,
		string? sqlRepeatableMigrationPrefix,
		string? sqlMigrationSeparator,
		string? sqlMigrationSuffix,
		string? metadataTableSchema,
		string? metadataTableName,
		bool? enableClusterMode,
		IMigrationDefinitions[]? children)
	{
		IMigrationDefinitions result = new MigrationDefinitionsVo(
			dbms,
			connectionStringTemplate,
			workingDirectoryTemplate,
			locations,
			schemas,
			PlaceholderPrefix: placeholderPrefix,
			PlaceholderSuffix: placeholderSuffix,
			Placeholders: placeholders,
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
			EnableClusterMode: enableClusterMode,
			Children: children);

		return result;
	}

	public IMigrationDefinitions[] CreateFlattenedList(
		IMigrationDefinitions[] migrationDefinitionsList)
	{
		throw new NotImplementedException();
	}

	#region Create from Yaml

	public IMigrationDefinitions[]? Create(
		string rawYaml)
	{
		object? deserialized = _deserializer.Deserialize(rawYaml);

		return deserialized is not IEnumerable<object> deserializedList
			? []
			: Create(deserializedList);
	}

	private IMigrationDefinitions[]? Create(
		IEnumerable<object> deserializedList)
	{
		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (deserializedList is null)
		{
			return null;
		}

		List<IMigrationDefinitions> result = [];

		foreach (object deserializedItem in deserializedList)
		{
			IMigrationDefinitions itemResult = ParseObjectItem(deserializedItem);

			result.Add(itemResult);
		}

		return result.ToArray();
	}

	private IMigrationDefinitions ParseObjectItem(
		object deserializedItem)
	{
		if (deserializedItem is not Dictionary<object, object> dictionaryItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized migration definitions key.");
		}

		if (!dictionaryItem
			    .TryGetValue(
				    "MigrationDefinitions",
				    out object? migrationDefinitionsObject))
		{
			throw new ExpectedItemKeyNotFoundException("The expected key `MigrationDefinitions` was not found.");
		}

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (migrationDefinitionsObject is null)
		{
			throw new NullReferenceException("Null migration definitions.");
		}

		if (migrationDefinitionsObject is not Dictionary<object, object> dictionaryMigrationDefinitions)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized migration definitions value.");
		}

		IMigrationDefinitions result = Create(
			ParseEnum<EvolveDbms>(
				dictionaryMigrationDefinitions,
				"Dbms"),
			ParseDelegate<IConnectionStringTemplate>(
				dictionaryMigrationDefinitions,
				"ConnectionStringTemplate",
				raw => _connectionStringTemplateFactory.Create(raw)),
			ParseDelegate<IWorkingDirectoryTemplate>(
				dictionaryMigrationDefinitions,
				"WorkingDirectoryTemplate",
				raw => _workingDirectoryTemplateFactory.Create(raw)),
			ParseListAsArray<string>(
				dictionaryMigrationDefinitions,
				"Locations"),
			ParseListAsArray<string>(
				dictionaryMigrationDefinitions,
				"Schemas"),
			ParseString(
				dictionaryMigrationDefinitions,
				"PlaceholderPrefix"),
			ParseString(
				dictionaryMigrationDefinitions,
				"PlaceholderSuffix"),
			ParseDictionary(
				dictionaryMigrationDefinitions,
				"Placeholders"),
			ParseEnum<CommandTypes>(
				dictionaryMigrationDefinitions,
				"Command"),
			ParseEnum<TransactionModes>(
				dictionaryMigrationDefinitions,
				"TransactionMode"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"EraseDisabled"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"EraseOnValidationError"),
			ParseInt32(
				dictionaryMigrationDefinitions,
				"StartVersion"),
			ParseInt32(
				dictionaryMigrationDefinitions,
				"TargetVersion"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"OutOfOrder"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"SkipNextMigrations"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"RetryRepeatableMigrationsUntilNoError"),
			ParseListAsArray<string>(
				dictionaryMigrationDefinitions,
				"EmbeddedResourceAssemblies"),
			ParseListAsArray<string>(
				dictionaryMigrationDefinitions,
				"EmbeddedResourceFilters"),
			ParseInt32(
				dictionaryMigrationDefinitions,
				"CommandTimeout"),
			ParseString(
				dictionaryMigrationDefinitions,
				"Encoding"),
			ParseString(
				dictionaryMigrationDefinitions,
				"SqlMigrationPrefix"),
			ParseString(
				dictionaryMigrationDefinitions,
				"SqlRepeatableMigrationPrefix"),
			ParseString(
				dictionaryMigrationDefinitions,
				"SqlMigrationSeparator"),
			ParseString(
				dictionaryMigrationDefinitions,
				"SqlMigrationSuffix"),
			ParseString(
				dictionaryMigrationDefinitions,
				"MetadataTableSchema"),
			ParseString(
				dictionaryMigrationDefinitions,
				"MetadataTableName"),
			ParseBoolean(
				dictionaryMigrationDefinitions,
				"EnableClusterMode"),
			ParseDelegate<IMigrationDefinitions[]>(
				dictionaryMigrationDefinitions,
				"Children",
				raw => Create((IEnumerable<object>)raw!)!));

		return result;
	}

	private TResult? ParseDelegate<TResult>(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key,
		Func<object?, TResult> expression)
	{
		return dictionaryMigrationDefinitions
			.TryGetValue(
				key,
				out object? rawValue)
			? expression(rawValue)
			: default;
	}

	private TType[]? ParseListAsArray<TType>(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
	{
		if (!dictionaryMigrationDefinitions
			    .TryGetValue(
				    key,
				    out object? rawValue)
		    || rawValue is not List<object> listItem)
		{
			return null;
		}

		return listItem
			.Cast<TType>()
			.ToArray();
	}

	private string? ParseString(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
	{
		return dictionaryMigrationDefinitions
			.TryGetValue(
				key,
				out object? rawValue)
			// ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
			? rawValue?.ToString()
			: null;
	}

	private Dictionary<string, string>? ParseDictionary(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
	{
		Dictionary<string, string> result = new();

		if (!dictionaryMigrationDefinitions
			    .TryGetValue(
				    key,
				    out object? rawValue))
		{
			return null;
		}

		if (rawValue is not List<object> listItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				$"Unexpected deserialized object type for deserialized `{key}` key.");
		}

		foreach (object item in listItem)
		{
			if (item is not Dictionary<object, object> dictionaryItem)
			{
				continue;
			}

			foreach (KeyValuePair<object, object> keyValuePair in dictionaryItem)
			{
				result[keyValuePair.Key.ToString()!] = keyValuePair.Value.ToString()!;
			}
		}

		return result;
	}

	private TEnum? ParseEnum<TEnum>(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
		where TEnum : struct, Enum
	{
		if (dictionaryMigrationDefinitions
			    .TryGetValue(
				    key,
				    out object? rawValue)
		    && Enum.TryParse(
			    (string?)rawValue,
			    out TEnum parsedValue))
		{
			return parsedValue;
		}

		return null;
	}

	private bool? ParseBoolean(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
	{
		if (dictionaryMigrationDefinitions
			    .TryGetValue(
				    key,
				    out object? rawValue)
		    && bool.TryParse(
			    (string?)rawValue,
			    out bool parsedValue))
		{
			return parsedValue;
		}

		return null;
	}

	private int? ParseInt32(
		Dictionary<object, object> dictionaryMigrationDefinitions,
		string key)
	{
		if (dictionaryMigrationDefinitions
			    .TryGetValue(
				    key,
				    out object? rawValue)
		    && int.TryParse(
			    (string?)rawValue,
			    out int parsedValue))
		{
			return parsedValue;
		}

		return null;
	}

	#endregion
}

/// <summary>
///     Value object that holds migration definitions.
/// </summary>
file record MigrationDefinitionsVo(
	EvolveDbms? Dbms,
	IConnectionStringTemplate? ConnectionStringTemplate,
	IWorkingDirectoryTemplate? WorkingDirectoryTemplate,
	string[]? Locations,
	string[]? Schemas,
	CommandTypes? Command,
	TransactionModes? TransactionMode,
	bool? EraseDisabled,
	bool? EraseOnValidationError,
	int? StartVersion,
	int? TargetVersion,
	bool? OutOfOrder,
	bool? SkipNextMigrations,
	bool? RetryRepeatableMigrationsUntilNoError,
	string[]? EmbeddedResourceAssemblies,
	string[]? EmbeddedResourceFilters,
	int? CommandTimeout,
	string? Encoding,
	string? SqlMigrationPrefix,
	string? SqlRepeatableMigrationPrefix,
	string? SqlMigrationSeparator,
	string? SqlMigrationSuffix,
	string? MetadataTableSchema,
	string? MetadataTableName,
	string? PlaceholderPrefix,
	string? PlaceholderSuffix,
	Dictionary<string, string>? Placeholders,
	bool? EnableClusterMode,
	IMigrationDefinitions[]? Children) : IMigrationDefinitions;
