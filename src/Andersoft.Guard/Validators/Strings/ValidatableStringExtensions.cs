using Andersoft.Guard.Validators;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Strings;

public static class ValidatableStringExtensions
{

  public static Result<Unit> IfLongerThan(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length > length)
    {
      return new Result<Unit>(new ArgumentException($"String should not be longer than {length} characters.", validatable.ParamName));
    }

    return Unit.Default;
  }

  public static Result<Unit> IfShorterThan(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length < length)
    {
      return new Result<Unit>(new ArgumentException($"String should not be shorter than {length} characters.", validatable.ParamName));
    }

    return Unit.Default;
  }
  public static Result<Unit> IfLengthEquals(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length == length)
    {
      return new Result<Unit>(new ArgumentException($"String length should not be equal to {length}.", validatable.ParamName));
    }

    return Unit.Default;
  }
  public static Result<Unit> IfLengthNotEquals(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length != length)
    {
      return new Result<Unit>(new ArgumentException($"String length should be equal to {length}.", validatable.ParamName));
    }

    return Unit.Default;
  }

  public static Result<Unit> IfWhiteSpace(this Validatable<string> validatable)
  {
    if (validatable.Value.All(char.IsWhiteSpace))
    {
      return new Result<Unit>(new ArgumentException("String should not be white space only.", validatable.ParamName));
    }

    return Unit.Default;
  }

  public static Result<Unit> IfEmpty(this Validatable<string> validatable)
  {
    if (validatable.Value.Length == 0)
    {
      return new Result<Unit>(new ArgumentException("String should not be empty.", validatable.ParamName));
    }

    return Unit.Default;
  }
  public static Result<Unit> IfEqualsIgnoreCase(this Validatable<string> validatable, string otherString)
  {
    return IfEquals(validatable, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<Unit> IfNotEqualsIgnoreCase(this Validatable<string> validatable, string otherString)
  {
    return IfNotEquals(validatable, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<Unit> IfEquals(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (string.Equals(validatable.Value, otherString, comparisonType))
    {
      return new Result<Unit>(new ArgumentException($"String should not be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return Unit.Default;
  }

  public static Result<Unit> IfNotEquals(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (!string.Equals(validatable.Value, otherString, comparisonType))
    {
      return new Result<Unit>(new ArgumentException($"String should be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return Unit.Default;
  }
}