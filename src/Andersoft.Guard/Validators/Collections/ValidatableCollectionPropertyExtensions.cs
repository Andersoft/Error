using System.Runtime.CompilerServices;
using LanguageExt.Common;

namespace Andersoft.Error.Validators.Collections;

public static class ValidatableCollectionPropertyExtensions
{
  public static Result<Validatable<TValue>> IfEmpty<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length == 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentException("Collection should not be empty.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotEmpty<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length != 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentException("Collection should be empty.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfCountEquals<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length == count)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Collection count should not be equal to {count}.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfCountNotEquals<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length != count)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Collection count should be equal to {count}.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfCountGreaterThan<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length > count)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"Collection count should not be greater than {count}.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfCountLessThan<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    int count,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Length < count)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Collection count should not be less than {count}.", $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfHasNullElements<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Any(x => x == null))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"Collection should not have null elements.", $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfContains<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    TCollection needle,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (func(validatable.Value).Contains(needle))
      {
        return new Result<Validatable<TValue>>(new ArgumentException("Collection should not contain element.", $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotContains<TValue, TCollection>(
    this Result<Validatable<TValue>> result,
    Func<TValue, TCollection[]> func,
    TCollection needle,
    [CallerArgumentExpression("func")] string? funcProperty = null)
    where TValue : notnull
  {
    return result.Match(Validate, error => new Result<Validatable<TValue>>(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!func(validatable.Value).Contains(needle))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          "Collection should contain element.",
          $"{validatable.ParamName}: {funcProperty}"));
      }

      return validatable;
    }
  }
}