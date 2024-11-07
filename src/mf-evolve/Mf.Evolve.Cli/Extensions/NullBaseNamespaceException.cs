using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Exception thrown when a null base namespace is encountered.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class NullBaseNamespaceException : Exception
{
	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="NullBaseNamespaceException" /> class with a default error
	///     message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullBaseNamespaceException()
		: base("Can't handle a null base namespace.")
	{
	}

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="NullBaseNamespaceException" /> class.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullBaseNamespaceException(
		string paramName)
		: base($"Can't handle a null base namespace as given by `{paramName}`.")
	{
	}

	/// <summary>
	///     Throws an exception if the specified argument is null.
	/// </summary>
	/// <param name="argument">The argument to check for null.</param>
	/// <param name="paramName">
	///     The name of the parameter, automatically supplied by the compiler if
	///     not specified.</param>
	/// <exception cref="NullBaseNamespaceException">
	///     Thrown if the argument is null.
	/// </exception>
	public static void ThrowIfNullOrWhiteSpace(
		[NotNull] object? argument,
		[CallerArgumentExpression(nameof(argument))]
		string? paramName = null)
	{
		if (argument is null
			|| string.IsNullOrWhiteSpace((string)argument))
		{
			Throw(paramName);
		}
	}

	/// <summary>
	///     Throws an <see cref="NullBaseNamespaceException"/> with the
	///     specified parameter name.
	/// </summary>
	/// <param name="paramName">
	///     The name of the parameter that caused the exception, if available.
	///     </param>
	/// <exception cref="NullBaseNamespaceException">
	///     Always thrown when this method is called.
	/// </exception>
	[DoesNotReturn]
	public static void Throw(
		string? paramName = null)
	{
		if (paramName is null)
		{
			throw new NullBaseNamespaceException();
		}

		throw new NullBaseNamespaceException(
			paramName);
	}
}
