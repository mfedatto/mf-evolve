using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.AppSettings;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public record CliConfig : IConfig
{
	public LoggingConfig Logging { get; set; } = new();
	public CommandFilterConfig CommandFilter { get; set; } = new();
	public string Section => "Cli";

	public record LoggingConfig
	{
		public bool ClearProviders { get; set; } = false;
		public bool AddSimpleConsole { get; set; } = false;
		public bool IncludeScopes { get; set; } = false;
		public string TimestampFormat { get; set; } = "yyyy-MM-dd HH:mm:ss ";
		public bool SingleLine { get; set; } = false;
		public bool EnabledLoggerColorBehavior { get; set; } = true;
	}

	public record CommandFilterConfig
	{
		public string IsAdminNull { get; set; } = "n/a";
		public string IsAdminTrue { get; set; } = "Yes";
		public string IsAdminFalse { get; set; } = "No";
		public int RulerSize { get; set; } = 80;
		public string RulerPattern { get; set; } = "=";
		public int SpacerSize { get; set; } = 1;
	}
}
