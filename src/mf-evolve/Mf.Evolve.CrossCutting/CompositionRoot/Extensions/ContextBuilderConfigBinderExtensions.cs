using Cocona.Builder;
using Mf.Evolve.Domain.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mf.Evolve.CrossCutting.CompositionRoot.Extensions;

public static class ContextBuilderConfigBinderExtensions
{
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
