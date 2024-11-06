using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when a command does not implement an expected method.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CommandDoesNotImplementExpectedMethodException : Exception
{
	/// <summary>
	///     Initializes a new instance of the <see cref="CommandDoesNotImplementExpectedMethodException" /> class
	///     with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementExpectedMethodException()
		: base("The command does not implement the expected method.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="CommandDoesNotImplementExpectedMethodException" /> class
	///     with a specified method name.
	/// </summary>
	/// <param name="methodName">The name of the method that is expected but not implemented.</param>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementExpectedMethodException(
		string methodName)
		: base($"The command does not implement the expected method `{methodName}`.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="CommandDoesNotImplementExpectedMethodException" /> class
	///     with specified command type and method name.
	/// </summary>
	/// <param name="commandType">The type of the command that is missing the expected method.</param>
	/// <param name="methodName">The name of the expected method.</param>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementExpectedMethodException(
		string commandType,
		string methodName)
		: base($"The command `{commandType}` does not implement the expected method `{methodName}`.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the <see cref="CommandDoesNotImplementExpectedMethodException" /> class
	///     with specified command type and method name.
	/// </summary>
	/// <param name="commandType">The type of the command that is missing the expected method.</param>
	/// <param name="methodName">The name of the expected method.</param>
	// ReSharper disable once UnusedMember.Global
	public CommandDoesNotImplementExpectedMethodException(
		Type commandType,
		string methodName)
		: this(commandType.ToString(), methodName)
	{
	}
}
