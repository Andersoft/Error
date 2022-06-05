using System.Text.RegularExpressions;
using LanguageExt.Common;

namespace Andersoft.Guard.Validators.Strings;

public static class ValidatableStringExtensions
{

  public static Result<Validatable<string>> IfLongerThan(this Result<Validatable<string>> result, int length)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Length > length)
      {
        return new Result<Validatable<string>>(
          new ArgumentException($"String should not be longer than {length} characters.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfShorterThan(this Result<Validatable<string>> result, int length)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Length < length)
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not be shorter than {length} characters.", validatable.ParamName));
      }

      return validatable;
    }
  }
  public static Result<Validatable<string>> IfLengthEquals(this Result<Validatable<string>> result, int length)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Length == length)
      {
        return new Result<Validatable<string>>(new ArgumentException($"String length should not be equal to {length}.", validatable.ParamName));
      }

      return validatable;
    }
  }
  public static Result<Validatable<string>> IfLengthNotEquals(this Result<Validatable<string>> result, int length)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Length != length)
      {
        return new Result<Validatable<string>>(new ArgumentException($"String length should be equal to {length}.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfWhiteSpace(this Result<Validatable<string>> result)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.All(char.IsWhiteSpace))
      {
        return new Result<Validatable<string>>(new ArgumentException("String should not be white space only.", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfEmpty(this Result<Validatable<string>> result)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Length == 0)
      {
        return new Result<Validatable<string>>(new ArgumentException("String should not be empty.", validatable.ParamName));
      }

      return validatable;
    }
  }
  public static Result<Validatable<string>> IfEqualsIgnoreCase(this Result<Validatable<string>> result, string otherString)
  {
    return IfEquals(result, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<Validatable<string>> IfNotEqualsIgnoreCase(this Result<Validatable<string>> result, string otherString)
  {
    return IfNotEquals(result, otherString, StringComparison.OrdinalIgnoreCase);
  }

  public static Result<Validatable<string>> IfEquals(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (string.Equals(validatable.Value, otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfNotEquals(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (!string.Equals(validatable.Value, otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should be equal to '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfEndsWith(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.EndsWith(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not end with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfNotEndsWith(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (!validatable.Value.EndsWith(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should end with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfStartsWith(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.StartsWith(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not start with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfNotStartsWith(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (!validatable.Value.StartsWith(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should start with '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }
  public static Result<Validatable<string>> IfContains(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (validatable.Value.Contains(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not contain '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }
  public static Result<Validatable<string>> IfNotContains(this Result<Validatable<string>> result, string otherString, StringComparison comparisonType = StringComparison.Ordinal)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (!validatable.Value.Contains(otherString, comparisonType))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should contain '{otherString}' (comparison type: '{comparisonType}').", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfMatches(this Result<Validatable<string>> result, Regex regex)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (regex.IsMatch(validatable.Value))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not match RegEx pattern '{regex}'", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfNotMatches(this Result<Validatable<string>> result, Regex regex)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      if (!regex.IsMatch(validatable.Value))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should match RegEx pattern '{regex}'", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfMatches(this Result<Validatable<string>> result, string regexPattern, RegexOptions regexOptions)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      var regex = new Regex(regexPattern, regexOptions);

      if (regex.IsMatch(validatable.Value))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should not match RegEx pattern '{regex}'", validatable.ParamName));
      }

      return validatable;
    }
  }

  public static Result<Validatable<string>> IfNotMatches(this Result<Validatable<string>> result, string regexPattern, RegexOptions regexOptions)
  {
    return result.Match(Validate, error => new(error));

    Result<Validatable<string>> Validate(Validatable<string> validatable)
    {
      var regex = new Regex(regexPattern, regexOptions);

      if (!regex.IsMatch(validatable.Value))
      {
        return new Result<Validatable<string>>(new ArgumentException($"String should match RegEx pattern '{regex}'", validatable.ParamName));
      }

      return validatable;
    }
  }
}