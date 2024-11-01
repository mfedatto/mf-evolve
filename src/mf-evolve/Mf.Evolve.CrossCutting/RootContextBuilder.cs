using Cocona.Builder;
using Mf.Evolve.CrossCutting.CompositionRoot;
using Mf.Evolve.CrossCutting.CompositionRoot.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Mf.Evolve.CrossCutting;

public class RootContextBuilder
{
	public static void ComposeRoot<TStartupContextBuilder>(
		CoconaAppBuilder builder,
		IConfiguration configuration)
		where TStartupContextBuilder : IContextBuilderInstaller, new()
	{
		builder
			.BuildContext<DomainContextBuilder>(configuration)
			.BuildContext<TStartupContextBuilder>(configuration);

		builder.Host.UseDefaultServiceProvider(
			(context, options) =>
			{
				options.ValidateOnBuild = context.HostingEnvironment.IsDevelopment();
				options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
			});
	}
}
