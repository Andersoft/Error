using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Comparable;

public static class ValidatableComparablePropertiesExtensions
{

  public static Result<TValue> IfGreaterThan<TValue, TProperty>(
    this Validatable<TValue> validatable, 
    Func<TValue, TProperty> func, 
    TProperty other, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (Comparer<TProperty>.Default.Compare(func(validatable.Value), other) > 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        $"{validatable.ParamName}: {funcProperty}",
        func(validatable.Value),
        $"Value should not be greater than {other}."));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfLessThan<TValue, TProperty>(
    this Validatable<TValue> validatable, 
    Func<TValue, TProperty> func, 
    TProperty other, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (Comparer<TProperty>.Default.Compare(func(validatable.Value), other) < 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        $"{validatable.ParamName}: {funcProperty}",
        func(validatable.Value),
        $"Value should not be less than {other}."));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfPositive<TValue, TProperty>(
    this Validatable<TValue> validatable, 
    Func<TValue, TProperty> func, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return IfGreaterThan(validatable, func, default!, funcProperty);
  }

  public static Result<TValue> IfNegative<TValue, TProperty>(
    this Validatable<TValue> validatable, 
    Func<TValue, TProperty> func, 
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    return IfLessThan(validatable, func, default!, funcProperty);
  }

  public static Result<TValue> IfOutOfRange<TValue, TProperty>(
    this Validatable<TValue> validatable, 
    Func<TValue, TProperty> func,
    TProperty min,
    TProperty max,
    [CallerArgumentExpression("func")] string? funcProperty = null) where TValue : notnull
  {
    if (Comparer<TProperty>.Default.Compare(func(validatable.Value), min) < 0 || Comparer<TProperty>.Default.Compare(func(validatable.Value), max) > 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        $"{validatable.ParamName}: {funcProperty}",
        func(validatable.Value),
        $"Value should be between {min} and {max}."));
    }

    return validatable.Value;
  }


}