using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Equalities;

public static class ValidatableEqualitiesExtensions
{
  public static Result<Validatable<TValue>> IfEquals<TValue>(this Result<Validatable<TValue>> result, TValue other)
    where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Value should not be equal to {other}.",
          validatable.ParamName));
      }

      return validatable;
    }
  }

  private static Result<Validatable<TValue>> IfEquals<TValue>(this Result<Validatable<TValue>> result, TValue other, string errorMessage)
    where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(errorMessage, validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotEquals<TValue>(this Result<Validatable<TValue>> result, TValue other)
    where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Value should be equal to {other}.",
          validatable.ParamName));
      }

      return validatable;
    }
  }

  private static Result<Validatable<TValue>> IfNotEquals<TValue>(this Result<Validatable<TValue>> result, TValue other, string errorMessage)
    where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(errorMessage, validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfDefault<TValue>(this Result<Validatable<TValue>> result)
    where TValue : notnull
  {
    return IfEquals(result, default!, "Value should not be default.");
  }

  public static Result<Validatable<TValue>> IfNotDefault<TValue>(this Result<Validatable<TValue>> result)
    where TValue : notnull
  {
    return IfNotEquals(result, default!, "Value should be default.");
  }
}