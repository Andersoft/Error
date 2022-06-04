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
}