using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Collections;

public static class ValidatableCollectionPropertyExtensions
{
  public static Result<TValue> IfEmpty<TValue, TCollection>(
    this Validatable<TValue> validatable,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull 
  {
    if (func(validatable.Value).Length == 0)
    {
      return new Result<TValue>(new ArgumentException("Collection should not be empty.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEmpty<TValue, TCollection>(this Validatable<TValue> validatable,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Length != 0)
    {
      return new Result<TValue>(new ArgumentException("Collection should be empty.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfCountEquals<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Length == count)
    {
      return new Result<TValue>(new ArgumentException($"Collection count should not be equal to {count}.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfCountNotEquals<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Length != count)
    {
      return new Result<TValue>(new ArgumentException($"Collection count should be equal to {count}.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfCountGreaterThan<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Length > count)
    {
      return new Result<TValue>(new ArgumentException($"Collection count should not be greater than {count}.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfCountLessThan<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Length < count)
    {
      return new Result<TValue>(new ArgumentException($"Collection count should not be less than {count}.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfHasNullElements<TValue, TCollection>(this Validatable<TValue> validatable,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Any(x => x == null))
    {
      return new Result<TValue>(new ArgumentException($"Collection should not have null elements.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfContains<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    TCollection needle,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (func(validatable.Value).Contains(needle))
    {
      return new Result<TValue>(new ArgumentException("Collection should not contain element.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotContains<TValue, TCollection>(this Validatable<TValue> validatable, Func<TValue, TCollection[]> func,
    TCollection needle,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (!func(validatable.Value).Contains(needle))
    {
      return new Result<TValue>(new ArgumentException("Collection should contain element.", $"{validatable.ParamName}: {funcProperty}"));
    }

    return validatable.Value;
  }
}