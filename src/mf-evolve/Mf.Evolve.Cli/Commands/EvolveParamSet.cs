using Cocona;
using Mf.Evolve.Domain.Common;

namespace Mf.Evolve.Cli.Commands;

public record EvolveParamSet(
	[Option("file", ['f'], Description = "Migration definitions file path")]
	string FilePath = GlobalConstants.DefaultFileName
) : ICommandParameterSet;
