﻿using System.Text.RegularExpressions;
using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.UnitTests.Validators.Strings;

public class StringRegexTests
{
  
  [TestCase("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueMatchesRegexPattern_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfMatches(regexPattern, regexOptions)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Amichai", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueNotMatchesRegexPattern_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfMatches(regexPattern, regexOptions)
      .Match(unit => unit, error => default(Unit?));

    // Assert
    result.Should().BeOfType<Unit>(); ;
  }

  
  [TestCase("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueMatchesRegex_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
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
  [TestCase("Amichai", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfMatches_GivenValueNotMatchesRegex_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfMatches(regex)
      .Match(unit => unit, error => default(Unit?));

    // Assert
    result.Should().BeOfType<Unit>(); ;
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Amichai", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueNotMatchesRegexPattern_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfNotMatches(regexPattern, regexOptions)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueMatchesRegexPattern_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Act
    var result = value.Error().IfNotMatches(regexPattern, regexOptions)
      .Match(unit => unit, error => default(Unit?));

    // Assert
    result.Should().BeOfType<Unit>(); ;
  }

  
  [TestCase("123456789", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("Amichai", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My AGE", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueNotMatchesRegex_ShouldThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfNotMatches(regex)
      .Match(_ => null, exception => exception as ArgumentException);

    // Assert
    result!.Message.Should().Be($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(value)}')");
  }

  
  [TestCase("Amichai", @"^[a-zA-Z]+$", RegexOptions.None)]
  [TestCase("123456789", @"^[0-9]+$", RegexOptions.None)]
  [TestCase("My NAME", @"\bname\b", RegexOptions.IgnoreCase)]
  public void WhenCheckingIfNotMatches_GivenValueMatchesRegex_ShouldNotThrow(string value, string regexPattern, RegexOptions regexOptions)
  {
    // Arrange
    var regex = new Regex(regexPattern, regexOptions);

    // Act
    var result = value.Error().IfNotMatches(regex)
      .Match(unit => unit, error => default(Unit?));

    // Assert
    result.Should().BeOfType<Unit>(); ;
  }
}