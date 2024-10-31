using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Cocona;
using Mf.Evolve.Cli.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Mf.Evolve.Cli.Extensions;

/// <summary>
///     Provides extension methods for adding commands to a <see cref="CoconaApp" />.
/// </summary>
/// <remarks>
///     This class contains methods for dynamically adding command classes based on the target namespace
///     and program class. It handles command registration and retrieval of command class information.
/// </remarks>
[UnconditionalSuppressMessage(
	"Trimming",
	"IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
	Justification = "Dynamic access is validated to ensure compatibility.")]
[UnconditionalSuppressMessage(
	"AOT",
	"IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.",
	Justification = "Dynamic code generation is validated and controlled for AOT compatibility.")]
[UnconditionalSuppressMessage(
	"Trimming",
	"IL2060:Member annotated with 'RequiresUnreferencedCodeAttribute' may be removed by trimming.",
	Justification = "Members are validated to ensure they are retained during trimming.")]
[UnconditionalSuppressMessage(
	"Trimming",
	"IL2091:Calling members annotated with 'RequiresUnreferencedCodeAttribute' may break functionality when trimming.",
	Justification = "Dynamic behavior is validated and conforms to trimming expectations.")]
[UnconditionalSuppressMessage(
	"Trimming",
	"IL2090:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when trimming.",
	Justification = "Dynamic behavior is carefully validated to ensure compatibility with trimming.")]
public static class CoconaAppExtensions
{
	/// <summary>
	///     Adds Cocona commands to the specified <see cref="CoconaApp" /> based on the target namespace.
	/// </summary>
	/// <typeparam name="TProgram">The program class containing the commands.</typeparam>
	/// <param name="app">The <see cref="CoconaApp" /> to which commands will be added.</param>
	/// <returns>The updated <see cref="CoconaApp" />.</returns>
	public static CoconaApp AddCoconaCommands<TProgram>(
		this CoconaApp app)
		where TProgram : class
	{
		ArgumentNullException.ThrowIfNull(app);

		string targetNamespace = GetCommandsBaseNamespaceFromClass<TProgram>();

		return app.AddCoconaCommands(targetNamespace);
	}

	/// <summary>
	///     Adds commands to the <see cref="CoconaApp" /> from the specified target namespace.
	/// </summary>
	/// <param name="app">The <see cref="CoconaApp" /> to which commands will be added.</param>
	/// <param name="targetNamespace">The namespace containing the command classes.</param>
	/// <returns>The updated <see cref="CoconaApp" />.</returns>
	private static CoconaApp AddCoconaCommands(
		this CoconaApp app,
		string targetNamespace)
	{
		ArgumentNullException.ThrowIfNull(app);
		ArgumentNullException.ThrowIfNull(targetNamespace, nameof(targetNamespace));

		MethodInfo addCommandMethod = GetAddCommandMethod();

		foreach (Type commandClass in GetICommandClasses(targetNamespace))
		{
			addCommandMethod.MakeGenericMethod(commandClass)
				.Invoke(null, [app]);
		}

		return app;
	}

	/// <summary>
	///     Retrieves the <see cref="MethodInfo" /> for the AddCommand method.
	/// </summary>
	/// <returns>The <see cref="MethodInfo" /> for the AddCommand method.</returns>
	private static MethodInfo GetAddCommandMethod()
	{
		MethodInfo addCommandMethod = typeof(CoconaAppExtensions)
			.GetMethods()
			.FirstOrDefault(
				method =>
					method is
					{
						// ReSharper disable once ArrangeStaticMemberQualifier
						Name: nameof(CoconaAppExtensions.AddCommand),
						IsGenericMethod: true
					})!;

		if (addCommandMethod is null)
		{
			throw new AddCommandMethodNotFoundException();
		}

		return addCommandMethod;
	}

