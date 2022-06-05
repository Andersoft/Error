using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Equalities;

public static class ValidatableEqualitiesExtensions
{
  public static Result<TValue> IfEquals<TValue>(this Validatable<TValue> validatable, TValue other)
    where TValue : notnull
  {
    if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException($"Value should not be equal to {other}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfEquals<TValue>(this Validatable<TValue> validatable, TValue other, string errorMessage)
    where TValue : notnull
  {
    if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEquals<TValue>(this Validatable<TValue> validatable, TValue other)
    where TValue : notnull
  {
    if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException($"Value should be equal to {other}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfNotEquals<TValue>(this Validatable<TValue> validatable, TValue other, string errorMessage)
    where TValue : notnull
  {
    if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfDefault<TValue>(this Validatable<TValue> validatable)
    where TValue : notnull
  {
    return IfEquals(validatable, default!, "Value should not be default.");
  }

  public static Result<TValue> IfNotDefault<TValue>(this Validatable<TValue> validatable)
    where TValue : notnull
  {
    return IfNotEquals(validatable, default!, "Value should be default.");
  }

  public static Result<TValue> IfNull<TValue>(
    this Validatable<TValue> validatable)
    where TValue : notnull
  {
    if (validatable.Value is null)
    {
      return new Result<TValue>(new ArgumentException("Value cannot be null.", validatable.ParamName));
    }

    return validatable.Value;
  }
}