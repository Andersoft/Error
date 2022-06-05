using Andersoft.Error.Validators;
using Andersoft.Error.Validators.Strings;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.StringProperties;


public class StringPropertiesSubstringTests
{
    [Test]
    public void WhenCheckingIfPropertyEndsWith_GivenPropertyDoesNotEndsWith_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };
        // Act
        var result = person.Error().IfEndsWith(p => p.Name, "Jo")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyEndsWith_GivenPropertyDoesEndsWith_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfEndsWith(p => p.Name, "hn")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not end with 'hn' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEndsWith_GivenPropertyDoesEndsWithCustomComparisonType_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfEndsWith(p => p.Name, "HN", StringComparison.OrdinalIgnoreCase)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not end with 'HN' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotEndsWith_GivenPropertyDoesNotEndsWith_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotEndsWith(p => p.Name, "Jo")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should end with 'Jo' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotEndsWith_GivenPropertyDoesEndsWithCustomComparisonType_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotEndsWith(p => p.Name, "HN", StringComparison.OrdinalIgnoreCase)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNotEndsWith_GivenPropertyDoesEndsWith_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotEndsWith(p => p.Name, "hn")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyStartsWith_GivenPropertyDoesNotStartsWith_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfStartsWith(p => p.Name, "hh")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyStartsWith_GivenPropertyDoesStartsWith_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfStartsWith(p => p.Name, "Jo")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not start with 'Jo' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyStartsWith_GivenPropertyDoesStartsWithCustomComparisonType_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfStartsWith(p => p.Name, "JO", StringComparison.OrdinalIgnoreCase)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not start with 'JO' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotStartsWith_GivenPropertyDoesNotStartsWith_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotStartsWith(p => p.Name, "hn")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should start with 'hn' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotStartsWith_GivenPropertyDoesStartsWith_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotStartsWith(p => p.Name, "Jo")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNotStartsWith_GivenPropertyDoesStartsWithCustomComparisonType_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotStartsWith(p => p.Name, "JO", StringComparison.OrdinalIgnoreCase)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyContains_GivenPropertyDoesContains_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfContains(p => p.Name, "oh")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not contain 'oh' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyContains_GivenPropertyDoesNotContains_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfContains(p => p.Name, "Oh")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNotContains_GivenPropertyDoesNotContains_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotContains(p => p.Name, "Oh")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should contain 'Oh' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotContains_GivenPropertyDoesContains_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "John" };

        // Act
        var result = person.Error().IfNotContains(p => p.Name, "oh")
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}


    [TestCase("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0065", StringComparison.InvariantCulture)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0045", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyContains_GivenPropertyDoesContainsUsingCustomComparisonType_ThenShouldReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfContains(p => p.Name, otherValue, comparisonType)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(person)}: p => p.Name')");
    }


    [TestCase("value", "123", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0041\u0041", "\u0031", StringComparison.InvariantCulture)]
    [TestCase("AA", "\u0031", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyContains_GivenPropertyDoesNotContainsUsingCustomComparisonType_ThenShouldNotReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfContains(p => p.Name, otherValue, comparisonType)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}


    [TestCase("value", "123", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0041\u0041", "\u0031", StringComparison.InvariantCulture)]
    [TestCase("AA", "\u0031", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyNotContains_GivenPropertyDoesNotContainsUsingCustomComparisonType_ThenShouldReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotContains(p => p.Name, otherValue, comparisonType)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should contain '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(person)}: p => p.Name')");
    }


    [TestCase("value", "AL", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0065", StringComparison.InvariantCulture)]
    [TestCase("\u0068\u0065\u006c\u006c\u006f", "\u0045", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyNotContains_GivenPropertyDoesContainsUsingCustomComparisonType_ThenShouldNotReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotContains(p => p.Name, otherValue, comparisonType)
          .Match(success => success.Value, error => null!);

        // Assert
        result.Should().Be(person);
}
}