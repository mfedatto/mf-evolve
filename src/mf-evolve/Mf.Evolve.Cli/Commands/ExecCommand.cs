// Generated by the command "dotnet new mfcc.coconacommand.paramset" from MFedatto
// Namespace: Mf.Evolve.Cli.Commands
// Command Name: Exec (lowercase: exec)
// ParamSet: EvolveParamSet

using Mf.Evolve.Domain.Common;
using Mf.Evolve.Domain.Migration;
using Microsoft.Extensions.Logging;

namespace Mf.Evolve.Cli.Commands;

/// <summary>
///     Represents the exec command in the CLI.
/// </summary>
// ReSharper disable once UnusedType.Global
public class ExecCommand : ICommand<EvolveParamSet>
{
	private readonly CliCancellationToken _cliCancellationToken;

	// ReSharper disable once NotAccessedField.Local
	private readonly ILogger<ExecCommand> _logger;
	private readonly IMigrationApplication _migrationApplication;

	public ExecCommand(
		ILogger<ExecCommand> logger,
		IMigrationApplication migrationApplication,
		CliCancellationToken cliCancellationToken)
	{
		_logger = logger;
		_migrationApplication = migrationApplication;
		_cliCancellationToken = cliCancellationToken;
	}

	/// <summary>
	///     Gets the name of the command.
	/// </summary>
	public string Name => "exec";

	/// <summary>
	///     Executes the exec command with the specified <see cref="EvolveParamSet" />
	///     parameters.
	/// </summary>
	/// <param name="paramSet">The parameter set for the exec command.</param>
	public void Run(
		EvolveParamSet paramSet)
	{
		_migrationApplication.ExecAsync(
				paramSet.FilePath,
				_cliCancellationToken.Token)
			.GetAwaiter()
			.GetResult();
	}
}
