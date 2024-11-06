namespace Mf.Evolve.Domain.Common;

public static class ConsoleWriterHelper
{
	/// <summary>
	///     Writes a message to the console with a specified foreground color, followed by a newline.
	/// </summary>
	/// <param name="message">The message to be written to the console.</param>
	/// <param name="foregroundColor">The color of the text.</param>
	// ReSharper disable once MemberCanBeMadeStatic.Local
	public static void WriteColoredLine(
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
	// ReSharper disable once MemberCanBeMadeStatic.Local
	public static void WriteColoredLine(
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
	// ReSharper disable once MemberCanBeMadeStatic.Local
	public static void WriteColored(
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
	public static void WriteColored(
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
	// ReSharper disable once MemberCanBeMadeStatic.Local
	public static void WriteColoredKeyValue(
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
	public static void WriteColoredKeyValue(
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
