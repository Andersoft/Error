using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.Strings;
using FluentAssertions;
using LanguageExt;

namespace Andersoft.Guard.UnitTests.Validators.StringProperties;


public class StringPropertiesLengthTests
{
    [Test]
    public void WhenCheckingIfPropertyLongerThan_GivenPropertyDoesIsLongerThan_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLongerThan(p => p.Name, 3)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be longer than 3 characters. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyLongerThan_GivenPropertyDoesIsNotLongerThan_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLongerThan(p => p.Name, 10)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyShorterThan_GivenPropertyDoesIsShorterThan_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfShorterThan(p => p.Name, 10)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String should not be shorter than 10 characters. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyShorterThan_GivenPropertyDoesIsNotShorterThan_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfShorterThan(p => p.Name, 3)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyLengthEquals_GivenPropertyDoesLengthEquals_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLengthEquals(p => p.Name, 8)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String length should not be equal to 8. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyLengthEquals_GivenPropertyDoesLengthNotEquals_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLengthEquals(p => p.Name, 100)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}

    [Test]
    public void WhenCheckingIfPropertyLengthNotEquals_GivenPropertyDoesLengthNotEquals_ThenShouldReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLengthNotEquals(p => p.Name, 100)
          .Match(_ => null, exception => exception as ArgumentException);

        // Assert
        result!.Message.Should().Be($"String length should be equal to 100. (Parameter '{nameof(person)}: p => p.Name')");
    }

    [Test]
    public void WhenCheckingIfPropertyLengthNotEquals_GivenPropertyDoesLengthEquals_ThenShouldNotReturnError()
    {
        // Arrange
        var person = new { Name = "Anderson" };

        // Act
        var result = person.Error().IfLengthNotEquals(p => p.Name, 8)
          .Match(success => success, error => null!);

        // Assert
        result.Should().Be(person);
}
}