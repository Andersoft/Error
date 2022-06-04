using System.Runtime.CompilerServices;

namespace Andersoft.Guard.Validators;

public static class ValidatableExtensions
{
  public static Validatable<TValue> Error<TValue>(this TValue value, [CallerArgumentExpression("value")] string? paramName = null)
  {
    return new(value, paramName!);
  }
}