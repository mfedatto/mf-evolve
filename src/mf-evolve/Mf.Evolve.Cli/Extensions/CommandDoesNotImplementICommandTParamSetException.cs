using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when a command does not implement the
///     <see cref="Mf.Evolve.Cli.Commands.ICommand{TParamSet}" /> interface.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CommandDoesNotImplementICommandTParamSetException : Exception
{
	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandDoesNotImplementICommandTParamSetException" />
	///     class with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementICommandTParamSetException()
		: base("The command does not implement ICommand<TParamSet>.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandDoesNotImplementICommandTParamSetException" />
	///     class with a specified command type.
	/// </summary>
	/// <param name="commandType">
	///     The type of the command that is missing the
	///     <see cref="Mf.Evolve.Cli.Commands.ICommand{TParamSet}" />
	///     implementation.
	/// </param>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementICommandTParamSetException(
		string commandType)
		: base($"The command `{commandType}` does not implement ICommand<TParamSet>.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandDoesNotImplementICommandTParamSetException" />
	///     class with a specified command type.
	/// </summary>
	/// <param name="commandType">
	///     The type of the command that is missing the
	///     <see cref="Mf.Evolve.Cli.Commands.ICommand{TParamSet}" />
	///     implementation.
	/// </param>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementICommandTParamSetException(
		Type commandType)
		: this(commandType.ToString())
	{
	}
}
