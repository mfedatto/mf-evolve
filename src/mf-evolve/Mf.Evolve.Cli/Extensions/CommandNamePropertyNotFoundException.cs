using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when the <c>CommandName</c> property is not found in a
///     command.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CommandNamePropertyNotFoundException : Exception
{
	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandNamePropertyNotFoundException" /> class with a
	///     default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public CommandNamePropertyNotFoundException()
		: base("The property CommandName was not found.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandNamePropertyNotFoundException" /> class with a
	///     specified command type.
	/// </summary>
	/// <param name="commandType">
	///     The type of the command that is missing the <c>CommandName</c>
	///     property.
	/// </param>
	// ReSharper disable once UnusedMember.Global
	public CommandNamePropertyNotFoundException(
		string commandType)
		: base($"The property CommandName was not found at command `{commandType}`.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="CommandNamePropertyNotFoundException" /> class with a
	///     specified command type.
	/// </summary>
	/// <param name="commandType">
	///     The type of the command that is missing the <c>CommandName</c>
	///     property.
	/// </param>
	// ReSharper disable once UnusedMember.Global
	public CommandNamePropertyNotFoundException(
		Type commandType) : this(commandType.ToString())
	{
	}
}
