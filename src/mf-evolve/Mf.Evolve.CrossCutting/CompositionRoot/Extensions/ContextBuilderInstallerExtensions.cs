using Cocona.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Mf.Evolve.CrossCutting.CompositionRoot.Extensions;

/// <summary>
/// Provides extension methods for configuring a <see cref="CoconaAppBuilder" /> with composition root services.
/// </summary>
public static class ContextBuilderInstallerExtensions
{
	/// <summary>
	/// Adds a composition root to the <see cref="CoconaAppBuilder" /> using a specified context builder.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	/// The type of the context builder, which must implement
	/// <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderInstaller" />.
	/// </typeparam>
	/// <param name="builder">
	/// The instance of <see cref="CoconaAppBuilder" /> to which the composition root is
	/// added.
	/// </param>
	/// <returns>The updated <see cref="CoconaAppBuilder" /> instance.</returns>
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static CoconaAppBuilder AddCompositionRoot<TStartupContextBuilder>(
		this CoconaAppBuilder builder)
		where TStartupContextBuilder : IContextBuilderInstaller, new()
	{
		IConfiguration configuration = builder.BuildConfiguration();

		RootContextBuilder.ComposeRoot<TStartupContextBuilder>(
				builder,
				configuration);

		return builder;
	}

	/// <summary>
	/// Builds the context using the specified context builder and configuration.
	/// </summary>
	/// <typeparam name="TContextBuilderInstaller">
	/// The type of the context builder, which must implement
	/// <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderInstaller" />.
	/// </typeparam>
	/// <param name="builder">The instance of <see cref="CoconaAppBuilder" /> used to build the context.</param>
	/// <param name="configuration">The configuration used to bind settings to the context.</param>
	/// <returns>The updated <see cref="CoconaAppBuilder" /> instance.</returns>
	// ReSharper disable once UnusedMethodReturnValue.Local
	public static CoconaAppBuilder BuildContext<TContextBuilderInstaller>(
		this CoconaAppBuilder builder,
		IConfiguration configuration)
		where TContextBuilderInstaller : IContextBuilderInstaller, new()
	{
		TContextBuilderInstaller installer = new();

		// ReSharper disable once SuspiciousTypeConversion.Global
		if (installer is IContextBuilderConfigBinder configurator)
		{
			configurator.BindConfig(
				builder,
				configuration);
		}

		installer.Install(
			builder,
			configuration);

		return builder;
	}

	/// <summary>
	/// Builds the application's configuration by loading settings from JSON files and environment variables.
	/// </summary>
	/// <param name="builder">The instance of <see cref="CoconaAppBuilder" /> used to build the configuration.</param>
	/// <returns>An <see cref="IConfiguration" /> instance containing the application settings.</returns>
	private static IConfiguration BuildConfiguration(
		this CoconaAppBuilder builder)
	{
		return builder.Configuration
			.AddJsonFile(
				"appsettings.json",
				true,
				true)
			.AddJsonFile(
				$"appsettings.{builder.Environment.EnvironmentName}.json",
				true,
				true)
			.AddEnvironmentVariables()
			.Build();
	}
}
