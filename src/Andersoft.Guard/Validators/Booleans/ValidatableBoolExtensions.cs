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
      public static Result<Unit> IfTrue(this Validatable<bool> validatable)
      {
        if (validatable.Value)
        {
          return new Result<Unit>(new ArgumentException("Value should be false.", validatable.ParamName));
        }

        return Unit.Default;
      }
      public static Result<Unit> IfFalse(this Validatable<bool> validatable)
      {
        if (!validatable.Value)
        {
          return new Result<Unit>(new ArgumentException("Value should be true.", validatable.ParamName));
        }

        return Unit.Default;
      }

      public static Result<Unit> IfFalse<TValue>(
        this Validatable<TValue> validatable, 
        Func<TValue, bool> func,
        [CallerArgumentExpression("func")] string? funcName = null)
      {
        if (!func(validatable.Value))
        {
          return new Result<Unit>(new ArgumentException($"Value should meet condition (condition: '{funcName}').", validatable.ParamName));
        }

        return Unit.Default;
      }

      public static Result<Unit> IfTrue<TValue>(
        this Validatable<TValue> validatable,
        Func<TValue, bool> func,
        [CallerArgumentExpression("func")] string? funcName = null)
      {
        if (func(validatable.Value))
        {
          return new Result<Unit>(new ArgumentException($"Value should not meet condition (condition: '{funcName}').", validatable.ParamName));
        }

        return Unit.Default;
      }

  }
}