	/// <summary>
	///     Retrieves the base namespace for commands from the specified program class.
	/// </summary>
	/// <typeparam name="TProgram">The program class.</typeparam>
	/// <param name="preserveDotCommandEnding">Indicates whether to preserve the dot command ending.</param>
	/// <returns>The commands base namespace.</returns>
	private static string GetCommandsBaseNamespaceFromClass<TProgram>(
		bool preserveDotCommandEnding = true)
		where TProgram : class
	{
		string? baseNamespace = typeof(TProgram).Namespace;

		if (baseNamespace is null)
		{
			throw new NullBaseNamespaceException();
		}

		const string dotCommands = ".Commands";

		if (baseNamespace.EndsWith(dotCommands)
		    && preserveDotCommandEnding)
		{
			return baseNamespace;
		}

		return $"{baseNamespace}{dotCommands}";
	}

	/// <summary>
	///     Retrieves classes within the target namespace.
	/// </summary>
	/// <param name="targetNamespace">The target namespace to search within.</param>
	/// <returns>A collection of classes in the specified namespace.</returns>
	private static IEnumerable<Type> GetTargetNamespaceClasses(
		string targetNamespace)
	{
		ArgumentNullException.ThrowIfNull(targetNamespace, nameof(targetNamespace));

		return Assembly.GetExecutingAssembly()
			.GetTypes()
			.Where(
				type =>
					type is { IsClass: true, Namespace: not null }
					&& (type.Namespace == targetNamespace
					    || type.Namespace.StartsWith(targetNamespace + ".")));
	}

	/// <summary>
	///     Retrieves command classes that implement the <see cref="ICommand{T}" /> interface.
	/// </summary>
	/// <param name="targetNamespace">The target namespace to search within.</param>
	/// <returns>A collection of command classes implementing <see cref="ICommand{T}" />.</returns>
	private static IEnumerable<Type> GetICommandClasses(
		string targetNamespace)
	{
		ArgumentNullException.ThrowIfNull(targetNamespace, nameof(targetNamespace));

		Type genericCommandInterface = typeof(ICommand<>);
		Type parameterSetInterface = typeof(ICommandParameterSet);

		return GetTargetNamespaceClasses(targetNamespace)
			.Where(
				([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] t) =>
					t.GetInterfaces()
						.Any(
							interfaceType =>
								interfaceType.IsGenericType
								&& interfaceType.GetGenericTypeDefinition() == genericCommandInterface
								&& parameterSetInterface.IsAssignableFrom(interfaceType.GetGenericArguments()[0])));
	}

	/// <summary>
	///     Adds a command of type <typeparamref name="TCommand" /> to the <see cref="CoconaApp" />.
	/// </summary>
	/// <typeparam name="TCommand">The type of command to add.</typeparam>
	/// <param name="app">The <see cref="CoconaApp" /> to which the command will be added.</param>
	/// <returns>The updated <see cref="CoconaApp" />.</returns>
	public static CoconaApp AddCommand<TCommand>(
		this CoconaApp app)
		where TCommand : class
	{
		ArgumentNullException.ThrowIfNull(app);

		return app.AddCommandInstance(
			ActivatorUtilities
				.CreateInstance<TCommand>(app.Services));
	}

	/// <summary>
	///     Adds a command instance to the <see cref="CoconaApp" />.
	/// </summary>
	/// <typeparam name="TCommand">The type of command instance.</typeparam>
	/// <param name="app">The <see cref="CoconaApp" /> to which the command instance will be added.</param>
	/// <param name="instance">The command instance to add.</param>
	/// <returns>The updated <see cref="CoconaApp" />.</returns>
	private static CoconaApp AddCommandInstance<TCommand>(
		this CoconaApp app,
		TCommand instance)
		where TCommand : class
	{
		ArgumentNullException.ThrowIfNull(app);
		ArgumentNullException.ThrowIfNull(instance, nameof(instance));

		string? commandName = GetCommandName(instance);

		if (commandName is not null)
		{
			app.AddCommand(
				commandName,
				CreateCommandDelegate(instance));
		}

		app.AddCommand(
			CreateCommandDelegate(instance));

		return app;
	}

