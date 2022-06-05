using LanguageExt.Common;

namespace Andersoft.Error.Validators.DateTimeValidators;

public static class ValidatableDateTimeExtensions
{
  public static Result<Validatable<DateTime>> IfDateTimeKind(this Result<Validatable<DateTime>> result, DateTimeKind kind)
  {
    return result.Match(Validate, error => new Result<Validatable<DateTime>>(error));

    Result<Validatable<DateTime>> Validate(Validatable<DateTime> validatable)
    {
      if (validatable.Value.Kind == kind)
      {
        return new Result<Validatable<DateTime>>(new ArgumentException($"Value should not be {kind}.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<DateTime>> IfDateTimeNotKind(this Result<Validatable<DateTime>> result, DateTimeKind kind)
  {
    return result.Match(Validate, error => new Result<Validatable<DateTime>>(error));

    Result<Validatable<DateTime>> Validate(Validatable<DateTime> validatable)
    {
      if (validatable.Value.Kind != kind)
      {
        return new Result<Validatable<DateTime>>(new ArgumentException($"Value should be {kind}.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<DateTime>> IfUtc(this Result<Validatable<DateTime>> result)
  {
    return IfDateTimeKind(result, DateTimeKind.Utc);
  }

  public static Result<Validatable<DateTime>> IfNotUtc(this Result<Validatable<DateTime>> result)
  {
    return IfDateTimeNotKind(result, DateTimeKind.Utc);
  }
}