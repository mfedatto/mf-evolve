using Cocona;

namespace Mf.Evolve.CrossCutting.CompositionRoot;

/// <summary>
/// Defines a contract for configuring a <see cref="CoconaApp" />.
/// </summary>
public interface IContextBuilderAppConfigurator
{
	/// <summary>
	/// Configures the specified <see cref="CoconaApp" /> instance.
	/// </summary>
	/// <param name="app">The <see cref="CoconaApp" /> instance to configure.</param>
	/// <returns>The configured <see cref="CoconaApp" /> instance.</returns>
	CoconaApp Configure(
		CoconaApp app);
}
