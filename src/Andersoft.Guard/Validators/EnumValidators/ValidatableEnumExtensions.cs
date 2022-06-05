﻿using LanguageExt.Common;

namespace Andersoft.Guard.Validators.EnumValidators;

public static class ValidatableEnumExtensions
{
  public static Result<Validatable<TValue>> IfOutOfRange<TValue>(this Result<Validatable<TValue>> result)
  where TValue : struct, Enum
  {
    return result.Match(Validate, error => new (error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!Enum.IsDefined(validatable.Value))
      {
        return new Result<Validatable<TValue>>(new ArgumentOutOfRangeException(
          validatable.ParamName,
          validatable.Value,
          "Value should be defined in enum."));
      }

      return validatable;
    }
  }
}