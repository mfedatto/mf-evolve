using Cocona.Builder;
using Mf.Evolve.CrossCutting.CompositionRoot;
using Mf.Evolve.CrossCutting.CompositionRoot.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Mf.Evolve.CrossCutting;

/// <summary>
///     Configures the application's root context by composing multiple context
///     builders and setting up service provider options.
/// </summary>
public class RootContextBuilder
{
	/// <summary>
	///     Composes the root context for the application by initializing and
	///     configuring various context builders and setting service provider
	///     validation options based on the hosting environment.
	/// </summary>
	/// <typeparam name="TStartupContextBuilder">
	///     The type of the startup context builder, implementing
	///     <see cref="IContextBuilderInstaller"/>.
	/// </typeparam>
	/// <param name="builder">
	///     The <see cref="CoconaAppBuilder"/> instance to configure the
	///     application's root context.
	/// </param>
	/// <param name="configuration">
	///     The <see cref="IConfiguration"/> instance containing configuration
	///     settings for the contexts.
	/// </param>
	public static void ComposeRoot<TStartupContextBuilder>(
		CoconaAppBuilder builder,
		IConfiguration configuration)
		where TStartupContextBuilder : IContextBuilderInstaller, new()
	{
		builder
			.BuildContext<DomainContextBuilder>(configuration)
			.BuildContext<ApplicationContextBuilder>(configuration)
			.BuildContext<ServiceContextBuilder>(configuration)
			.BuildContext<IOContextBuilder>(configuration)
			.BuildContext<TStartupContextBuilder>(configuration);

		builder.Host.UseDefaultServiceProvider(
			(context, options) =>
			{
				options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
				options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
			});
	}
}
