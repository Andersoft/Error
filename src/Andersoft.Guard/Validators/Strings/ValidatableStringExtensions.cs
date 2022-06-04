using System.Text.RegularExpressions;
using Andersoft.Guard.Validators;
using LanguageExt;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Strings;

public static class ValidatableStringExtensions
{

  public static Result<string> IfLongerThan(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length > length)
    {
      return new Result<string>(new ArgumentException($"String should not be longer than {length} characters.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfShorterThan(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length < length)
    {
      return new Result<string>(new ArgumentException($"String should not be shorter than {length} characters.", validatable.ParamName));
    }

    return validatable.Value;
  }
  public static Result<string> IfLengthEquals(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length == length)
    {
      return new Result<string>(new ArgumentException($"String length should not be equal to {length}.", validatable.ParamName));
    }

    return validatable.Value;
  }
  public static Result<string> IfLengthNotEquals(this Validatable<string> validatable, int length)
  {
    if (validatable.Value.Length != length)
    {
      return new Result<string>(new ArgumentException($"String length should be equal to {length}.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfWhiteSpace(this Validatable<string> validatable)
  {
    if (validatable.Value.All(char.IsWhiteSpace))
    {
      return new Result<string>(new ArgumentException("String should not be white space only.", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfEmpty(this Validatable<string> validatable)
  {
    if (validatable.Value.Length == 0)
    {
      return new Result<string>(new ArgumentException("String should not be empty.", validatable.ParamName));
    }

    return validatable.Value;
  }
  public static Result<string> IfEqualsIgnoreCase(this Validatable<string> validatable, string otherString)
  {
    return IfEquals(validatable, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<string> IfNotEqualsIgnoreCase(this Validatable<string> validatable, string otherString)
  {
    return IfNotEquals(validatable, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<string> IfEquals(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (string.Equals(validatable.Value, otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should not be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfNotEquals(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (!string.Equals(validatable.Value, otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfEndsWith(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (validatable.Value.EndsWith(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should not end with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfNotEndsWith(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (!validatable.Value.EndsWith(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should end with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfStartsWith(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (validatable.Value.StartsWith(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should not start with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfNotStartsWith(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (!validatable.Value.StartsWith(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should start with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }
  public static Result<string> IfContains(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (validatable.Value.Contains(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should not contain '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }
  public static Result<string> IfNotContains(this Validatable<string> validatable, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    if (!validatable.Value.Contains(otherString, comparisonType))
    {
      return new Result<string>(new ArgumentException($"String should contain '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfMatches(this Validatable<string> validatable, Regex regex)
  {
    if (regex.IsMatch(validatable.Value))
    {
      return new Result<string>(new ArgumentException($"String should not match RegEx pattern '{regex}'", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfNotMatches(this Validatable<string> validatable, Regex regex)
  {
    if (!regex.IsMatch(validatable.Value))
    {
      return new Result<string>(new ArgumentException($"String should match RegEx pattern '{regex}'", validatable.ParamName));
    }

    return validatable.Value;
  }
  
  public static Result<string> IfMatches(this Validatable<string> validatable, string regexPattern, RegexOptions regexOptions)
  {
    var regex = new Regex(regexPattern, regexOptions);

    if (regex.IsMatch(validatable.Value))
    {
      return new Result<string>(new ArgumentException($"String should not match RegEx pattern '{regex}'", validatable.ParamName));
    }

    return validatable.Value;
  }

  public static Result<string> IfNotMatches(this Validatable<string> validatable, string regexPattern, RegexOptions regexOptions)
  {
    var regex = new Regex(regexPattern, regexOptions);

    if (!regex.IsMatch(validatable.Value))
    {
      return new Result<string>(new ArgumentException($"String should match RegEx pattern '{regex}'", validatable.ParamName));
    }

    return validatable.Value;
  }
}