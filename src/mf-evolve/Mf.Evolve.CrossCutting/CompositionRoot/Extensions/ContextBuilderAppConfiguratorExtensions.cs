using Cocona;

namespace Mf.Evolve.CrossCutting.CompositionRoot.Extensions;

/// <summary>
///     Provides extension methods for configuring a <see cref="CoconaApp" />.
/// </summary>
public static class ContextBuilderAppConfiguratorExtensions
{
	/// <summary>
	///     Configures the specified <see cref="CoconaApp" /> using a context builder.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	///     The type of the context builder, which must implement
	///     <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderAppConfigurator" />.
	/// </typeparam>
	/// <param name="app">The instance of <see cref="CoconaApp" /> to configure.</param>
	/// <returns>The configured <see cref="CoconaApp" /> instance.</returns>
	public static CoconaApp ConfigureApp<TStartupContextBuilder>(
		this CoconaApp app)
		where TStartupContextBuilder : IContextBuilderAppConfigurator, new()
	{
		return app.Configure<TStartupContextBuilder>();
	}

	/// <summary>
	///     Configures the <see cref="CoconaApp" /> using the specified context builder.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	///     The type of the context builder, which must implement
	///     <see cref="Mf.Evolve.CrossCutting.CompositionRoot.IContextBuilderAppConfigurator" />.
	/// </typeparam>
	/// <param name="app">The instance of <see cref="CoconaApp" /> to configure.</param>
	/// <returns>The configured <see cref="CoconaApp" /> instance.</returns>
	private static CoconaApp Configure<TStartupContextBuilder>(
		this CoconaApp app)
		where TStartupContextBuilder : IContextBuilderAppConfigurator, new()
	{
		return new TStartupContextBuilder().Configure(app);
	}
}
