using Mf.Evolve.Domain.MigrationDefinitions;

namespace Mf.Evolve.IO;

// ReSharper disable once InconsistentNaming
public class MigrationDefinitionsIo : IMigrationDefinitionsIO
{
	public string GetRawContent(
		string filePath,
		CancellationToken cancellationToken)
	{
		string result = GetRawContentAsync(
				filePath,
				cancellationToken)
			.GetAwaiter()
			.GetResult();

		return result;
	}

	public async Task<string> GetRawContentAsync(
		string filePath,
		CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		if (!File.Exists(filePath))
		{
			throw new FileNotFoundException(filePath);
		}

		// ReSharper disable once ConvertToUsingDeclaration
		using (StreamReader streamReader = new(filePath))
		{
			string result =
				await streamReader.ReadToEndAsync(
					cancellationToken);

			return result;
		}
	}
}
