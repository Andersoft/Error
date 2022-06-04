using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.DateTimeValidators;

public static class ValidatableDateTimePropertyExtensions
{
  public static Result<TValue> IfDateTimeKind<TValue>(
    this Validatable<TValue> validatable, 
    Func<TValue, DateTime> func, 
    DateTimeKind kind, 
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (func(validatable.Value).Kind == kind)
    {
      return new Result<TValue>(new ArgumentException($"Value should not be {kind}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfDateTimeNotKind<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, DateTime> func,
    DateTimeKind kind,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    if (func(validatable.Value).Kind != kind)
    {
      return new Result<TValue>(new ArgumentException($"Value should be {kind}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfUtc<TValue>(
    this Validatable<TValue> validatable, 
    Func<TValue, DateTime> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    return IfDateTimeKind(validatable, func, DateTimeKind.Utc, funcName);
  }

  public static Result<TValue> IfNotUtc<TValue>(this Validatable<TValue> validatable,
    Func<TValue, DateTime> func,
    [CallerArgumentExpression("func")] string? funcName = null)
  {
    return IfDateTimeNotKind(validatable, func, DateTimeKind.Utc, funcName);
  }
}