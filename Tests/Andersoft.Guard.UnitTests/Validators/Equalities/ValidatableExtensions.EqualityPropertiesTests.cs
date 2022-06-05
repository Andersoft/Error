using Andersoft.Error.Validators;
using Andersoft.Error.Validators.Equalities;
using FluentAssertions;

namespace Andersoft.Error.UnitTests.Validators.Equalities;


public class EqualityPropertiesTests
{
    [Test]
    public void WhenCheckingIfPropertyIsNull_GivenPropertyIsIsNull_ThenShouldError()
    {
        // Arrange
        var value = new { Property = null as string };

        // Act
       var result = value.Error().IfNull(v => v.Property)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value cannot be null. (Parameter '{nameof(value)}: v => v.Property')");
    }

    [Test]
    public void WhenCheckingIfPropertyIsNull_GivenPropertyIsIsNotNull_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = "value" };

        // Act
       var result = value.Error().IfNull(v => v.Property).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyIsNotNull_GivenPropertyIsIsNotNull_ThenShouldError()
    {
        // Arrange
        var value = new { Property = "value" };

        // Act
       var result = value.Error().IfNotNull(v => v.Property)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be null. (Parameter '{nameof(value)}: v => v.Property')");
    }

    [Test]
    public void WhenCheckingIfPropertyIsNotNull_GivenPropertyIsIsNull_ThenShouldError()
    {
        // Arrange
        var value = new { Property = default(string) };

        // Act
       var result = value.Error().IfNotNull(v => v.Property).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyIsDefault_GivenValueIsDefault_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = default(System.DateTime) };

        // Act
       var result = person.Error().IfDefault(p => p.BirthDate)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be default. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfPropertyIsDefault_GivenValueIsNotDefault_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfDefault(p => p.BirthDate).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfPropertyIsNotDefault_GivenValueIsNotDefault_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfNotDefault(p => p.BirthDate)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be default. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfPropertyIsNotDefault_GivenValueIsDefault_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = default(System.DateTime) };

        // Act
       var result = person.Error().IfNotDefault(p => p.BirthDate).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfPropertyEquals_GivenPropertyIsEquals_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfEquals(v => v.Property, 5)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be equal to 5. (Parameter '{nameof(value)}: v => v.Property')");
    }

    [Test]
    public void WhenCheckingIfPropertyEquals_GivenPropertyIsNotEquals_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfEquals(v => v.Property, 6).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }

    [Test]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyIsNotEquals_ThenShouldError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfNotEquals(v => v.Property, 6)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be equal to 6. (Parameter '{nameof(value)}: v => v.Property')");
    }

    [Test]
    public void WhenCheckingIfPropertyNotEquals_GivenPropertyIsEquals_ThenShouldNotError()
    {
        // Arrange
        var value = new { Property = 5 };

        // Act
       var result = value.Error().IfNotEquals(v => v.Property, 5).Match(success => success.Value, error => default!); ;

        // Assert
        result.Should().Be(value);
    }
}