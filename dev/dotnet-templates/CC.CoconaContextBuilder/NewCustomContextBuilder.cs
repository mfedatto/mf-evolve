using Cocona;
using Cocona.Builder;
using __namespace__.CompositionRoot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace __namespace__;

/// <summary>
/// Implements the <see cref="__namespace__.CompositionRoot.IContextBuilderInstaller" /> and
/// <see cref="__namespace__.CompositionRoot.IContextBuilderAppConfigurator" /> interfaces
/// to provide configuration and installation logic for the __context__ context.
/// </summary>
public class __context__ContextBuilder : IContextBuilderInstaller, IContextBuilderAppConfigurator
{
	/// <summary>
	/// Installs the necessary services and configurations into the specified <see cref="CoconaAppBuilder" />.
	/// </summary>
	/// <param name="builder">The <see cref="CoconaAppBuilder" /> instance to configure.</param>
	/// <param name="configuration">Optional configuration settings. Defaults to <c>null</c>.</param>
	public void Install(
		CoconaAppBuilder builder,
		IConfiguration? configuration = null)
	{
		// builder.Services.AddSingleton<IMember__context__, Member__context__>();
	}

	/// <summary>
	/// Configures the specified <see cref="CoconaApp" /> instance.
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
}
