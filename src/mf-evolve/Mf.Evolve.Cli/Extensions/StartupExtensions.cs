using Cocona;
using Cocona.Builder;
using Mf.Evolve.Cli.Filters;
using Mf.Evolve.CrossCutting.CompositionRoot;
using Mf.Evolve.CrossCutting.CompositionRoot.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Provides extension methods for configuring
///     <see cref="CoconaAppBuilder" /> and <see cref="CoconaApp" />.
/// </summary>
public static class StartupExtensions
{
	/// <summary>
	///     Configures the <see cref="CoconaAppBuilder" /> by adding a
	///     composition root based on the specified startup context builder.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	///     The type of the startup context builder, which must implement
	///     <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderInstaller" />.
	/// </typeparam>
	/// <param name="builder">
	///     The <see cref="CoconaAppBuilder" /> to configure.
	/// </param>
	public static CoconaAppBuilder Setup<TStartupContextBuilder>(
		this CoconaAppBuilder builder)
		where TStartupContextBuilder : IContextBuilderInstaller, new()
	{
		ArgumentNullException.ThrowIfNull(builder);

		builder.AddCompositionRoot<TStartupContextBuilder>();

		return builder;
	}

	/// <summary>
	///     Configures the <see cref="CoconaApp" /> by adding commands from the
	///     specified startup context builder.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	///     The type of the startup context builder, which must implement
	///     <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderAppConfigurator" />.
	/// </typeparam>
	/// <param name="app">The <see cref="CoconaApp" /> to configure.</param>
	public static CoconaApp Configure<TStartupContextBuilder>(
		this CoconaApp app)
		where TStartupContextBuilder : IContextBuilderAppConfigurator, new()
	{
		ArgumentNullException.ThrowIfNull(app);

		app.UseFilter(
			ActivatorUtilities.CreateInstance<CommandFilter>(app.Services));

		return app.ConfigureApp<TStartupContextBuilder>()
			.AddCoconaCommands<Program>();
	}
}
