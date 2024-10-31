using Cocona;
using Mf.Evolve.Cli.Extensions;
using Mf.Evolve.CrossCutting;

namespace Mf.Evolve.Cli;

public class Program
{
	public static void Main(string[] args)
	{
		CoconaApp.CreateBuilder(args)
			.Setup<CliContextBuilder>()
			.Build()
			.Configure<CliContextBuilder>()
			.Run();
	}
}
