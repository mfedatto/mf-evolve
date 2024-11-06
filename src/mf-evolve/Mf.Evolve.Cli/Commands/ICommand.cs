using Cocona;

namespace Mf.Evolve.Cli.Commands;

/// <summary>
///     Defines a contract for a command that can be executed with a specified set of parameters.
/// </summary>
/// <typeparam name="TParamSet">
///     The type of the parameter set, which must implement
///     <see cref="Cocona.ICommandParameterSet" />.
/// </typeparam>
public interface ICommand<in TParamSet>
	where TParamSet : ICommandParameterSet
{
	/// <summary>
	///     Gets the name of the command.
	/// </summary>
	string? Name { get; }

	/// <summary>
	///     Executes the command using the specified parameter set.
	/// </summary>
	/// <param name="options">The parameters for the command execution.</param>
	void Run(TParamSet options);
}
