using System.Text.RegularExpressions;
using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.UnitTests.Validators.StringProperties;


public class StringPropertiesRegexTests
{
    
    [TestCase("Anderson", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Haman", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("My Name", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyMatches_GivenPropertyDoesMatchesRegexPattern_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfMatches(p => p.Name, regexPattern, regexOptions)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not match RegEx pattern '{regexPattern}' (Parameter '{nameof(person)}: p => p.Name')");
    }

    
    [TestCase("123456789", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Anderson", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("No Match", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyMatches_GivenPropertyDoesNotMatchesRegexPattern_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfMatches(p => p.Name, regexPattern, regexOptions)
          .Match(unit => unit, error => default(Unit?));

        // Assert
        result.Should().BeOfType<Unit>();
}

    
    [TestCase("Anderson", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Haman", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("My Name", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyMatches_GivenPropertyDoesMatchesRegex_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        var result = person.Error().IfMatches(p => p.Name, regex)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not match RegEx pattern '{regex}' (Parameter '{nameof(person)}: p => p.Name')");
    }

    
    [TestCase("123456789", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Anderson", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("No Match", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyMatches_GivenPropertyDoesNotMatchesRegex_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        var result = person.Error().IfMatches(p => p.Name, regex)
          .Match(unit => unit, error => default(Unit?));

        // Assert
        result.Should().BeOfType<Unit>();
}

    
    [TestCase("123456789", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Anderson", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("No Match", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyNotMatches_GivenPropertyDoesNotMatchesRegexPattern_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotMatches(p => p.Name, regexPattern, regexOptions)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should match RegEx pattern '{regexPattern}' (Parameter '{nameof(person)}: p => p.Name')");
    }

    
    [TestCase("Anderson", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Haman", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("My Name", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyNotMatches_GivenPropertyDoesMatchesRegexPattern_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotMatches(p => p.Name, regexPattern, regexOptions)
          .Match(unit => unit, error => default(Unit?));

        // Assert
        result.Should().BeOfType<Unit>();
}

    
    [TestCase("123456789", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Anderson", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("No Match", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyNotMatches_GivenPropertyDoesNotMatchesRegex_ThenShouldReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        var result = person.Error().IfNotMatches(p => p.Name, regex)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should match RegEx pattern '{regex}' (Parameter '{nameof(person)}: p => p.Name')");
    }

    
    [TestCase("Anderson", @"[a-zA-Z]", RegexOptions.None)]
    [TestCase("Haman", @"\b[H]\w+", RegexOptions.None)]
    [TestCase("My Name", @"\bname\b", RegexOptions.IgnoreCase)]
    public void WhenCheckingIfPropertyNotMatches_GivenPropertyDoesMatchesRegex_ThenShouldNotReturnError(string value, string regexPattern, RegexOptions regexOptions)
    {
        // Arrange
        var person = new { Name = value };
        var regex = new Regex(regexPattern, regexOptions);

        // Act
        var result = person.Error().IfNotMatches(p => p.Name, regex)
          .Match(unit => unit, error => default(Unit?));

        // Assert
        result.Should().BeOfType<Unit>();
}
}