using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.DateTimeValidators;

public static class ValidatableDateTimePropertyExtensions
{
  public static Result<Validatable<TValue>> IfDateTimeKind<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, DateTime> func,
    DateTimeKind kind,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Kind == kind)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Value should not be {kind}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfDateTimeNotKind<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, DateTime> func,
    DateTimeKind kind,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Kind != kind)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Value should be {kind}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfUtc<TValue>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, DateTime> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return IfDateTimeKind(result, func, DateTimeKind.Utc, funcName);
  }

  public static Result<Validatable<TValue>> IfNotUtc<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, DateTime> func,
    [CallerArgumentExpression("func")] string? funcName = null)
    where TValue : notnull
  {
    return IfDateTimeNotKind(result, func, DateTimeKind.Utc, funcName);
  }
}