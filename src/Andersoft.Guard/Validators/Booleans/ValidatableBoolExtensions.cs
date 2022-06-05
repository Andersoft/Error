using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Booleans
{
  public static class ValidatableBoolExtensions
  {
    public static Result<Validatable<bool>> IfTrue(this Result<Validatable<bool>> result)
    {
      return result.Match(Validate, error => new(error));

      Result<Validatable<bool>> Validate(Validatable<bool> validatable)
      {
        if (validatable.Value)
        {
          return new Result<Validatable<bool>>(new ArgumentException("Value should be false.", validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<bool>> IfFalse(this Result<Validatable<bool>> result)
    {
      return result.Match(Validate, error => new(error));

      Result<Validatable<bool>> Validate(Validatable<bool> validatable)
      {
        if (!validatable.Value)
        {
          return new Result<Validatable<bool>>(new ArgumentException("Value should be true.", validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue>> IfFalse<TValue>(
      this Result<Validatable<TValue>> result,
      Func<TValue, bool> func,
      [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
    {
      return result.Match(Validate, error => new(error));

      Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
      {
        if (!func(validatable.Value))
        {
          return new Result<Validatable<TValue>>(new ArgumentException($"Value should meet condition (condition: '{funcName}').", validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue>> IfTrue<TValue>(
      this Result<Validatable<TValue>> result,
      Func<TValue, bool> func,
      [CallerArgumentExpression("func")] string? funcName = null) where TValue : notnull
    {
      return result.Match(Validate, error => new(error));

      Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
      {
        if (func(validatable.Value))
        {
          return new Result<Validatable<TValue>>(new ArgumentException($"Value should not meet condition (condition: '{funcName}').", validatable.ParamName));
        }

        return validatable;
      }
    }

  }
}
