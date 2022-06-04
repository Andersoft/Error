using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Strings;

public static class ValidatableStringPropertyExtensions
{
  public static Result<TValue> IfNullOrEmpty<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, string> propertyFunc, 
    [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (string.IsNullOrEmpty(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException("String should not be null or empty.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNullOrWhiteSpace<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (string.IsNullOrWhiteSpace(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException("String should not be null or whitespace.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfEndsWith<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, string> propertyFunc,
    string otherString,
    StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).EndsWith(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should not end with '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEndsWith<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, string> propertyFunc,
    string otherString,
    StringComparison comparisonType = StringComparison.Ordinal,
    [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (!propertyFunc(validatable.Value).EndsWith(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should end with '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfLongerThan<TValue>(
    this Validatable<TValue> validatable,
    Func<TValue, string> propertyFunc, 
    int length,
    [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Length > length)
    {
      return new Result<TValue>(new ArgumentException($"String should not be longer than {length} characters.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfShorterThan<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Length < length)
    {
      return new Result<TValue>(new ArgumentException($"String should not be shorter than {length} characters.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfLengthEquals<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Length == length)
    {
      return new Result<TValue>(new ArgumentException($"String length should not be equal to {length}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfLengthNotEquals<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, int length, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Length != length)
    {
      return new Result<TValue>(new ArgumentException($"String length should be equal to {length}.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfWhiteSpace<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).All(char.IsWhiteSpace))
    {
      return new Result<TValue>(new ArgumentException("String should not be white space only.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfEmpty<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Length == 0)
    {
      return new Result<TValue>(new ArgumentException("String should not be empty.", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfEqualsIgnoreCase<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    return IfEquals(validatable, propertyFunc, otherString, StringComparison.OrdinalIgnoreCase, funcName);
  }

  public static Result<TValue> IfNotEqualsIgnoreCase<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    return IfNotEquals(validatable, propertyFunc, otherString, StringComparison.OrdinalIgnoreCase, funcName);
  }

  public static Result<TValue> IfEquals<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (string.Equals(propertyFunc(validatable.Value), otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should not be equal to '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotEquals<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (!string.Equals(propertyFunc(validatable.Value), otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should be equal to '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfStartsWith<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).StartsWith(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should not start with '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotStartsWith<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (!propertyFunc(validatable.Value).StartsWith(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should start with '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfContains<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (propertyFunc(validatable.Value).Contains(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should not contain '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
  public static Result<TValue> IfNotContains<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string otherString, StringComparison comparisonType = StringComparison.Ordinal, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (!propertyFunc(validatable.Value).Contains(otherString, comparisonType))
    {
      return new Result<TValue>(new ArgumentException($"String should contain '{otherString}' (comparison type: '{comparisonType}').", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfMatches<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, Regex regex, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (regex.IsMatch(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException($"String should not match RegEx pattern '{regex}'", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotMatches<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, Regex regex, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    if (!regex.IsMatch(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException($"String should match RegEx pattern '{regex}'", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfMatches<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string regexPattern, RegexOptions regexOptions, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    var regex = new Regex(regexPattern, regexOptions);

    if (regex.IsMatch(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException($"String should not match RegEx pattern '{regex}'", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }

  public static Result<TValue> IfNotMatches<TValue>(this Validatable<TValue> validatable, Func<TValue, string> propertyFunc, string regexPattern, RegexOptions regexOptions, [CallerArgumentExpression("propertyFunc")] string? funcName = null)
  {
    var regex = new Regex(regexPattern, regexOptions);

    if (!regex.IsMatch(propertyFunc(validatable.Value)))
    {
      return new Result<TValue>(new ArgumentException($"String should match RegEx pattern '{regex}'", $"{validatable.ParamName}: {funcName}"));
    }

    return validatable.Value;
  }
}