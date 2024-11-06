using Mf.Evolve.Domain.Common;

namespace Mf.Evolve.Domain.WorkingDirectoryTemplate;

public interface IWorkingDirectoryTemplate : IPlaceholder
{
	// ReSharper disable once UnusedMember.Global
	string? WorkingDirectory { get; }
}
