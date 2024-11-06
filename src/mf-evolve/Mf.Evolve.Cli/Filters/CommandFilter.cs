using System.Runtime.InteropServices;
using System.Security.Principal;
using Cocona.Filters;
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
	private static bool _isAdminEvaluated;
	private static bool? _isAdmin;
	private readonly ILogger<CommandFilter> _logger;

	/// <summary>
	///     Initializes a new instance of the <see cref="CommandFilter" /> class.
	/// </summary>
	/// <param name="logger">Logger instance for error and informational logging.</param>
	public CommandFilter(
		ILogger<CommandFilter> logger)
	{
		_logger = logger;
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
	private static void WriteHeader()
	{
		WriteColoredLine("""
		                                                                                                
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

		WriteColoredKeyValue(
			"OS",
			RuntimeInformation.OSDescription,
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		WriteColoredKeyValue(
			"Platform",
			RuntimeInformation.OSArchitecture.ToString(),
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		WriteColoredKeyValue(
			"Command line",
			Environment.CommandLine,
			$":{Environment.NewLine}~> ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		WriteColoredKeyValue(
			"Directory",
			Directory.GetCurrentDirectory(),
			$":{Environment.NewLine}~> ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);
		WriteColoredKeyValue(
			"User",
			Environment.UserName,
			": ",
			ConsoleColor.Green,
			ConsoleColor.Cyan);

		WriteColoredKeyValue(
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
	private static string GetIsAdministratorIndication()
	{
		bool? isAdmin = IsAdministrator();

		if (isAdmin is null)
		{
			return "n/a";
		}

		return isAdmin.Value
			? "Yes"
			: "No";
	}

	/// <summary>
	///     Checks if the current user has administrator privileges.
	/// </summary>
	/// <returns>A nullable boolean indicating administrator status; null if status cannot be determined.</returns>
	private static bool? IsAdministrator()
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
	private static void WriteRuler(
		string message = "",
		int size = 80,
		string pattern = "=")
	{
		int patternRepetitions = (int)Math.Ceiling(size / (double)pattern.Length);
		string patternRulerBase = string.Concat(Enumerable.Repeat(pattern, patternRepetitions));
		ReadOnlySpan<char> patternRuler = patternRulerBase.AsSpan(0, size);

		if (string.IsNullOrEmpty(message))
		{
			Console.WriteLine(patternRuler.ToString());
		}

		// ReSharper disable once InvertIf
		if (!string.IsNullOrEmpty(message))
		{
			int spacerSize = 1;
			string spacer = new(' ', spacerSize);

			int preMessageSize = (size - message.Length) / 2 - spacerSize;
			// ReSharper disable once UselessBinaryOperation
			int postMessageSize = size - preMessageSize - message.Length - spacerSize * 2;

			ReadOnlySpan<char> preMessage = patternRuler[..preMessageSize];
			ReadOnlySpan<char> postMessage = patternRuler.Slice(size - postMessageSize, postMessageSize);

			string messageRuler = preMessage.ToString() + spacer + message + spacer + postMessage.ToString();

			Console.WriteLine(messageRuler);
		}
	}

	/// <summary>
	///     Writes a farewell message to the console.
	/// </summary>
	private static void WriteFarewell()
	{
		WriteColoredLine("""

		                 "Again, you can’t connect the dots looking forward; you can only connect them
		                 looking backward. So you have to trust that the dots will somehow connect in
		                 your future. You have to trust in something — your gut, destiny, life, karma,
		                 whatever. This approach has never let me down, and it has made all the
		                 difference in my life."
		                 Jobs, Steve. Commencement Address. Stanford University, 12 June 2005.

		                 """,
			ConsoleColor.White);
	}

	/// <summary>
	///     Writes a message to the console with a specified foreground color, followed by a newline.
	/// </summary>
	/// <param name="message">The message to be written to the console.</param>
	/// <param name="foregroundColor">The color of the text.</param>
	private static void WriteColoredLine(
		string message,
		ConsoleColor foregroundColor)
	{
		WriteColored(
			message + Environment.NewLine,
			foregroundColor);
	}

	/// <summary>
	///     Writes a message to the console with specified background and foreground colors, followed by a newline.
	/// </summary>
	/// <param name="message">The message to be written to the console.</param>
	/// <param name="backgroundColor">The background color of the text.</param>
	/// <param name="foregroundColor">The color of the text.</param>
	private static void WriteColoredLine(
		string message,
		ConsoleColor backgroundColor,
		ConsoleColor foregroundColor)
	{
		WriteColored(
			message + Environment.NewLine,
			backgroundColor,
			foregroundColor);
	}

	/// <summary>
	///     Writes a message to the console with specified background and foreground colors.
	/// </summary>
	/// <param name="message">The message to be written to the console.</param>
	/// <param name="backgroundColor">The background color of the text.</param>
	/// <param name="foregroundColor">The color of the text.</param>
	private static void WriteColored(
		string message,
		ConsoleColor backgroundColor,
		ConsoleColor foregroundColor)
	{
		ConsoleColor defaultBackgroundColor = Console.BackgroundColor;
		ConsoleColor defaultForegroundColor = Console.ForegroundColor;

		Console.BackgroundColor = backgroundColor;
		Console.ForegroundColor = foregroundColor;

		Console.Write(message);

		Console.BackgroundColor = defaultBackgroundColor;
		Console.ForegroundColor = defaultForegroundColor;
	}

	/// <summary>
	///     Writes a message to the console with a specified foreground color.
	/// </summary>
	/// <param name="message">The message to be written to the console.</param>
	/// <param name="foregroundColor">The color of the text.</param>
	private static void WriteColored(
		string message,
		ConsoleColor foregroundColor)
	{
		WriteColored(
			message,
			Console.BackgroundColor,
			foregroundColor);
	}

	/// <summary>
	///     Writes a key-value pair to the console with specified colors for the key and value.
	/// </summary>
	/// <param name="key">The key part of the pair.</param>
	/// <param name="value">The value part of the pair.</param>
	/// <param name="separator">The separator string between the key and value.</param>
	/// <param name="keyForegroundColor">The color of the key text.</param>
	/// <param name="valueForegroundColor">The color of the value text.</param>
	private static void WriteColoredKeyValue(
		string key,
		string value,
		string separator,
		ConsoleColor keyForegroundColor,
		ConsoleColor valueForegroundColor)
	{
		WriteColoredKeyValue(
			key,
			value,
			separator,
			Console.BackgroundColor,
			keyForegroundColor,
			Console.BackgroundColor,
			valueForegroundColor,
			Console.BackgroundColor,
			Console.ForegroundColor);
	}

	/// <summary>
	///     Writes a key-value pair to the console with specified colors for the key, value, and separator.
	/// </summary>
	/// <param name="key">The key part of the pair.</param>
	/// <param name="value">The value part of the pair.</param>
	/// <param name="separator">The separator string between the key and value.</param>
	/// <param name="keyBackgroundColor">The background color of the key text.</param>
	/// <param name="keyForegroundColor">The color of the key text.</param>
	/// <param name="valueBackgroundColor">The background color of the value text.</param>
	/// <param name="valueForegroundColor">The color of the value text.</param>
	/// <param name="separatorBackgroundColor">The background color of the separator text.</param>
	/// <param name="separatorForegroundColor">The color of the separator text.</param>
	private static void WriteColoredKeyValue(
		string key,
		string value,
		string separator,
		ConsoleColor keyBackgroundColor,
		ConsoleColor keyForegroundColor,
		ConsoleColor valueBackgroundColor,
		ConsoleColor valueForegroundColor,
		ConsoleColor separatorBackgroundColor,
		ConsoleColor separatorForegroundColor)
	{
		WriteColored(key, keyBackgroundColor, keyForegroundColor);
		WriteColored(separator, separatorBackgroundColor, separatorForegroundColor);
		WriteColoredLine(value, valueBackgroundColor, valueForegroundColor);
	}
}
