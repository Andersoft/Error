using LanguageExt.Common;

namespace Andersoft.Error.Validators.Comparable;

public static class ValidatableComparableExtensions
{

  public static Result<Validatable<TValue>> IfGreaterThan<TValue>(this Result<Validatable<TValue>> result, TValue other)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TValue>.Default.Compare(validatable.Value, other) > 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          validatable.ParamName,
          validatable.Value,
          $"Value should not be greater than {other}."));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfLessThan<TValue>(this Result<Validatable<TValue>> result, TValue other)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TValue>.Default.Compare(validatable.Value, other) < 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          validatable.ParamName,
          validatable.Value,
          $"Value should not be less than {other}."));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfPositive<TValue>(this Result<Validatable<TValue>> result)
    where TValue : notnull
  {
    return IfGreaterThan(result, default!);
  }

  public static Result<Validatable<TValue>> IfNegative<TValue>(this Result<Validatable<TValue>> result)
    where TValue : notnull
  {
    return IfLessThan(result, default!);
  }

  public static Result<Validatable<TValue>> IfOutOfRange<TValue>(
    this Result<Validatable<TValue>> result,
    TValue min,
    TValue max) where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (Comparer<TValue>.Default.Compare(validatable.Value, min) < 0 ||
          Comparer<TValue>.Default.Compare(validatable.Value, max) > 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          validatable.ParamName,
          validatable.Value,
          $"Value should be between {min} and {max}."));
      }

      return validatable;
    }
  }


}