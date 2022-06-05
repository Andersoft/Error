using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Error.Validators;

public static class Guard
{
  public static IReadOnlyList<Exception> Inspect<T1>(Result<T1> left)
  {
    List<Exception> errors = new List<Exception>();
    left.IfFail(error => errors.Add(error));
    return errors;
  }

  public static IReadOnlyList<Exception> And<T1, T2>(
    this Result<T1> left,
    Result<T2> right)
  {
    List<Exception> errors = new List<Exception>();

    left.IfFail(error => errors.Add(error));
    right.IfFail(error => errors.Add(error));
    return errors;
  }

  public static IReadOnlyList<Exception> And<T1>(
    this IReadOnlyList<Exception> left,
    Result<T1> right)
  {
    List<Exception> errors = new List<Exception>(left);
    right.IfFail(error => errors.Add(error));
    return errors;
  }
  public static void ThrowOnError(this IReadOnlyList<Exception> errors)
  {
    if (errors.Any())
    {
      throw new AggregateException(errors);
    }
  }

  public static Result<Unit> AggregateErrors(this IReadOnlyList<Exception> errors)
  {
    if (errors.Any())
    {
      return new Result<Unit>(new AggregateException(errors));
    }

    return Unit.Default;
  }
}