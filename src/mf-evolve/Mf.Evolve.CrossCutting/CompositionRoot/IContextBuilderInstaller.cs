using Cocona.Builder;
using Microsoft.Extensions.Configuration;

namespace Mf.Evolve.CrossCutting.CompositionRoot;

/// <summary>
///     Defines a contract for installing context builders in a
///     <see cref="CoconaAppBuilder" />.
/// </summary>
public interface IContextBuilderInstaller
{
	/// <summary>
	///     Installs the context builder using the specified
	///     <see cref="CoconaAppBuilder" /> and <see cref="IConfiguration" />.
	/// </summary>
	/// <param name="contextBuilder">
	///     The <see cref="CoconaAppBuilder" /> instance used to configure the
	///     application.
	/// </param>
	/// <param name="configuration">
	///     The <see cref="IConfiguration" /> instance containing configuration
	///     settings.
	/// </param>
	void Install(
		// ReSharper disable once UnusedParameter.Global
		CoconaAppBuilder contextBuilder,
		// ReSharper disable once UnusedParameter.Global
		IConfiguration configuration);
}
