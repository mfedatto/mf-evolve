using System.Runtime.InteropServices;
using System.Security.Principal;
using Cocona.Filters;
using Mf.Evolve.Domain.Common;
using Microsoft.Extensions.Logging;
using Mono.Unix;

namespace Mf.Evolve.Cli.Filters;

public class CommandFilter : ICommandFilter
{
	private static bool _isAdminEvaluated;
	private static bool? _isAdmin;
	private readonly ILogger<CommandFilter> _logger;

	public CommandFilter(
		ILogger<CommandFilter> logger)
	{
		_logger = logger;
	}

	public async ValueTask<int> OnCommandExecutionAsync(
		CoconaCommandExecutingContext ctx,
		CommandExecutionDelegate next)
	{
		try
		{
			WriteHeader();

			// ReSharper disable once SuggestVarOrType_BuiltInTypes
			var result = await next(ctx);

			WriteTrailer();

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

		WriteRuler("Application Information");

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

	private static void WriteTrailer()
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

	private static void WriteColoredLine(
		string message,
		ConsoleColor foregroundColor)
	{
		WriteColored(
			message + Environment.NewLine,
			foregroundColor);
	}

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

	private static void WriteColored(
		string message,
		ConsoleColor foregroundColor)
	{
		WriteColored(
			message,
			Console.BackgroundColor,
			foregroundColor);
	}

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
