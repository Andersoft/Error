using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Collections
{
  public static class ValidatableCollectionExtensions
  {
    public static Result<Validatable<TValue[]>> IfEmpty<TValue>(this Result<Validatable<TValue[]>> result)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length == 0)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException("Collection should not be empty.", validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfNotEmpty<TValue>(this Result<Validatable<TValue[]>> result)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length != 0)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException("Collection should be empty.", validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfCountEquals<TValue>(this Result<Validatable<TValue[]>> result, int count)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length == count)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            $"Collection count should not be equal to {count}.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfCountNotEquals<TValue>(this Result<Validatable<TValue[]>> result, int count)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length != count)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            $"Collection count should be equal to {count}.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfCountGreaterThan<TValue>(this Result<Validatable<TValue[]>> result, int count)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length > count)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            $"Collection count should not be greater than {count}.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfCountLessThan<TValue>(this Result<Validatable<TValue[]>> result, int count)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Length < count)
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            $"Collection count should not be less than {count}.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfHasNullElements<TValue>(this Result<Validatable<TValue[]>> result)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Any(x => x == null))
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            "Collection should not have null elements.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfContains<TValue>(this Result<Validatable<TValue[]>> result, TValue needle)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (validatable.Value.Contains(needle))
        {
          return new Result<Validatable<TValue[]>>(new ArgumentException(
            "Collection should not contain element.",
            validatable.ParamName));
        }

        return validatable;
      }
    }

    public static Result<Validatable<TValue[]>> IfNotContains<TValue>(this Result<Validatable<TValue[]>> result, TValue needle)
    {
      return result.Match(Validate, error => new Result<Validatable<TValue[]>>(error));

      Result<Validatable<TValue[]>> Validate(Validatable<TValue[]> validatable)
      {
        if (!validatable.Value.Contains(needle))
        {
          return new Result<Validatable<TValue[]>>(
            new ArgumentException("Collection should contain element.", validatable.ParamName));
        }

        return validatable;
      }
    }
  }
}
