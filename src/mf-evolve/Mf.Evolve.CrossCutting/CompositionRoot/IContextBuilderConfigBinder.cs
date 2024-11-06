using Cocona.Builder;
using Microsoft.Extensions.Configuration;

namespace Mf.Evolve.CrossCutting.CompositionRoot;

/// <summary>
///     Defines a contract for binding configuration settings to a <see cref="CoconaAppBuilder" />.
/// </summary>
public interface IContextBuilderConfigBinder
{
	/// <summary>
	///     Binds the specified configuration settings to the given <see cref="CoconaAppBuilder" />.
	/// </summary>
	/// <param name="builder">
	///     The <see cref="CoconaAppBuilder" /> instance to which the configuration will be
	///     bound.
	/// </param>
	/// <param name="configuration">The configuration settings to bind.</param>
	void BindConfig(
		CoconaAppBuilder builder,
		IConfiguration configuration);
}
