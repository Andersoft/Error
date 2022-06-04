using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Collections
{
  public static class ValidatableCollectionExtensions
  {
    public static Result<TValue[]> IfEmpty<TValue>(this Validatable<TValue[]> validatable)
    {
      if (validatable.Value.Length == 0)
      {
        return new Result<TValue[]>(new ArgumentException("Collection should not be empty.", validatable.ParamName));
      }

      return validatable.Value;
    }

    public static Result<TValue[]> IfNotEmpty<TValue>(this Validatable<TValue[]> validatable)
    {
      if (validatable.Value.Length != 0)
      {
        return new Result<TValue[]>(new ArgumentException("Collection should be empty.", validatable.ParamName));
      }

      return validatable.Value;
    }
    public static Result<TValue[]> IfCountEquals<TValue>(this Validatable<TValue[]> validatable, int count)
    {
      if (validatable.Value.Length == count)
      {
        return new Result<TValue[]>(new ArgumentException($"Collection count should not be equal to {count}.", validatable.ParamName));
      }

      return validatable.Value;
    }

    public static Result<TValue[]> IfCountNotEquals<TValue>(this Validatable<TValue[]> validatable, int count)
    {
      if (validatable.Value.Length != count)
      {
        return new Result<TValue[]>(new ArgumentException($"Collection count should be equal to {count}.", validatable.ParamName));
      }

      return validatable.Value;
    }

    public static Result<TValue[]> IfCountGreaterThan<TValue>(this Validatable<TValue[]> validatable, int count)
    {
      if (validatable.Value.Length > count)
      {
        return new Result<TValue[]>(new ArgumentException($"Collection count should not be greater than {count}.", validatable.ParamName));
      }

      return validatable.Value;
    }
    public static Result<TValue[]> IfCountLessThan<TValue>(this Validatable<TValue[]> validatable, int count)
    {
      if (validatable.Value.Length < count)
      {
        return new Result<TValue[]>(new ArgumentException($"Collection count should not be less than {count}.", validatable.ParamName));
      }

      return validatable.Value;
    }
    public static Result<TValue[]> IfHasNullElements<TValue>(this Validatable<TValue[]> validatable)
    {
      if (validatable.Value.Any(x => x == null))
      {
        return new Result<TValue[]>(new ArgumentException($"Collection should not have null elements.", validatable.ParamName));
      }

      return validatable.Value;
    }
    public static Result<TValue[]> IfContains<TValue>(this Validatable<TValue[]> validatable, TValue needle)
    {
      if (validatable.Value.Contains(needle))
      {
        return new Result<TValue[]>(new ArgumentException("Collection should not contain element.", validatable.ParamName));
      }

      return validatable.Value;
    }

    public static Result<TValue[]> IfNotContains<TValue>(this Validatable<TValue[]> validatable, TValue needle)
    {
      if (!validatable.Value.Contains(needle))
      {
        return new Result<TValue[]>(new ArgumentException("Collection should contain element.", validatable.ParamName));
      }

      return validatable.Value;
    }
  }
}
