using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
/// Exception thrown when a null base namespace is encountered.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class NullLoggerException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="NullLoggerException" /> class
	/// with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullLoggerException()
		: base("Can't handle a null logger.")
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="NullLoggerException" /> class
	/// with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullLoggerException(
		string loggerType)
		: base($"Can't handle null `{loggerType}`.")
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="NullLoggerException" /> class
	/// with a default error message.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public NullLoggerException(
		Type loggerType)
		: this(loggerType.ToString())
	{
	}
}
