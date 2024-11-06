using System.Runtime.InteropServices;
using System.Security.Principal;
using Cocona.Filters;
using Mf.Evolve.Domain.AppSettings;
using Mf.Evolve.Domain.Common;
using Microsoft.Extensions.Logging;
using Mono.Unix;

namespace Mf.Evolve.Cli.Filters;

/// <summary>
///     Implements <see cref="ICommandFilter" /> to provide custom logic for CLI command execution.
///     Displays a header with application details and handles error logging.
/// </summary>
public class CommandFilter : ICommandFilter
{
	private bool? _isAdmin;
	private bool _isAdminEvaluated;
	private readonly ILogger<CommandFilter> _logger;
	private readonly CliConfig _cliConfig;

	/// <summary>
	///     Initializes a new instance of the <see cref="CommandFilter" /> class.
	/// </summary>
	/// <param name="logger">Logger instance for error and informational logging.</param>
	/// <param name="cliConfig"></param>
	public CommandFilter(
		ILogger<CommandFilter> logger,
		CliConfig cliConfig)
	{
		_logger = logger;
		_cliConfig = cliConfig;
	}

	/// <summary>
	///     Executes the command filter, displaying headers and trailers around the command execution.
	///     Logs any unhandled exceptions.
	/// </summary>
	/// <param name="ctx">The context for the command execution.</param>
	/// <param name="next">The delegate representing the next middleware in the pipeline.</param>
	/// <returns>An integer representing the command execution result.</returns>
	public async ValueTask<int> OnCommandExecutionAsync(
		CoconaCommandExecutingContext ctx,
		CommandExecutionDelegate next)
	{
		try
		{
			WriteRuntimeInfo();
			WriteHeader();

			// ReSharper disable once SuggestVarOrType_BuiltInTypes
			var result = await next(ctx);

			WriteFarewell();

			CliCancellationToken.IsAppEndingRegularlly = true;

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError(
				ex,
				"An unhandled exception occurred during the execution of the application. Check the logs for more details.");

			return 1;
		}
	}

	/// <summary>
	///     Logs runtime information including OS details, platform architecture, command line, current directory, username,
	///     and administrator status.
	/// </summary>
	private void WriteRuntimeInfo()
	{
		_logger.LogInformation("""
		                       MF Evolve - Runtime Information:
		                       ~> OS: {osDescription}
		                       ~> Platform: {osArchitecture}
		                       ~> Command line:
		                          ~> {commandLine}
		                       ~> Directory:
		                          ~> {currentDirectory}
		                       ~> User: {userName}
		                       ~> Super User: {isAdministrator}
		                       """,
			RuntimeInformation.OSDescription,
			RuntimeInformation.OSArchitecture.ToString(),
			Environment.CommandLine,
			Directory.GetCurrentDirectory(),
			Environment.UserName,
			GetIsAdministratorIndication());
	}

