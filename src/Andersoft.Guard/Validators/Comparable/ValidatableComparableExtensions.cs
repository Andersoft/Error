using System.Text.RegularExpressions;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Comparable;

public static class ValidatableComparableExtensions
{

  public static Result<TValue> IfGreaterThan<TValue>(this Validatable<TValue> validatable, TValue other)
  {
    if (Comparer<TValue>.Default.Compare(validatable.Value, other) > 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        validatable.ParamName,
        validatable.Value,
        $"Value should not be greater than {other}."));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfLessThan<TValue>(this Validatable<TValue> validatable, TValue other)
  {
    if (Comparer<TValue>.Default.Compare(validatable.Value, other) < 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        validatable.ParamName,
        validatable.Value,
        $"Value should not be less than {other}."));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfPositive<TValue>(this Validatable<TValue> validatable)
  {
    return IfGreaterThan(validatable, default!);
  }

  public static Result<TValue> IfNegative<TValue>(this Validatable<TValue> validatable)
  {
    return IfLessThan(validatable, default!);
  }

  public static Result<TValue> IfOutOfRange<TValue>(
    this Validatable<TValue> validatable,
    TValue min,
    TValue max)
  {
    if (Comparer<TValue>.Default.Compare(validatable.Value, min) < 0 || Comparer<TValue>.Default.Compare(validatable.Value, max) > 0)
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        validatable.ParamName,
        validatable.Value,
        $"Value should be between {min} and {max}."));
    }

    return validatable.Value;
  }


}