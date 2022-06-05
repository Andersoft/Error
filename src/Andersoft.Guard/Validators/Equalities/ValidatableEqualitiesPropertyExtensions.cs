using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Equalities;

public static class ValidatableEqualitiesPropertyExtensions
{
  public static Result<Validatable<TValue>> IfEquals<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    TOther other,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Value should not be equal to {other}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotEquals<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func, 
    TOther other,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Value should be equal to {other}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  private static Result<Validatable<TValue>> IfEquals<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    TOther other,
    string errorMessage,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          errorMessage,
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  private static Result<Validatable<TValue>> IfNotEquals<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    TOther other,
    string errorMessage,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!EqualityComparer<TOther>.Default.Equals(func(validatable.Value), other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          errorMessage,
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfDefault<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return IfEquals(result, func, default!, "Value should not be default.", funcName);
  }

  public static Result<Validatable<TValue>> IfNotDefault<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return IfNotEquals(result, func, default!, "Value should be default.", funcName);
  }

  public static Result<Validatable<TValue>> IfNull<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value) is null)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          "Value cannot be null.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotNull<TValue, TOther>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TOther> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value) is not null)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          "Value should be null.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
}