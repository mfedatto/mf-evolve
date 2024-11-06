using System.Diagnostics.CodeAnalysis;
using Mf.Evolve.Domain.MigrationDefinitions;

namespace Mf.Evolve.Domain.ConnectionStringTemplate;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public class ConnectionStringTemplateFactory
{
	// ReSharper disable once MemberCanBePrivate.Global
	// ReSharper disable once MemberCanBeMadeStatic.Global
	public IConnectionStringTemplate Create(
		string? connectionString,
		string? placeholderPrefix,
		string? placeholderSuffix,
		Dictionary<string, string>? placeholders)
	{
		return new ConnectionStringTemplateVo(
			connectionString,
			placeholderPrefix,
			placeholderSuffix,
			placeholders);
	}

	// ReSharper disable once MemberCanBeMadeStatic.Global
	public IConnectionStringTemplate Create(
		object? parsedObject)
	{
		if (parsedObject is not Dictionary<object, object> dictionaryItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized connection string template key.");
		}

		return Create(
			ParseConnectionString(dictionaryItem),
			ParsePlaceholderPrefix(dictionaryItem),
			ParsePlaceholderSuffix(dictionaryItem),
			ParsePlaceholders(dictionaryItem));
	}

	private string? ParseConnectionString(
		Dictionary<object, object> dictionaryConnectionStringTemplate)
	{
		return dictionaryConnectionStringTemplate
			.TryGetValue(
				"ConnectionString",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	private string? ParsePlaceholderPrefix(
		Dictionary<object, object> dictionaryConnectionStringTemplate)
	{
		return dictionaryConnectionStringTemplate
			.TryGetValue(
				"PlaceholderPrefix",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	private string? ParsePlaceholderSuffix(
		Dictionary<object, object> dictionaryConnectionStringTemplate)
	{
		return dictionaryConnectionStringTemplate
			.TryGetValue(
				"PlaceholderSuffix",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	// ReSharper disable once MemberCanBeMadeStatic.Local
	private Dictionary<string, string>? ParsePlaceholders(
		Dictionary<object, object> dictionaryConnectionStringTemplate)
	{
		Dictionary<string, string> result = new();

		if (!dictionaryConnectionStringTemplate
			    .TryGetValue(
				    "Placeholders",
				    out object? rawValue))
		{
			return null;
		}

		if (rawValue is not List<object> listItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized connection string placeholders key.");
		}

		foreach (object item in listItem)
		{
			if (item is not Dictionary<object, object> dictionaryItem)
			{
				throw new UnexpectedDeserializedObjectTypeException(
					"Unexpected deserialized object type for deserialized connection string placeholders dictionary item.");
			}

			foreach (KeyValuePair<object, object> keyValuePair in dictionaryItem)
			{
				result[keyValuePair.Key.ToString()!] = keyValuePair.Value.ToString()!;
			}
		}

		return result;
	}
}

file record ConnectionStringTemplateVo(
	string? ConnectionString,
	string? PlaceholderPrefix,
	string? PlaceholderSuffix,
	Dictionary<string, string>? Placeholders) : IConnectionStringTemplate;