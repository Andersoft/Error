using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Comparable;

public static class ValidatableComparablePropertiesExtensions
{

  public static Result<Validatable<TValue>> IfGreaterThan<TValue, TProperty>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, TProperty> func, 
    TProperty other, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TProperty>.Default.Compare(func(validatable.Value), other) > 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          $"{validatable.ParamName}: {funcProperty}",
          func(validatable.Value),
          $"Value should not be greater than {other}."));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfLessThan<TValue, TProperty>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, TProperty> func, 
    TProperty other, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TProperty>.Default.Compare(func(validatable.Value), other) < 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          $"{validatable.ParamName}: {funcProperty}",
          func(validatable.Value),
          $"Value should not be less than {other}."));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfPositive<TValue, TProperty>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, TProperty> func, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return IfGreaterThan(result, func, default!, funcProperty);
  }

  public static Result<Validatable<TValue>> IfNegative<TValue, TProperty>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, TProperty> func, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return IfLessThan(result, func, default!, funcProperty);
  }

  public static Result<Validatable<TValue>> IfOutOfRange<TValue, TProperty>(
    this Result<Validatable<TValue>> result, 
    Func<TValue, TProperty> func,
    TProperty min,
    TProperty max,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TProperty>.Default.Compare(func(validatable.Value), min) < 0 ||
          Comparer<TProperty>.Default.Compare(func(validatable.Value), max) > 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          $"{validatable.ParamName}: {funcProperty}",
          func(validatable.Value),
          $"Value should be between {min} and {max}."));
      }

      return validatable;
    }
  }
}