	/// <summary>
	///     Writes the application header with system information to the console.
	/// </summary>
	private void WriteHeader()
	{
		ConsoleWriterHelper.WriteColoredLine("""
		                                                                                                                    
		                                      ███╗   ███╗ ███████╗   ███████╗ ██╗   ██╗  ██████╗  ██╗     ██╗   ██╗ ███████╗
		                                      ████╗ ████║ ██╔════╝   ██╔════╝ ██║   ██║ ██╔═══██╗ ██║     ██║   ██║ ██╔════╝
		                                      ██╔████╔██║ █████╗     █████╗   ██║   ██║ ██║   ██║ ██║     ██║   ██║ █████╗  
		                                      ██║╚██╔╝██║ ██╔══╝     ██╔══╝   ╚██╗ ██╔╝ ██║   ██║ ██║     ╚██╗ ██╔╝ ██╔══╝  
		                                      ██║ ╚═╝ ██║ ██║        ███████╗  ╚████╔╝  ╚██████╔╝ ███████╗ ╚████╔╝  ███████╗
		                                      ╚═╝     ╚═╝ ╚═╝        ╚══════╝   ╚═══╝    ╚═════╝  ╚══════╝  ╚═══╝   ╚══════╝
		                                                                                                                    
		                                     """,
			ConsoleColor.Black,
			ConsoleColor.Green);

		WriteRuler("Runtime Information");

		ConsoleWriterHelper.WriteColoredKeyValue(
			"OS",
			RuntimeInformation.OSDescription,
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		ConsoleWriterHelper.WriteColoredKeyValue(
			"Platform",
			RuntimeInformation.OSArchitecture.ToString(),
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		ConsoleWriterHelper.WriteColoredKeyValue(
			"Command line",
			Environment.CommandLine,
			$":{Environment.NewLine}~> ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		ConsoleWriterHelper.WriteColoredKeyValue(
			"Directory",
			Directory.GetCurrentDirectory(),
			$":{Environment.NewLine}~> ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		ConsoleWriterHelper.WriteColoredKeyValue(
			"User",
			Environment.UserName,
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		ConsoleWriterHelper.WriteColoredKeyValue(
			"Super User",
			GetIsAdministratorIndication(),
			": ",
			ConsoleColor.Green,
			IsAdministrator() is null or false
				? ConsoleColor.Cyan
				: ConsoleColor.Magenta);

		WriteRuler();
	}

	/// <summary>
	///     Gets a textual representation indicating if the current user is an administrator.
	/// </summary>
	/// <returns>A string representing administrator status ("Yes", "No", or "n/a" if unknown).</returns>
	private string GetIsAdministratorIndication()
	{
		bool? isAdmin = IsAdministrator();

		if (isAdmin is null)
		{
			return _cliConfig.CommandFilter.IsAdminNull;
		}

		return isAdmin.Value
			? _cliConfig.CommandFilter.IsAdminTrue
			: _cliConfig.CommandFilter.IsAdminFalse;
	}

	/// <summary>
	///     Checks if the current user has administrator privileges.
	/// </summary>
	/// <returns>A nullable boolean indicating administrator status; null if status cannot be determined.</returns>
	// ReSharper disable once MemberCanBeMadeStatic.Local
	private bool? IsAdministrator()
	{
		if (_isAdminEvaluated)
		{
			return _isAdmin;
		}

		_isAdmin = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
			? new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)
			: RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
				? new UnixUserInfo(UnixEnvironment.UserName).UserId == 0
				: null;

		_isAdminEvaluated = true;

		return _isAdmin;
	}

	/// <summary>
	///     Writes a ruler line with an optional message.
	/// </summary>
	/// <param name="message">Optional message to display in the middle of the ruler line.</param>
	/// <param name="size">The total width of the ruler line.</param>
	/// <param name="pattern">The pattern used to fill the ruler line.</param>
	// ReSharper disable once MemberCanBeMadeStatic.Local
	private void WriteRuler(
		string? message = null,
		int? size = null,
		string? pattern = null)
	{
		string rulerPattern = pattern ?? _cliConfig.CommandFilter.RulerPattern;
		int rulerSize = size ?? _cliConfig.CommandFilter.RulerSize;
		int patternRepetitions = (int)Math.Ceiling(rulerSize / (double)rulerPattern.Length);
		string patternRulerBase = string.Concat(Enumerable.Repeat(rulerPattern, patternRepetitions));
		ReadOnlySpan<char> patternRuler = patternRulerBase.AsSpan(0, rulerSize);

		if (string.IsNullOrEmpty(message))
		{
			Console.WriteLine(patternRuler.ToString());
		}

		// ReSharper disable once InvertIf
		if (!string.IsNullOrEmpty(message))
		{
			int spacerSize = _cliConfig.CommandFilter.SpacerSize;
			string spacer = new(' ', spacerSize);

			int preMessageSize = (rulerSize - message.Length) / 2 - spacerSize;
			// ReSharper disable once UselessBinaryOperation
			int postMessageSize = rulerSize - preMessageSize - message.Length - spacerSize * 2;

			ReadOnlySpan<char> preMessage = patternRuler[..preMessageSize];
			ReadOnlySpan<char> postMessage = patternRuler.Slice(rulerSize - postMessageSize, postMessageSize);

			string messageRuler = preMessage.ToString() + spacer + message + spacer + postMessage.ToString();

			Console.WriteLine(messageRuler);
		}
	}

	/// <summary>
	///     Writes a farewell message to the console.
	/// </summary>
	// ReSharper disable once MemberCanBeMadeStatic.Local
	private void WriteFarewell()
	{
		ConsoleWriterHelper.WriteColoredLine("""

		                                    "Again, you can’t connect the dots looking forward; you can only connect them
		                                    looking backward. So you have to trust that the dots will somehow connect in
		                                    your future. You have to trust in something — your gut, destiny, life, karma,
		                                    whatever. This approach has never let me down, and it has made all the
		                                    difference in my life."
		                                    Jobs, Steve. Commencement Address. Stanford University, 12 June 2005.

		                                    """,
			ConsoleColor.White);
	}
}
