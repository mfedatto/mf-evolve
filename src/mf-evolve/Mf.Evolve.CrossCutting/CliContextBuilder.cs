using Cocona;
using Cocona.Builder;
using Mf.Evolve.CrossCutting.CompositionRoot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Mf.Evolve.CrossCutting;

/// <summary>
///     Implements the <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderInstaller" /> and
///     <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderAppConfigurator" /> interfaces
///     to provide configuration and installation logic for a command-line interface (CLI) application.
/// </summary>
public class CliContextBuilder : IContextBuilderInstaller, IContextBuilderAppConfigurator
{
	/// <summary>
	///     Configures the specified <see cref="CoconaApp" /> instance.
	/// </summary>
	/// <param name="app">The <see cref="CoconaApp" /> instance to configure.</param>
	/// <returns>The configured <see cref="CoconaApp" /> instance.</returns>
	public CoconaApp Configure(
		CoconaApp app)
	{
		if (!app.Environment.IsDevelopment())
		{
		}

		return app;
	}

	/// <summary>
	///     Installs the necessary services and configurations into the specified <see cref="CoconaAppBuilder" />.
	/// </summary>
	/// <param name="builder">The <see cref="CoconaAppBuilder" /> instance to configure.</param>
	/// <param name="configuration">Optional configuration settings. Defaults to <c>null</c>.</param>
	public void Install(
		CoconaAppBuilder builder,
		IConfiguration? configuration = null)
	{
	}
}