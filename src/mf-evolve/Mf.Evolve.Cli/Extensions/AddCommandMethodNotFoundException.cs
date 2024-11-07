using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when the <c>AddCommand</c> method is not found.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class AddCommandMethodNotFoundException : Exception
{
	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="AddCommandMethodNotFoundException" /> class.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public AddCommandMethodNotFoundException()
		: base("The method AddCommand was not found.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="AddCommandMethodNotFoundException" /> class.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public AddCommandMethodNotFoundException(
		string paramName)
		: base($"The method AddCommand at `{paramName}` was not found.")
	{
	}

	/// <summary>
	///     Throws an exception if the specified argument is null.
	/// </summary>
	/// <param name="argument">The argument to check for null.</param>
	/// <param name="paramName">
	///     The name of the parameter, automatically supplied by the compiler if
	///     not specified.</param>
	/// <exception cref="AddCommandMethodNotFoundException">
	///     Thrown if the argument is null.
	/// </exception>
	public static void ThrowIfNull(
		[NotNull] object? argument,
		[CallerArgumentExpression(nameof(argument))]
		string? paramName = null)
	{
		if (argument is null)
		{
			Throw(paramName);
		}
	}

	/// <summary>
	///     Throws an <see cref="AddCommandMethodNotFoundException"/> with the
	///     specified parameter name.
	/// </summary>
	/// <param name="paramName">
	///     The name of the parameter that caused the exception, if available.
	///     </param>
	/// <exception cref="AddCommandMethodNotFoundException">
	///     Always thrown when this method is called.
	/// </exception>
	[DoesNotReturn]
	public static void Throw(
		string? paramName = null)
	{
		if (paramName is null)
		{
			throw new AddCommandMethodNotFoundException();
		}

		throw new AddCommandMethodNotFoundException(
			paramName);
	}
}
