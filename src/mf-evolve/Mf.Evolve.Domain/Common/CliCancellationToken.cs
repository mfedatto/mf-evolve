using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Mf.Evolve.Domain.Common;

/// <summary>
///     Provides a singleton cancellation token for CLI applications,
///     with automatic cancellation triggered on application exit.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class CliCancellationToken
{
	public static bool IsAppEndingRegularlly = false;
	
	private readonly CancellationTokenSource _cts = new();

	// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
	private readonly ILogger<CliCancellationToken> _logger;

	/// <summary>
	///     Initializes a new instance of the <see cref="CliCancellationToken" /> class.
	///     Registers a cancellation request to be triggered on application exit.
	/// </summary>
	public CliCancellationToken(
		ILogger<CliCancellationToken> logger)
	{
		_logger = logger;

		AppDomain.CurrentDomain.ProcessExit +=
			// ReSharper disable once UnusedParameter.Local
			// ReSharper disable once UnusedParameter.Local
			(sender, e) =>
			{
				if (IsAppEndingRegularlly)
				{
					return;
				}

				_cts.Cancel();

				try
				{
					_logger.LogWarning("Cancellation requested on application exit.");
				}
				catch
				{
					// ignored
				}
			};
	}

	/// <summary>
	///     Gets the application's global cancellation token.
	///     This token will be canceled when the application exits.
	/// </summary>
	public CancellationToken Token => _cts.Token;

	/// <summary>
	///     Requests cancellation of the global token.
	/// </summary>
	public void Cancel()
	{
		_cts.Cancel();
	}

	/// <summary>
	///     Releases resources held by the cancellation token source.
	///     Should be called when the application completes.
	/// </summary>
	public void Dispose()
	{
		_cts.Dispose();
	}
}
