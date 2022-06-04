using System.Net.Http.Headers;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.DateTimeValidators;

public static class ValidatableDateTimeExtensions
{
  public static Result<DateTime> IfDateTimeKind(this Validatable<DateTime> validatable, DateTimeKind kind)
  {
    if (validatable.Value.Kind == kind)
    {
      return new Result<DateTime>(new ArgumentException($"Value should not be {kind}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<DateTime> IfDateTimeNotKind(this Validatable<DateTime> validatable, DateTimeKind kind)
  {
    if (validatable.Value.Kind != kind)
    {
      return new Result<DateTime>(new ArgumentException($"Value should be {kind}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<DateTime> IfUtc(this Validatable<DateTime> validatable)
  {
    return IfDateTimeKind(validatable, DateTimeKind.Utc);
  }

  public static Result<DateTime> IfNotUtc(this Validatable<DateTime> validatable)
  {
    return IfDateTimeNotKind(validatable, DateTimeKind.Utc);
  }
}