namespace Mf.Evolve.Domain.MigrationDefinitions;

/// <summary>
/// Specifies the modes of transaction management.
/// </summary>
public enum TransactionModes
{
	/// <summary>
	/// Indicates that the transaction mode is not defined.
	/// </summary>
	Undefined,

	/// <summary>
	/// Represents a mode where each transaction is committed individually.
	/// </summary>
	CommitEach,

	/// <summary>
	/// Represents a mode where all transactions are committed as a single unit.
	/// </summary>
	CommitAll,

	/// <summary>
	/// Represents a mode where all transactions are rolled back regardless if it was success or not.
	/// </summary>
	RollbackAll
}
