using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.EnumValidators;

public static class ValidatableEnumPropertyExtensions
{
  public static Result<Validatable<TValue>> IfOutOfRange<TValue, TProperty>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TProperty> func,
    [CallerArgumentExpression("func")]string? funcName = null) where TValue : notnull
    where TProperty : struct, Enum
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!Enum.IsDefined(func(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          $"{validatable.ParamName}: {funcName}",
          func(validatable.Value),
          "Value should be defined in enum."));
      }

      return validatable;
    }
  }
}