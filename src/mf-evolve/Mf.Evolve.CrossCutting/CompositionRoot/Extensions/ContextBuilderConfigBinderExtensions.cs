using Cocona.Builder;
using Mf.Evolve.Domain.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mf.Evolve.CrossCutting.CompositionRoot.Extensions;

/// <summary>
///     Provides extension methods for binding configuration sections to
///     configurator classes.
/// </summary>
public static class ContextBuilderConfigBinderExtensions
{
	/// <summary>
	///     Binds a configuration section to a specified configurator type and
	///     registers it as a singleton service.
	/// </summary>
	/// <typeparam name="TConfigurator">
	///     The type of the configurator, which must implement
	///     <see cref="IConfig"/> and have a parameterless constructor.
	/// </typeparam>
	/// <param name="builder">
	///     The <see cref="CoconaAppBuilder"/> to which the configuration will
	///     be bound.
	/// </param>
	/// <param name="configuration">
	///     The <see cref="IConfiguration"/> instance containing the
	///     configuration data.
	/// </param>
	// ReSharper disable once UnusedMethodReturnValue.Global
	public static CoconaAppBuilder BindConfig<TConfigurator>(
		this CoconaAppBuilder builder,
		IConfiguration configuration)
		where TConfigurator : class, IConfig, new()
	{
		TConfigurator configurator = new();

		configuration.GetSection(configurator.Section)
			.Bind(configurator);

		builder.Services.AddSingleton(configurator);

		return builder;
	}
}
