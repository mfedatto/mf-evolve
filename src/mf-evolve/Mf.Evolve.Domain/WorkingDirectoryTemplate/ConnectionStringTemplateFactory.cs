using System.Diagnostics.CodeAnalysis;
using Mf.Evolve.Domain.MigrationDefinitions;

namespace Mf.Evolve.Domain.WorkingDirectoryTemplate;

[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public class WorkingDirectoryTemplateFactory
{
	// ReSharper disable once MemberCanBePrivate.Global
	// ReSharper disable once MemberCanBeMadeStatic.Global
	public IWorkingDirectoryTemplate Create(
		string? workingDirectory,
		string? placeholderPrefix,
		string? placeholderSuffix,
		Dictionary<string, string>? placeholders)
	{
		return new WorkingDirectoryTemplateVo(
			workingDirectory,
			placeholderPrefix,
			placeholderSuffix,
			placeholders);
	}

	// ReSharper disable once MemberCanBeMadeStatic.Global
	public IWorkingDirectoryTemplate Create(
		object? parsedObject)
	{
		if (parsedObject is not Dictionary<object, object> dictionaryItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized working directory template key.");
		}

		return Create(
			ParseWorkingDirectory(dictionaryItem),
			ParsePlaceholderPrefix(dictionaryItem),
			ParsePlaceholderSuffix(dictionaryItem),
			ParsePlaceholders(dictionaryItem));
	}

	private string? ParseWorkingDirectory(
		Dictionary<object, object> dictionaryWorkingDirectoryTemplate)
	{
		return dictionaryWorkingDirectoryTemplate
			.TryGetValue(
				"WorkingDirectory",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	private string? ParsePlaceholderPrefix(
		Dictionary<object, object> dictionaryWorkingDirectoryTemplate)
	{
		return dictionaryWorkingDirectoryTemplate
			.TryGetValue(
				"PlaceholderPrefix",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	private string? ParsePlaceholderSuffix(
		Dictionary<object, object> dictionaryWorkingDirectoryTemplate)
	{
		return dictionaryWorkingDirectoryTemplate
			.TryGetValue(
				"PlaceholderSuffix",
				out object? rawValue)
			? rawValue.ToString()
			: null;
	}

	// ReSharper disable once MemberCanBeMadeStatic.Local
	private Dictionary<string, string>? ParsePlaceholders(
		Dictionary<object, object> dictionaryWorkingDirectoryTemplate)
	{
		Dictionary<string, string> result = new();

		if (!dictionaryWorkingDirectoryTemplate
			    .TryGetValue(
				    "Placeholders",
				    out object? rawValue))
		{
			return null;
		}

		if (rawValue is not List<object> listItem)
		{
			throw new UnexpectedDeserializedObjectTypeException(
				"Unexpected deserialized object type for deserialized working directory placeholders key.");
		}

		foreach (object item in listItem)
		{
			if (item is not Dictionary<object, object> dictionaryItem)
			{
				throw new UnexpectedDeserializedObjectTypeException(
					"Unexpected deserialized object type for deserialized working directory placeholders dictionary item.");
			}

			foreach (KeyValuePair<object, object> keyValuePair in dictionaryItem)
			{
				result[keyValuePair.Key.ToString()!] = keyValuePair.Value.ToString()!;
			}
		}

		return result;
	}
}

file record WorkingDirectoryTemplateVo(
	string? WorkingDirectory,
	string? PlaceholderPrefix,
	string? PlaceholderSuffix,
	Dictionary<string, string>? Placeholders) : IWorkingDirectoryTemplate;
