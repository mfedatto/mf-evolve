namespace Mf.Evolve.Domain.AppSettings;

/// <summary>
///     Represents a configuration interface that provides the section name for
///     configuration binding.
/// </summary>
public interface IConfig
{
	/// <summary>
	///     Gets the configuration section name associated with this
	///     configuration.
	/// </summary>
	string Section { get; }
}
