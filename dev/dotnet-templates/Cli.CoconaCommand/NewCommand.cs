using Cocona;

namespace __namespace__;

/// <summary>
/// Represents the __command_lower__ command in the CLI.
/// </summary>
public class __command__Command : ICommand<__command__Command.ParamSet>
{
	/// <summary>
	/// Gets the name of the command.
	/// </summary>
	public string Name => "__command_lower__";

	/// <summary>
	/// Executes the __command_lower__ command with the specified <see cref="ParamSet"/>
	/// parameters.
	/// </summary>
	/// <param name="paramSet">The parameter set for the __command_lower__ command.</param>
	public void Run(
		ParamSet paramSet)
	{
		
	}

	/// <summary>
	/// Represents an <see cref="Cocona.ICommandParameterSet"/> as the parameter
	/// set for the <see cref="__command__Command"/>.
	/// </summary>
	public record ParamSet() : ICommandParameterSet;
}
