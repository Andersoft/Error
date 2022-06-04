using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Equalities;

public static class ValidatableEqualitiesExtensions
{
  public static Result<TValue> IfEquals<TValue>(this Validatable<TValue> validatable, TValue other)
  {
    if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException($"Value should not be equal to {other}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfEquals<TValue>(this Validatable<TValue> validatable, TValue other, string errorMessage)
  {
    if (EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEquals<TValue>(this Validatable<TValue> validatable, TValue other)
  {
    if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException($"Value should be equal to {other}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  private static Result<TValue> IfNotEquals<TValue>(this Validatable<TValue> validatable, TValue other, string errorMessage)
  {
    if (!EqualityComparer<TValue>.Default.Equals(validatable.Value, other))
    {
      return new Result<TValue>(new ArgumentException(errorMessage, validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfDefault<TValue>(this Validatable<TValue> validatable)
  {
    return IfEquals(validatable, default!, "Value should not be default.");
  }

  public static Result<TValue> IfNotDefault<TValue>(this Validatable<TValue> validatable)
  {
    return IfNotEquals(validatable, default!, "Value should be default.");
  }
}