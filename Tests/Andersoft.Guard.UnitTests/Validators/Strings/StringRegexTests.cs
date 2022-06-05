using System.Text.RegularExpressions;
using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.Strings;

public class StringRegexTests
{
  
  [TestCase("Anderson", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueMatchesRegexPattern_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfMatches(regexPattern, regexOptions)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Anderson", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueNotMatchesRegexPattern_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfMatches(regexPattern, regexOptions)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(value); ;
  }

  
  [TestCase("Anderson", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueMatchesRegex_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfMatches(regex)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Anderson", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueNotMatchesRegex_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfMatches(regex)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(value); ;
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Anderson", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueNotMatchesRegexPattern_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfNotMatches(regexPattern, regexOptions)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("Anderson", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueMatchesRegexPattern_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfNotMatches(regexPattern, regexOptions)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(value); ;
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Anderson", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueNotMatchesRegex_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfNotMatches(regex)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("Anderson", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueMatchesRegex_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfNotMatches(regex)
      .Match(success => success.Value, error => default!);

    // Assert
    result.Should().Be(value);
  }
}