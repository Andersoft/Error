using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Strings;

public static class ValidatableStringPropertyExtensions
{
  public static Result<Validatable<TValue>> IfNullOrEmpty<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, string> propertyFunc, 
    [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (string.IsNullOrEmpty(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException("String should not be null or empty.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNullOrWhiteSpace<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (string.IsNullOrWhiteSpace(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException("String should not be null or whitespace.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfEndsWith<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, string> propertyFunc,
    string otherString,
    StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).EndsWith(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not end with '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotEndsWith<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, string> propertyFunc,
    string otherString,
    StringComparison comparisonType = StringComparison.Ordinal,
    [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!propertyFunc(validatable.Value).EndsWith(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should end with '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfLongerThan<TValue>(
    this Result<Validatable<TValue>> result,
    Func<TValue, string> propertyFunc, 
    int length,
    [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Length > length)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not be longer than {length} characters.", $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfShorterThan<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Length < length)
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not be shorter than {length} characters.", $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
  public static Result<Validatable<TValue>> IfLengthEquals<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Length == length)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String length should not be equal to {length}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
  public static Result<Validatable<TValue>> IfLengthNotEquals<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Length != length)
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String length should be equal to {length}.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfWhiteSpace<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).All(char.IsWhiteSpace))
      {
        return new Result<Validatable<TValue>>(new ArgumentException("String should not be white space only.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfEmpty<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Length == 0)
      {
        return new Result<Validatable<TValue>>(new ArgumentException("String should not be empty.",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
  public static Result<Validatable<TValue>> IfEqualsIgnoreCase<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return IfEquals(result, propertyFunc, otherString, StringComparison.OrdinalIgnoreCase, funcName);
  }

  public static Result<Validatable<TValue>> IfNotEqualsIgnoreCase<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return IfNotEquals(result, propertyFunc, otherString, StringComparison.OrdinalIgnoreCase, funcName);
  }

  public static Result<Validatable<TValue>> IfEquals<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (string.Equals(propertyFunc(validatable.Value), otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not be equal to '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotEquals<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!string.Equals(propertyFunc(validatable.Value), otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should be equal to '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfStartsWith<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).StartsWith(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not start with '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotStartsWith<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!propertyFunc(validatable.Value).StartsWith(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should start with '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
  public static Result<Validatable<TValue>> IfContains<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (propertyFunc(validatable.Value).Contains(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should not contain '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
  public static Result<Validatable<TValue>> IfNotContains<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!propertyFunc(validatable.Value).Contains(otherString, comparisonType))
      {
        return new Result<Validatable<TValue>>(new ArgumentException(
          $"String should contain '{otherString}' (comparison type: '{comparisonType}').",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfMatches<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, Regex regex, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (regex.IsMatch(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String should not match RegEx pattern '{regex}'",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotMatches<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, Regex regex, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      if (!regex.IsMatch(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String should match RegEx pattern '{regex}'",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfMatches<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string regexPattern, RegexOptions regexOptions, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      var regex = new Regex(regexPattern, regexOptions);

      if (regex.IsMatch(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String should not match RegEx pattern '{regex}'",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }

  public static Result<Validatable<TValue>> IfNotMatches<TValue>(this Result<Validatable<TValue>> result, Func<TValue, string> propertyFunc, string regexPattern, RegexOptions regexOptions, [CallerArgumentExpression("propertyFunc")] string? funcName = null) where TValue : notnull
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<TValue>> Validate(Validatable<TValue> validatable)
    {
      var regex = new Regex(regexPattern, regexOptions);

      if (!regex.IsMatch(propertyFunc(validatable.Value)))
      {
        return new Result<Validatable<TValue>>(new ArgumentException($"String should match RegEx pattern '{regex}'",
          $"{validatable.ParamName}: {funcName}"));
      }

      return validatable;
    }
  }
}