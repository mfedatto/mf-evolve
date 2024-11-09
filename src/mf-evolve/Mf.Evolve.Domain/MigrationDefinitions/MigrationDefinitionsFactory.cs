using System.Diagnostics.CodeAnalysis;
using Mf.Evolve.Domain.Common;
using Mf.Evolve.Domain.ConnectionStringTemplate;
using Mf.Evolve.Domain.WorkingDirectoryTemplate;
using YamlDotNet.Serialization;

namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
///     Factory class for creating instances of
///     <see cref="IMigrationDefinitions" />.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public class MigrationDefinitionsFactory
{
	private readonly IDeserializer _deserializer;
	private readonly ConnectionStringTemplateFactory _connectionStringTemplateFactory;
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
	///     Creates a new instance of <see cref="IMigrationDefinitions" /> with
	///     the specified parameters.
	/// </summary>
	// ReSharper disable once MemberCanBePrivate.Global
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

	#region Create flattened list

	/// <summary>
	///     Creates a flattened list of migration definitions from a provided
	///     array of definitions.
	/// </summary>
	public IMigrationDefinitions[] CreateFlattenedList(
		IMigrationDefinitions[] migrationDefinitionsList)
	{
		List<IMigrationDefinitions> result = [];

		foreach (IMigrationDefinitions parent in migrationDefinitionsList)
		{
			IMigrationDefinitions[] flattenedListFromParent =
				CreateFlattenedListFromParent(parent);

			result.AddRange(flattenedListFromParent);
		}

		return result
			.ToArray();
	}

	/// <summary>
	///     Creates a flattened list of migration definitions from a provided
	///     parent definition. This method is recursive.
	/// </summary>
	private IMigrationDefinitions[] CreateFlattenedListFromParent(
		IMigrationDefinitions parent)
	{
		if (parent.Children is null
		    || parent.Children.Length == 0)
		{
			return [parent];
		}

		List<IMigrationDefinitions> result = [];

		foreach (IMigrationDefinitions child in parent.Children
			         .SelectMany(CreateFlattenedListFromParent))
		{
			IMigrationDefinitions flattenedChild =
				CreateFlattenChild(
					parent,
					child);

			result.Add(flattenedChild);
		}

		return result
			.ToArray();
	}

	/// <summary>
	///     Creates a new instance of <see cref="IMigrationDefinitions" /> by 
	///     flattening the child definitions into the parent context. This 
	///     method uses the <see cref="InheritanceHelper{TContext}" /> to merge 
	///     values from the parent and child contexts, ensuring that child 
	///     values override parent values where applicable.
	/// </summary>
	[SuppressMessage("ReSharper", "RedundantTypeArgumentsOfMethod")]
	private IMigrationDefinitions CreateFlattenChild(
		IMigrationDefinitions parent,
		IMigrationDefinitions child)
	{
		InheritanceHelper<IMigrationDefinitions> definitionsInheritanceHelper =
			new(parent, child);

		IMigrationDefinitions flattenedChild =
			Create(
				dbms: definitionsInheritanceHelper
					.Get<EvolveDbms?>(ih => ih.Dbms),
				connectionStringTemplate: CreateFlattenConnectionStringTemplate(
					parent,
					child),
				workingDirectoryTemplate: CreateFlattenWorkingDirectoryTemplate(
					parent,
					child),
				locations: definitionsInheritanceHelper
					.GetNullableStringArray(ih => ih.Locations),
				schemas: definitionsInheritanceHelper
					.GetNullableStringArray(ih => ih.Schemas),
				placeholderPrefix: definitionsInheritanceHelper
					.GetNullableString(ih => ih.PlaceholderPrefix),
				placeholderSuffix: definitionsInheritanceHelper
					.GetNullableString(ih => ih.PlaceholderSuffix),
				placeholders: definitionsInheritanceHelper
					.MergeNullable<string, string>(ih => ih.Placeholders),
				command: definitionsInheritanceHelper
					.Get<CommandTypes?>(ih => ih.Command),
				transactionMode: definitionsInheritanceHelper
					.Get<TransactionModes?>(ih => ih.TransactionMode),
				eraseDisabled: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.EraseDisabled),
				eraseOnValidationError: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.EraseOnValidationError),
				startVersion: definitionsInheritanceHelper
					.GetNullableInt32(ih => ih.StartVersion),
				targetVersion: definitionsInheritanceHelper
					.GetNullableInt32(ih => ih.TargetVersion),
				outOfOrder: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.OutOfOrder),
				skipNextMigrations: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.SkipNextMigrations),
				retryRepeatableMigrationsUntilNoError: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.RetryRepeatableMigrationsUntilNoError),
				embeddedResourceAssemblies: definitionsInheritanceHelper
					.GetNullableStringArray(ih => ih.EmbeddedResourceAssemblies),
				embeddedResourceFilters: definitionsInheritanceHelper
					.GetNullableStringArray(ih => ih.EmbeddedResourceFilters),
				commandTimeout: definitionsInheritanceHelper
					.GetNullableInt32(ih => ih.CommandTimeout),
				encoding: definitionsInheritanceHelper
					.GetNullableString(ih => ih.Encoding),
				sqlMigrationPrefix: definitionsInheritanceHelper
					.GetNullableString(ih => ih.SqlMigrationPrefix),
				sqlRepeatableMigrationPrefix: definitionsInheritanceHelper
					.GetNullableString(ih => ih.SqlRepeatableMigrationPrefix),
				sqlMigrationSeparator: definitionsInheritanceHelper
					.GetNullableString(ih => ih.SqlMigrationSeparator),
				sqlMigrationSuffix: definitionsInheritanceHelper
					.GetNullableString(ih => ih.SqlMigrationSuffix),
				metadataTableSchema: definitionsInheritanceHelper
					.GetNullableString(ih => ih.MetadataTableSchema),
				metadataTableName: definitionsInheritanceHelper
					.GetNullableString(ih => ih.MetadataTableName),
				enableClusterMode: definitionsInheritanceHelper
					.GetNullableBoolean(ih => ih.EnableClusterMode),
				children: null);
		return flattenedChild;
	}

	/// <summary>
	///     Creates a flattened connection string template by merging the parent
	///     and child contexts.
	/// </summary>
	private IConnectionStringTemplate CreateFlattenConnectionStringTemplate(IMigrationDefinitions parent,
		IMigrationDefinitions child)
	{
		InheritanceHelper<IConnectionStringTemplate?> connectionStringInheritanceHelper =
			new(
				parent.ConnectionStringTemplate,
				child.ConnectionStringTemplate);
		IConnectionStringTemplate result =
			_connectionStringTemplateFactory.Create(
				connectionStringInheritanceHelper
					.GetNullableString(ih => ih?.ConnectionString),
				connectionStringInheritanceHelper
					.GetNullableString(ih => ih?.PlaceholderPrefix),
				connectionStringInheritanceHelper
					.GetNullableString(ih => ih?.PlaceholderSuffix),
				connectionStringInheritanceHelper
					.MergeNullable(ih => ih?.Placeholders));

		return result;
	}

	/// <summary>
	///     Creates a flattened working directory template by merging the parent
	///     and child contexts.
	/// </summary>
	private IWorkingDirectoryTemplate CreateFlattenWorkingDirectoryTemplate(IMigrationDefinitions parent,
		IMigrationDefinitions child)
	{
		InheritanceHelper<IWorkingDirectoryTemplate?> workingDirectoryInheritanceHelper =
			new(
				parent.WorkingDirectoryTemplate,
				child.WorkingDirectoryTemplate);
		IWorkingDirectoryTemplate result =
			_workingDirectoryTemplateFactory.Create(
				workingDirectoryInheritanceHelper
					.GetNullableString(ih => ih?.WorkingDirectory),
				workingDirectoryInheritanceHelper
					.GetNullableString(ih => ih?.PlaceholderPrefix),
				workingDirectoryInheritanceHelper
					.GetNullableString(ih => ih?.PlaceholderSuffix),
				workingDirectoryInheritanceHelper
					.MergeNullable(ih => ih?.Placeholders));

		return result;
	}

	#endregion

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
