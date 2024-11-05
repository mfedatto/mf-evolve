using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.AppSettings;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public record CliConfig : IConfig
{
	public string Section => "Cli";

	public LoggingConfig Logging { get; set; } = new();

	public record LoggingConfig
	{
		public bool ClearProviders { get; set; } = false;
		public bool AddSimpleConsole { get; set; } = false;
		public bool IncludeScopes { get; set; } = false;
		public string TimestampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss ";
		public bool SingleLine { get; set; } = false;
		public bool EnabledLoggerColorBehavior { get; set; } = true;
	}
}