	/// <summary>
	///     Obtém o nome do comando a partir da instância do comando.
	/// </summary>
	/// <typeparam name="TCommand">O tipo do comando, que deve ser uma classe.</typeparam>
	/// <param name="instance">A instância do comando a ser analisada.</param>
	/// <returns>O nome do comando, ou null se não estiver definido.</returns>
	/// <exception cref="CommandNamePropertyNotFoundException">
	///     Lançado quando a propriedade de nome do comando não é
	///     encontrada.
	/// </exception>
	private static string? GetCommandName<TCommand>(
		TCommand instance)
		where TCommand : class
	{
		ArgumentNullException.ThrowIfNull(instance, nameof(instance));

		PropertyInfo? property = typeof(TCommand)
			.GetProperty(nameof(ICommand<ICommandParameterSet>.Name));

		if (property is null)
		{
			throw new CommandNamePropertyNotFoundException(typeof(TCommand));
		}

		return property.GetValue(instance)?
			.ToString();
	}

	/// <summary>
	///     Retrieves the generic command interface implemented by the specified command class.
	/// </summary>
	/// <typeparam name="TCommand">The type of the command class, which must be a class.</typeparam>
	/// <returns>The type of the generic command interface implemented by the class.</returns>
	/// <exception cref="CommandDoesNotImplementICommandTParamSetException">
	///     Thrown when the command class does not implement
	///     the ICommand&lt;TParamSet&gt; interface.
	/// </exception>
	private static Type GetCommandInterfaceType<TCommand>()
		where TCommand : class
	{
		Type? interfaceType = typeof(TCommand).GetInterfaces()
			.FirstOrDefault(
				i => i.IsGenericType
				     && i.GetGenericTypeDefinition() == typeof(ICommand<>));

		if (interfaceType is null)
		{
			throw new CommandDoesNotImplementICommandTParamSetException(
				typeof(TCommand));
		}

		return interfaceType;
	}

	/// <summary>
	///     Retrieves the parameter set type from the specified command interface type.
	/// </summary>
	/// <param name="interfaceType">
	///     The type of the command interface, which must be a generic interface implementing ICommand
	///     &lt;TParamSet&gt;.
	/// </param>
	/// <returns>The type of the parameter set associated with the command interface.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the <paramref name="interfaceType" /> is null.</exception>
	private static Type GetParamSetType(
		Type interfaceType)
	{
		ArgumentNullException.ThrowIfNull(interfaceType, nameof(interfaceType));

		return interfaceType.GetGenericArguments()[0];
	}

	/// <summary>
	///     Retrieves the method information for the command's run method defined by the ICommand&lt;TParamSet&gt; interface.
	/// </summary>
	/// <typeparam name="TCommand">The type of the command, which must implement ICommand&lt;TParamSet&gt;.</typeparam>
	/// <returns>The <see cref="MethodInfo" /> representing the run method of the command.</returns>
	/// <exception cref="CommandDoesNotImplementExpectedMethodException">
	///     Thrown when the specified command type does not implement the expected run method.
	/// </exception>
	private static MethodInfo GetCommandMethodInfo<TCommand>()
		where TCommand : class
	{
		// ReSharper disable once InconsistentNaming
		const string commandMethodName = nameof(ICommand<ICommandParameterSet>.Run);
		MethodInfo? commandMethod = typeof(TCommand).GetMethod(commandMethodName);

		if (commandMethod is null)
		{
			throw new CommandDoesNotImplementExpectedMethodException(
				typeof(TCommand),
				commandMethodName);
		}

		return commandMethod;
	}

	/// <summary>
	///     Creates a delegate for the command's run method using the specified command instance.
	/// </summary>
	/// <typeparam name="TCommand">The type of the command instance, which must implement ICommand&lt;TParamSet&gt;.</typeparam>
	/// <param name="instance">The command instance from which to create the delegate.</param>
	/// <returns>A <see cref="Delegate" /> representing the command's run method.</returns>
	/// <exception cref="ArgumentNullException">Thrown when the <paramref name="instance" /> is null.</exception>
	private static Delegate CreateCommandDelegate<TCommand>(
		TCommand instance)
		where TCommand : class
	{
		ArgumentNullException.ThrowIfNull(instance, nameof(instance));

		Type interfaceType = GetCommandInterfaceType<TCommand>();
		Type paramSetType = GetParamSetType(interfaceType);
		MethodInfo commandMethod = GetCommandMethodInfo<TCommand>();
		Type delegateType = typeof(Action<>).MakeGenericType(paramSetType);

		return Delegate.CreateDelegate(delegateType, instance, commandMethod);
	}
}
