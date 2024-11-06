using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
///     Specifies the types of commands that can be executed in the migration process.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum CommandTypes
{
	/// <summary>
	///     Indicates that the command type is not defined.
	/// </summary>
	Undefined,

	/// <summary>
	///     Represents a command to perform migration.
	/// </summary>
	Migrate,

	/// <summary>
	///     Represents a command to erase migrations.
	/// </summary>
	Erase,

	/// <summary>
	///     Represents a command to repair migrations.
	/// </summary>
	Repair,

	/// <summary>
	///     Represents a command to retrieve information about migrations.
	/// </summary>
	Info
}
