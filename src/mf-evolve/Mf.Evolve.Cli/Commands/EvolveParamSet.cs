
	public record ParamSet(
		[Option("file", ['f'], Description = "Migration definitions file path")]
		string FilePath = GlobalConstants.DefaultFileName
		) : ICommandParameterSet;
