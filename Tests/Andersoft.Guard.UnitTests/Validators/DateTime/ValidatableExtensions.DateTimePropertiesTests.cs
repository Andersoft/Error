using Andersoft.Guard.Validators;
using Andersoft.Guard.Validators.DateTimeValidators;
using FluentAssertions;

namespace Andersoft.Guard.UnitTests.Validators.DateTime;


public class DateTimePropertiesTests
{
    [Test]
    public void WhenCheckingIfDateTimePropertyUtc_WhenValueIsUtc_ThenShouldError()
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
    public void WhenCheckingIfDateTimePropertyUtc_WhenValueIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfUtc(p => p.BirthDate);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimePropertyNotUtc_WhenValueIsNotUtc_ThenShouldError()
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
    public void WhenCheckingIfDateTimePropertyNotUtc_WhenValueIsUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfNotUtc(p => p.BirthDate);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyUtc_WhenValueIsUtc_ThenShouldError()
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
    public void WhenCheckingIfDateTimeKindPropertyUtc_WhenValueIsNotUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.Now };

        // Act
       var result = person.Error().IfDateTimeKind(p => p.BirthDate, DateTimeKind.Utc);

        // Assert
        result.Should().Be(person);
    }

    [Test]
    public void WhenCheckingIfDateTimeKindPropertyNotUtc_WhenValueIsNotUtc_ThenShouldError()
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
    public void WhenCheckingIfDateTimeKindPropertyNotUtc_WhenValueIsUtc_ThenShouldNotError()
    {
        // Arrange
        var person = new { BirthDate = System.DateTime.UtcNow };

        // Act
       var result = person.Error().IfDateTimeNotKind(p => p.BirthDate, DateTimeKind.Utc);

        // Assert
        result.Should().Be(person);
    }
}
