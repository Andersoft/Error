using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.EnumValidators;

public static class ValidatableEnumPropertyExtensions
{
  public static Result<TValue> IfOutOfRange<TValue, TProperty>(
    this Validatable<TValue> validatable,
    Func<TValue, TProperty> func,
    [CallerArgumentExpression("func")]string? funcName = null)
    where TProperty : struct, Enum
  {
    if (!Enum.IsDefined(func(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentOutOfRangeException(
        $"{validatable.ParamName}: {funcName}",
        func(validatable.Value),
        "Value should be defined in enum."));
    }

    return validatable.Value;
  }
}