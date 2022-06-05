using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators;

public static class ValidatableExtensions
{
  public static Validatable<TValue> Error<TValue>(this TValue value,
    [CallerArgumentExpression("value")] string? paramName = null)
    where TValue : notnull
  {
    return new(value, paramName!);
  }

  public static Result<Validatable<TValue>> ErrorIfNull<TValue>(
    this TValue? value,
    [CallerArgumentExpression("value")] string? paramName = null)
    where TValue : notnull
  {
    if (value == null)
    {
      return new Result<Validatable<TValue>>(new ArgumentException("Value cannot be null.", paramName));
      
    }

    return new Validatable<TValue>(value, paramName!);
  }
}