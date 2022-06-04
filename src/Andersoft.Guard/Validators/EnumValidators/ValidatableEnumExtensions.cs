using LanguageExt.Common;

namespace Andersoft.Guard.Validators.EnumValidators;

public static class ValidatableEnumExtensions
{
  public static Result<TValue> IfOutOfRange<TValue>(this Validatable<TValue> validatable)
  where TValue : struct, Enum
  {
    if (!Enum.IsDefined(validatable.Value))
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        validatable.ParamName,
        validatable.Value,
        "Value should be defined in enum."));
    }

    return validatable.Value;
  }
}