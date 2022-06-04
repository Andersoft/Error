using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.DateTimeValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.DateTime;


public class DateTimePropertiesTests
{
    [Test]
    public void WhenCheckingIfDateTimePropertyUtc_GivenValueIsUtc_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfUtc(p => p.BirthDate)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be Utc. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfDateTimePropertyUtc_GivenValueIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfUtc(p => p.BirthDate);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimePropertyNotUtc_GivenValueIsNotUtc_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfNotUtc(p => p.BirthDate)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be Utc. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfDateTimePropertyNotUtc_GivenValueIsUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfNotUtc(p => p.BirthDate);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyUtc_GivenValueIsUtc_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfDateTimeKind(p => p.BirthDate, DateTimeKind.Utc)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should not be Utc. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyUtc_GivenValueIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfDateTimeKind(p => p.BirthDate, DateTimeKind.Utc);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyNotUtc_GivenValueIsNotUtc_ThenShouldError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfDateTimeNotKind(p => p.BirthDate, DateTimeKind.Utc)
         .Match(_ => null, exception => exception as ArgumentException);

       // Assert
       result!.Message.Should().Be($"Value should be Utc. (Parameter '{nameof(person)}: p => p.BirthDate')");
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyNotUtc_GivenValueIsUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfDateTimeNotKind(p => p.BirthDate, DateTimeKind.Utc);

        // Assert
        result.Should().Be(person);
    }
}
