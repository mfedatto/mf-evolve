using Mf.Evolve.Domain.Common;

namespace Mf.Evolve.Domain.WorkingDirectoryTemplate;

public interface IWorkingDirectoryTemplate : IPlaceholder
{
	string WorkingDirectory { get; }
}
