using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.UnitTests.Validators.StringProperties;

public class StringPropertiesEqualityTests
{
    [Test]
    public void WhenCheckingIfPropertyWhiteSpace_GivenPropertyDoesIsWhiteSpace_ThenShouldError()
    {
        // Arrange
        var person = new { Name = " " };

        // Act
        var result = person.Error().IfWhiteSpace(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be white space only. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyWhiteSpace_GivenPropertyDoesIsNotWhiteSpace_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfWhiteSpace(p => p.Name)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNullOrEmpty_GivenPropertyDoesIsNull_ThenShouldError()
    {
        // Arrange
        var person = new { Name = null as string };

        // Act
        var result = person.Error().IfNullOrEmpty(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be null or empty. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrEmpty_GivenPropertyDoesIsEmpty_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "" };

        // Act
        var result = person.Error().IfNullOrEmpty(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be null or empty. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrEmpty_GivenPropertyDoesIsWhiteSpace_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = " " };

        // Act
        var result = person.Error().IfNullOrEmpty(p => p.Name)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person); ;
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrEmpty_GivenPropertyDoesNotEmpty_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Milan" };

        // Act
        var result = person.Error().IfNullOrEmpty(p => p.Name)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person); ;
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrWhiteSpace_GivenPropertyDoesIsNull_ThenShouldError()
    {
        // Arrange
        var person = new { Name = null as string };

        // Act
        var result = person.Error().IfNullOrWhiteSpace(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be null or whitespace. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrWhiteSpace_GivenPropertyDoesIsEmpty_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "" };

        // Act
        var result = person.Error().IfNullOrWhiteSpace(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be null or whitespace. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrWhiteSpace_GivenPropertyDoesIsWhiteSpace_ThenShouldError()
    {
        // Arrange
        var person = new { Name = " " };

        // Act
        var result = person.Error().IfNullOrWhiteSpace(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be null or whitespace. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNullOrWhiteSpace_GivenPropertyDoesIsNotWhiteSpace_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Milan" };

        // Act
        var result = person.Error().IfNullOrWhiteSpace(p => p.Name)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person); 
    }

    [Test]
    public void WhenCheckingIfPropertyEmpty_GivenPropertyDoesIsEmpty_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "" };

        // Act
        var result = person.Error().IfEmpty(p => p.Name)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be empty. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEmpty_GivenPropertyDoesNotEmpty_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEmpty(p => p.Name)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyEquals_GivenPropertyDoesEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEquals(p => p.Name, "Anderson")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be equal to 'Anderson' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEquals_GivenPropertyDoesNotEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEquals(p => p.Name, "Amichai2")
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person); 
    }

    
    [TestCase("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [TestCase("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyEquals_GivenPropertyDoesEqualsUsingCustomComparisonType_ThenShouldReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfEquals(p => p.Name, otherValue, comparisonType)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyEquals_GivenPropertyDoesNotEqualsUsingCustomComparisonType_ThenShouldNotReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfEquals(p => p.Name, otherValue, comparisonType)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyDoesNotEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfNotEquals(p => p.Name, "Jordan")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should be equal to 'Jordan' (comparison type: '{StringComparison.Ordinal}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyDoesEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfNotEquals(p => p.Name, "Anderson")
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
    }

    
    [TestCase("value", "VALUE", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "\u00e5", StringComparison.InvariantCulture)]
    [TestCase("AA", "A\u0000\u0000\u0000a", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyDoesEqualsUsingCustomComparisonType_ThenShouldNotReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotEquals(p => p.Name, otherValue, comparisonType)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person); 
    }

    
    [TestCase("value", "different value", StringComparison.OrdinalIgnoreCase)]
    [TestCase("\u0061\u030a", "different value", StringComparison.InvariantCulture)]
    [TestCase("AA", "different value", StringComparison.InvariantCultureIgnoreCase)]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyDoesNotEqualsUsingCustomComparisonType_ThenShouldReturnError(string value, string otherValue, StringComparison comparisonType)
    {
        // Arrange
        var person = new { Name = value };

        // Act
        var result = person.Error().IfNotEquals(p => p.Name, otherValue, comparisonType)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should be equal to '{otherValue}' (comparison type: '{comparisonType}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEqualsIgnoreCase_GivenPropertyDoesEqualsSameCase_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEqualsIgnoreCase(p => p.Name, "Anderson")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be equal to 'Anderson' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEqualsIgnoreCase_GivenPropertyDoesEqualsDifferentCase_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEqualsIgnoreCase(p => p.Name, "ANDERSON")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be equal to 'ANDERSON' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyEqualsIgnoreCase_GivenPropertyDoesNotEquals_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfEqualsIgnoreCase(p => p.Name, "Jordan")
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNotEqualsIgnoreCase_GivenPropertyDoesNotEquals_ThenShouldError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfNotEqualsIgnoreCase(p => p.Name, "Jordan")
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should be equal to 'Jordan' (comparison type: '{StringComparison.OrdinalIgnoreCase}'). (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotEqualsIgnoreCase_GivenPropertyDoesEqualsSameCase_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfNotEqualsIgnoreCase(p => p.Name, "Anderson")
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyNotEqualsIgnoreCase_GivenPropertyDoesEqualsDifferentCase_ThenShouldNotError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfNotEqualsIgnoreCase(p => p.Name, "ANDERSON")
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}
}