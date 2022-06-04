using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Equalities;

public static class ValidatableEqualitiesPropertyExtensions
{
  public static Result<TValue> IfEquals<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    TOther other,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
    {
      return new Result<TValue>(new ArgumentException($"Value should not be equal to {other}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEquals<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func, 
    TOther other,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (!EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
    {
      return new Result<TValue>(new ArgumentException($"Value should be equal to {other}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfEquals<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    TOther other,
    string errorMessage,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfNotEquals<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    TOther other,
    string errorMessage,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (!EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfDefault<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    return IfEquals(validatable, func, default!, "Value should not be default.", funcName);
  }

  public static Result<TValue> IfNotDefault<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    return IfNotEquals(validatable, func, default!, "Value should be default.", funcName);
  }

  public static Result<TValue> IfNull<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (func(validatable.Value) is null)
    {
      return new Result<TValue>(new ArgumentException("Value cannot be null.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotNull<TValue, TOther>(
    this Validatable<TValue> validatable,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (func(validatable.Value) is not null)
    {
      return new Result<TValue>(new ArgumentException("Value should be null.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
}