using System.Diagnostics.CodeAnalysis;

namespace Mf.Evolve.Domain.Common;

/// <summary>
///     Provides helper methods for retrieving and merging values between parent
///     and child contexts.
/// </summary>
/// <typeparam name="TContext">The type of the parent and child contexts.</typeparam>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InheritanceHelper<TContext>
{
	private readonly TContext _parent;
	private readonly TContext _child;

	/// <summary>
	///     Initializes a new instance of the
	///     <see cref="InheritanceHelper{TContext}"/> class.
	/// </summary>
	/// <exception cref="ArgumentNullException">
	///     Thrown if either <paramref name="parent"/> or
	///     <paramref name="child"/> is null.
	/// </exception>
	public InheritanceHelper(
		TContext parent,
		TContext child)
	{
		ArgumentNullException.ThrowIfNull(parent);
		ArgumentNullException.ThrowIfNull(child);

		_parent = parent;
		_child = child;
	}

	/// <summary>
	///     Retrieves a value from the child context, falling back to the parent
	///     context if the child value is null.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to retrieve.</typeparam>
	/// <param name="getter">
	///     A function to get the value from the context.
	/// </param>
	public TValue Get<TValue>(
		Func<TContext, TValue> getter)
	{
		TValue parentValue = getter(_parent);
		TValue childValue = getter(_child);

		return childValue ?? parentValue;
	}

	/// <summary>
	///     Retrieves a nullable value from the child context, falling back to
	///     the parent context if the child value is null.
	/// </summary>
	/// <typeparam name="TValue">
	///     The type of the nullable value to retrieve.
	/// </typeparam>
	public TValue? GetNullable<TValue>(
		Func<TContext, TValue?> getter)
	{
		return Get(getter);
	}

	/// <summary>
	///     Retrieves a string value from the child context, falling back to the
	///     parent context if the child value is null.
	/// </summary>
	public string GetString(
		Func<TContext, string> getter)
	{
		return Get(getter);
	}

	/// <summary>
	///     Retrieves a boolean value from the child context, falling back to
	///     the parent context if the child value is null.
	/// </summary>
	public bool GetBoolean(
		Func<TContext, bool> getter)
	{
		return Get(getter);
	}

	/// <summary>
	///     Retrieves an integer value from the child context, falling back to
	///     the parent context if the child value is null.
	/// </summary>
	public int GetInt32(
		Func<TContext, int> getter)
	{
		return Get(getter);
	}

	/// <summary>
	///     Retrieves a string array from the child context, falling back to the
	///     parent context if the child value is null.
	/// </summary>
	public string[] GetStringArray(
		Func<TContext, string[]> getter)
	{
		return Get(getter);
	}

	/// <summary>
	///     Retrieves a nullable string value from the child context, falling
	///     back to the parent context if the child value is null.
	/// </summary>
	public string? GetNullableString(
		Func<TContext, string?> getter)
	{
		return GetNullable(getter);
	}

	/// <summary>
	///     Retrieves a nullable boolean value from the child context, falling
	///     back to the parent context if the child value is null.
	/// </summary>
	public bool? GetNullableBoolean(
		Func<TContext, bool?> getter)
	{
		return GetNullable(getter);
	}

	/// <summary>
	///     Retrieves a nullable integer value from the child context, falling
	///     back to the parent context if the child value is null.
	/// </summary>
	public int? GetNullableInt32(
		Func<TContext, int?> getter)
	{
		return GetNullable(getter);
	}

	/// <summary>
	///     Retrieves a nullable string array from the child context, falling
	///     back to the parent context if the child value is null.
	/// </summary>
	public string[]? GetNullableStringArray(
		Func<TContext, string[]?> getter)
	{
		return GetNullable(getter);
	}

	/// <summary>
	///     Merges two dictionaries, one from the parent context and one from
	///     the child context. The parent dictionary is used as the base, and
	///     the child dictionary is merged on top of it.
	/// </summary>
	public Dictionary<TKey, TValue> Merge<TKey, TValue>(
		Func<TContext, Dictionary<TKey, TValue>> getter)
		where TKey : notnull
	{
		Dictionary<TKey, TValue> parentValue = getter(_parent);
		Dictionary<TKey, TValue> childValue = getter(_child);
		Dictionary<TKey, TValue> result = new();

		foreach (KeyValuePair<TKey, TValue> parentKvItem in parentValue)
		{
			result[parentKvItem.Key] = childValue[parentKvItem.Key];
		}

		return result;
	}

	/// <summary>
	///     Merges two nullable dictionaries, one from the parent context and
	///     one from the child context. The parent dictionary is used as the
	///     base, and the child dictionary is merged on top of it.
	/// </summary>
	public Dictionary<TKey, TValue>? MergeNullable<TKey, TValue>(
		Func<TContext, Dictionary<TKey, TValue>?> getter)
		where TKey : notnull
	{
		Dictionary<TKey, TValue>? parentValue = getter(_parent);
		Dictionary<TKey, TValue>? childValue = getter(_child);

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (childValue is null)
		{
			return parentValue;
		}

		// ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
		if (parentValue is null)
		{
			return childValue;
		}

		Dictionary<TKey, TValue> result = new(childValue);

		foreach (KeyValuePair<TKey, TValue> parentKvItem in parentValue)
		{
			if (result.ContainsKey(parentKvItem.Key))
			{
				result[parentKvItem.Key] = childValue[parentKvItem.Key];
				
				continue;
			}
			
			result.Add(parentKvItem.Key, parentKvItem.Value);
		}

		return result;
	}
}
